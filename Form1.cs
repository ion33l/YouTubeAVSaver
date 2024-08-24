using AngleSharp.Dom;

using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

using System.Diagnostics;
using System.Threading;
using System;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        public YoutubeClient ytClient;
        private CancellationTokenSource cancellationTokenSource;
        private List<(bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution, string[] Sizes, string url)> videoControlValues =
                new List<(bool, int, string, string, string[], string, string[], string)>();
        private List<(CheckBox checkBox, Label noLabel, TextBox titleTextBox, PictureBox pictureBox, ComboBox resolutionComboBox, string url)> videoControlReferences =
            new List<(CheckBox, Label, TextBox, PictureBox, ComboBox, string)>();
        private Progress<ProgressInfo> progressReporter;

        public string lastFetchedVideoUrl = "";
        public bool fromPlaylist = false;
        public string totalDuration = "";
        public bool downloadParts = false;
        public bool downloadThumbnail = false;

        public Form1()
        {
            InitializeComponent();
            ytClient = new YoutubeClient();
            cancelButton.Hide();
            panelAudioOnly.Visible = false;
            progressBar.Visible = false;
            showClearTitleOf(false);
            cancellationTokenSource = new CancellationTokenSource();
            progressReporter = new Progress<ProgressInfo>(info =>
            {
                progressBar.Value = info.Value;
                progressBar.Visible = info.Visible;
            });

            this.Resize += new EventHandler(Form1_Resize);
            labelPlaylist.SizeChanged += new EventHandler(labelPlaylist_SizeChanged);
            CenterLabel();

            mainAnchors();

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this.videoAndAudioButton, "mp4 download.");
            toolTip.SetToolTip(this.audioOnlyButton, "mp3 download. For playlists that are music albums, it will make \na folder tree: \"artist/year - album\" and add the mp3 tags.");
            toolTip.SetToolTip(this.textBoxSearch, "Parses the above titles and removes the text you enter here.\nFor playlists that are music albums it's best to keep only the song names,\n as tags will add the artist and other info.");
            toolTip.SetToolTip(this.labelClearTitleOf, "Parses the above titles and removes the text you enter here.\nFor playlists that are music albums it's best to keep only the song names,\n as tags will add the artist and other info.");
            toolTip.SetToolTip(this.checkBox1, "If checked it will look in the YouTube description and search\nfor chapters/parts times. If not found, it will let you add yours.");
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterLabel();
        }
        private void labelPlaylist_SizeChanged(object sender, EventArgs e)
        {
            CenterLabel();
        }
        private void CenterLabel()
        {
            label8.Left = (this.ClientSize.Width - label8.Width) / 2;
            labelPlaylist.Left = (this.ClientSize.Width - labelPlaylist.Width) / 2;
        }
        public void mainAnchors()
        {
            fetchButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            downloadButton.Anchor = AnchorStyles.Bottom;
            youtubeURLTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label15.Anchor = AnchorStyles.Top;

            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right; //Resolution
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Right; //Size
            label8.Anchor = AnchorStyles.Top;  //VIDEOS TO DOWNLOAD
            labelPlaylist.Anchor = AnchorStyles.Top;

            scrollablePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelAudioOnly.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        }

        private struct ProgressInfo
        {
            public int Value { get; set; }
            public bool Visible { get; set; }
        }

        private void youtubeURLTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Suppress the default beep sound
                e.SuppressKeyPress = true;

                // Call the button click event handler
                fetchButton_Click(this, new EventArgs());
            }
        }

        private (bool Check, int No, string Title, string Thumbnail, string SelectedResolution)[]
             GetFetchedVideoDetails()   /*TODO remove this if not used*/
        {
            var fetchedVideoDetails = new List<(bool, int, string, string, string)>();

            foreach (var (checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox, url) in videoControlReferences)
            {
                bool isChecked = checkBox.Checked;
                int no = int.Parse(noLabel.Text); // Assuming the label text is the number
                string title = titleTextBox.Text;
                string thumbnail = pictureBox.ImageLocation;
                string selectedResolution = resolutionComboBox.SelectedItem?.ToString() ?? "N/A";

                fetchedVideoDetails.Add((isChecked, no, title, thumbnail, selectedResolution));
            }

            return fetchedVideoDetails.ToArray();
        }

        async Task<(bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution, string[] Sizes, string url)[]>
             getPanelVideosDetailsAsync(string url)
        {
            label8.Text = "VIDEO(S) TO DOWNLOAD";
            labelPlaylist.Text = "";

            if (url.Contains("playlist"))
            {
                fromPlaylist = true;
                var playlist = await ytClient.Playlists.GetAsync(url);
                var playlistVideos = await ytClient.Playlists.GetVideosAsync(playlist.Id);
                progressBar.Visible = true;
                for (int i = 0; i < playlistVideos.Count; i++)
                {
                    UpdateProgress((int)(i * 100 / playlistVideos.Count));

                    var videoId = YoutubeExplode.Videos.VideoId.Parse(playlistVideos[i].Url);
                    var streamManifest = await ytClient.Videos.Streams.GetManifestAsync(videoId);
                    var videoStreams = streamManifest.GetVideoStreams();
                    var audioStreams = streamManifest.GetAudioOnlyStreams();
                    var audioStreamInfo = audioStreams
                                            .OrderByDescending(s => s.Bitrate)
                                            .Select(s => new
                                            {
                                                audioSize = (s.Size.Bytes)// / (1024.0 * 1024.0)).ToString("0.##")// + " MB"
                                            })
                                            .Take(1)
                                            .SingleOrDefault();

                    var resolutionSizeList = videoStreams
                                            //.Where(s => s.Container == YoutubeExplode.Videos.Streams.Container.Mp4)
                                            .OrderByDescending(s => s.VideoQuality)
                                            .DistinctBy(s => s.VideoQuality.Label, StringComparer.OrdinalIgnoreCase)
                                            .Select(s => new
                                            {
                                                Resolution = s.VideoQuality.Label,
                                                videoSize = ((s.Size.Bytes + audioStreamInfo.audioSize) / (1024.0 * 1024.0) * 1.00308).ToString("0.##") + " MB" //after converting it gets just a bit larger
                                            })
                                            .ToList();

                    string[] resolutions = resolutionSizeList.Select(rs => rs.Resolution).ToArray();
                    string[] sizes = resolutionSizeList.Select(rs => rs.videoSize).ToArray();
                    for (int j = 0; j < sizes.Length; j++)
                    {
                        if (resolutions[j] == null)
                            sizes[j] = "N/A";
                    }

                    videoControlValues.Add((true, i + 1, playlistVideos[i].Title, playlistVideos[i].Thumbnails.GetWithHighestResolution().Url, resolutions.ToArray(), resolutions.ToArray()[0], sizes.ToArray(), playlistVideos[i].Url));
                }
                progressBar.Visible = false;
                label8.Text = "VIDEOS TO DOWNLOAD FOR PLAYLIST:";
                labelPlaylist.Text = playlist.Title;
            }
            else
            {
                var video = await ytClient.Videos.GetAsync(url);
                var videoId = YoutubeExplode.Videos.VideoId.Parse(url);
                var streamManifest = await ytClient.Videos.Streams.GetManifestAsync(videoId);
                var videoStreams = streamManifest.GetVideoStreams();
                var audioStreams = streamManifest.GetAudioOnlyStreams();
                var audioStreamInfo = audioStreams
                                        .OrderByDescending(s => s.Bitrate)
                                        .Select(s => new
                                        {
                                            audioSize = (s.Size.Bytes)// / (1024.0 * 1024.0)).ToString("0.##")// + " MB"
                                        })
                                        .Take(1)
                                        .SingleOrDefault();

                var resolutionSizeList = videoStreams
                                        //.Where(s => s.Container == YoutubeExplode.Videos.Streams.Container.Mp4)
                                        .OrderByDescending(s => s.VideoQuality)
                                        .DistinctBy(s => s.VideoQuality.Label, StringComparer.OrdinalIgnoreCase)
                                        .Select(s => new
                                        {
                                            Resolution = s.VideoQuality.Label,
                                            videoSize = ((s.Size.Bytes + audioStreamInfo.audioSize) / (1024.0 * 1024.0)).ToString("0.##") + " MB"
                                        })
                                        .ToList();

                string[] resolutions = resolutionSizeList.Select(rs => rs.Resolution).ToArray();
                string[] sizes = resolutionSizeList.Select(rs => rs.videoSize).ToArray();
                for (int j = 0; j < sizes.Length; j++)
                {
                    if (resolutions[j] == null)
                        sizes[j] = "N/A";
                }

                videoControlValues.Add((true, 1, video.Title, video.Thumbnails.GetWithHighestResolution().Url, resolutions.ToArray(), resolutions.ToArray()[0], sizes.ToArray(), url));

                label8.Text = "VIDEO TO DOWNLOAD:";
                labelPlaylist.Text = "";
            }

            return videoControlValues.ToArray();
        }

        void UpdateSizeLabel(int selectedIndex, string[] sizes, Label sizeLabel)
        {
            string correspondingSize = "N/A";

            if (sizes != null && selectedIndex >= 0 && selectedIndex < sizes.Length)
            {
                correspondingSize = sizes[selectedIndex];
            }

            sizeLabel.Text = correspondingSize;

        }

        private void UpdateProgress(int value)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<int>(UpdateProgress), value);
            }
            else
            {
                progressBar.Value = value;
            }
        }

        void showClearTitleOf(bool show)
        {
            if (show)
            {
                labelClearTitleOf.Visible = true;
                textBoxSearch.Visible = true;
                buttonClearText.Visible = true;
                    
            }
            else
            {
                labelClearTitleOf.Visible = false;
                textBoxSearch.Visible = false;
                buttonClearText.Visible = false;
            }
        }
        private void ClearPanel()
        {
            scrollablePanel.Controls.Clear();
            videoControlReferences.Clear();
            videoControlValues.Clear();
        }

        private void UpdatePanel((bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution, string[] Sizes, string url)[] videos)
        {
            Color oddColor = Color.FromArgb(250, 250, 250); // Light grey for odd entries
            Color evenColor = Color.FromArgb(220, 220, 220); // Slightly darker grey for even entries
            Color white = Color.White;

            bool exceedsScrollablePanel = false;
            if (videos.Length > (scrollablePanel.Height / 80 /*future videoPanel.Height*/))
                exceedsScrollablePanel = true;

            showClearTitleOf(videos.Length > 1);

            for (int i = 0; i < videos.Length; i++)
            {
                var video = videos[i];
                int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
                // Choose the background color based on the index
                Color backgroundColor = (i % 2 == 0) ? evenColor : oddColor;

                // Container Panel for each video entry
                Panel videoPanel = new Panel
                {
                    Location = new Point(10, i * 80 + 10),
                    Size = new Size(exceedsScrollablePanel ? scrollablePanel.Width - scrollbarWidth : scrollablePanel.Width - scrollbarWidth - scrollbarWidth, 80),
                    BackColor = backgroundColor
                };

                // Checkbox
                CheckBox checkBox = new CheckBox
                {
                    Location = new Point(3, 25),
                    Checked = video.Check,
                    BackColor = backgroundColor,
                    Size = new Size(20, 20)
                };
                videoPanel.Controls.Add(checkBox);

                // No (Label)
                Label noLabel = new Label
                {
                    Location = new Point(30, 27),
                    Text = video.No.ToString(),
                    Size = new Size(20, 20),
                    BackColor = backgroundColor
                };
                videoPanel.Controls.Add(noLabel);

                // Title (TextBox)

                TextBox titleTextBox = new TextBox
                {
                    Location = new Point(50, 25),
                    Width = ((i < (int)(scrollablePanel.Height / videoPanel.Height) + 1) && exceedsScrollablePanel) ? (scrollablePanel.Width - 225) : (scrollablePanel.Width - 225 - scrollbarWidth),  /* sum of all other items + bug if scrollable panel exceeds the visible panel. the scrollbar appears*/
                    //Width = scrollablePanel.Width - 225 - scrollbarWidth,
                    Text = video.Title,
                    BackColor = white
                };
                titleTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                videoPanel.Controls.Add(titleTextBox);

                // Thumbnail (PictureBox)
                PictureBox pictureBox = new PictureBox
                {
                    Location = new Point(330, 25),
                    Size = new Size(50, 50),
                    ImageLocation = video.Thumbnail, // TODO - Use actual thumbnail path
                    //Image = thumbnailImage,// ?? Properties.Resources.DefaultImage,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = backgroundColor
                };
                //videoPanel.Controls.Add(pictureBox); //decided not to show thumnails because of its overhead

                // Resolution (ComboBox)

                ComboBox resolutionComboBox = new ComboBox
                {
                    Location = new Point(((i < (int)(scrollablePanel.Height / videoPanel.Height) + 1) && exceedsScrollablePanel) ? (scrollablePanel.Width - 167) : (scrollablePanel.Width - 167 - scrollbarWidth), 25),
                    //Location = new Point(scrollablePanel.Width - 167 /* size + resolution*/, 25),
                    Width = 67,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BackColor = white
                };

                // Add unique resolution options for each video
                resolutionComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                resolutionComboBox.Items.AddRange(video.Resolutions);

                // Select the appropriate resolution
                if (video.SelectedResolution != null && Array.Exists(video.Resolutions, res => res == video.SelectedResolution))
                {
                    resolutionComboBox.SelectedItem = video.SelectedResolution;
                }
                else
                {
                    resolutionComboBox.Items.Add("N/A");
                    resolutionComboBox.SelectedItem = "N/A";
                }

                videoPanel.Controls.Add(resolutionComboBox);

                // Get the index of the selected resolution
                int selectedIndex = resolutionComboBox.SelectedIndex;
                string correspondingSize = "N/A";

                if (selectedIndex >= 0 && selectedIndex < video.Sizes.Length)
                {
                    correspondingSize = video.Sizes[selectedIndex];
                }

                // Size (Label)
                Label sizeLabel = new Label
                {
                    Location = new Point(((i < (int)(scrollablePanel.Height / videoPanel.Height) + 1) && exceedsScrollablePanel) ? (scrollablePanel.Width - 90) : (scrollablePanel.Width - 90 - scrollbarWidth), 27),
                    //Location = new Point(scrollablePanel.Width - 100, 27),
                    Text = correspondingSize,
                    Size = new Size(90, 20),
                    BackColor = backgroundColor
                };

                // Update the Label initially
                UpdateSizeLabel(resolutionComboBox.SelectedIndex, video.Sizes, sizeLabel);

                sizeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                videoPanel.Controls.Add(sizeLabel);

                // Handle the SelectedIndexChanged event
                resolutionComboBox.SelectedIndexChanged += (sender, e) =>
                {
                    if (sender is ComboBox comboBox)
                    {
                        UpdateSizeLabel(comboBox.SelectedIndex, video.Sizes, sizeLabel);
                    }

                };
                string url = video.url;

                videoPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                scrollablePanel.Controls.Add(videoPanel);

                // Store control references
                videoControlReferences.Add((checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox, url));
            }
        }

        void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null) return;

            string output = e.Data;
            string timeString = "";
            if (output.StartsWith("  Duration"))
            {
                totalDuration = output.Substring(12, 11);
            }
            if (!string.IsNullOrEmpty(totalDuration))
            {
                if (output.Contains("time="))
                {
                    timeString = output.Substring(output.IndexOf("time=") + 5, 11);

                    try
                    {
                        TimeSpan currentTime = TimeSpan.Parse(timeString);
                        TimeSpan totalDurationTime = TimeSpan.Parse(totalDuration);
                        int progressPercentage = (int)(currentTime.TotalSeconds / totalDurationTime.TotalSeconds * 100);
                        ((IProgress<ProgressInfo>)progressReporter).Report(new ProgressInfo { Value = progressPercentage, Visible = true });
                    }
                    catch
                    {
                        ((IProgress<ProgressInfo>)progressReporter).Report(new ProgressInfo { Value = 100, Visible = false });
                    }
                }
            }
        }

        public void showProgressBarAndOthers(bool show, string textBoxText)
        {
            if (show)
            {
                labelOperation.Text = textBoxText;
                cancellationTokenSource = new CancellationTokenSource();
                progressBar.Visible = true;
                cancelButton.Show();
                downloadButton.Enabled = false;
                browseButton.Enabled = false;
                fetchButton.Enabled = false;
            }
            else
            {
                labelOperation.Text = "";
                if (cancellationTokenSource != null)
                    cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
                progressBar.Visible = false;
                cancelButton.Hide();
                downloadButton.Enabled = true;
                browseButton.Enabled = true;
                fetchButton.Enabled = true;
            }
        }

        public async Task CopyToAsyncWithProgress(Stream videoStream, Stream fileStream, IProgress<double> progress, CancellationToken cancellationToken)
        {
            var totalBytes = videoStream.Length;
            var buffer = new byte[81920]; // 80 KB buffer
            int bytesRead;
            long totalBytesRead = 0;

            while ((bytesRead = await videoStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                totalBytesRead += bytesRead;

                // Report progress if progress is not null and totalBytes is available
                if (progress != null && totalBytes > 0)
                {
                    var progressPercentage = (double)totalBytesRead / totalBytes * 100;
                    progress.Report(progressPercentage);
                }
            }
        }

        private static TimeSpan ParseTime(string time)
        {
            // Split the time string into components
            var parts = time.Split(':');
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            if (parts.Length == 2)
            {
                // Format: mm:ss
                minutes = int.Parse(parts[0]);
                seconds = int.Parse(parts[1]);
            }
            else if (parts.Length == 3)
            {
                // Format: hh:mm:ss
                hours = int.Parse(parts[0]);
                minutes = int.Parse(parts[1]);
                seconds = int.Parse(parts[2]);
            }

            // Return the TimeSpan created with hours, minutes, and seconds
            return new TimeSpan(hours, minutes, seconds);
        }

        private String getTruncatedIndexTitleString(int index, String title)
        {
            String newTitle;

            if (title.Length > 17)
            {
                newTitle = (index + 1).ToString() + ". " + title.Substring(0, 17) + ".."; // Add "..." to indicate truncation
            }
            else
                newTitle = (index + 1).ToString() + ". " + title;

            return newTitle;
        }

        public static List<SongSegment> getPartsFromDescription(string description, string artist, TimeSpan maxLength)
        {
            try
            {
                var segments = new List<SongSegment>();

                // Split description into lines
                var lines = description.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    string pattern = @"(?<time>\d{1,2}:\d{2}(?::\d{2})?)\s*(?<title>.+)?|(?<title>.+?)\s*(?<time>\d{1,2}:\d{2}(?::\d{2})?)";
                    var match = Regex.Match(line, pattern);

                    if (match.Success)
                    {
                        string time = match.Groups["time"].Value;
                        string title = match.Groups["title"].Value.Trim();
                        title = title.Trim(' ', '-');

                        var startTime = ParseTime(time);

                        segments.Add(new SongSegment
                        {
                            StartTime = startTime,
                            EndTime = TimeSpan.Zero, // Placeholder, to be determined later
                            Title = title,
                            Artist = artist

                        });
                    }
                }

                // Calculate the end times
                for (int i = 0; i < segments.Count - 1; i++)
                {
                    segments[i].EndTime = segments[i + 1].StartTime;
                }
                if (segments.Count > 0)
                {
                    segments[segments.Count - 1].EndTime = maxLength; // Last segment's end time is unknown
                }

                return segments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<SongSegment>();
            }
        }

        public async Task<(string artist, string album, string year, string genre)> getTagsFromURL(string URL)
        {
            string artist = "",
                   album = "",
                   year = "",
                   genre = "";
            try
            {
                if (URL.Contains("playlist"))
                {
                    var playlist = await ytClient.Playlists.GetAsync(URL);
                    var playlistVideos = await ytClient.Playlists.GetVideosAsync(playlist.Id);
                    album = playlist.Title;

                    var videoDetails = await ytClient.Videos.GetAsync(playlistVideos[0].Id);
                    var parts = videoDetails.Title.Split(new[] { '-' }, 2);
                    if (parts.Length == 2)
                        artist = parts[0].Trim();
                    else
                        artist = playlistVideos[0].Author.ToString();

                    year = System.Text.RegularExpressions.Regex.Match(videoDetails.Description, @"\b\d{4}\b").ToString();
                    if (year == "")
                        year = videoDetails.UploadDate.Year.ToString();
                }
                else
                {
                    var videoDetails = await ytClient.Videos.GetAsync(URL);
                    var parts = videoDetails.Title.Split(new[] { '-' }, 2);
                    if (parts.Length == 2)
                    {
                        artist = parts[0].Trim();
                        album = parts[1].Trim();
                        char[] stopChars = { '{', '}', '(', ')', '/', '[', ']', '-', '_', '?' };
                        int index = 0;
                        while (index < album.Length && !stopChars.Contains(album[index]))
                        {
                            index++;
                        }
                        album = album.Substring(0, index).Trim();
                    }
                    else
                        artist = videoDetails.Author.ToString();
                    /*TODO - refactor*/
                    year = System.Text.RegularExpressions.Regex.Match(videoDetails.Description, @"\b\d{4}\b").ToString();
                    if (year == "")
                        year = videoDetails.UploadDate.Year.ToString();
                }
            }
            catch
            {

            }
            return (artist, album, year, genre);
        }

        string CreateAlbumFolders(string downloadPath, string artist, string album, string year)
        {
            //Create folders 
            string yearAlbumName = year + " - " + album;
            string artistFolderPath = Path.Combine(downloadPath, artist);
            string albumFolderPath = Path.Combine(artistFolderPath, yearAlbumName);
            try
            {
                // Create artist folder if it doesn't exist
                if (!Directory.Exists(artistFolderPath))
                {
                    Directory.CreateDirectory(artistFolderPath);
                }

                // Create album folder if it doesn't exist
                if (!Directory.Exists(albumFolderPath))
                {
                    Directory.CreateDirectory(albumFolderPath);
                }

                return albumFolderPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return "";
        }

        public Task SetMp3Tags(string filePath, string trackNumber, string artist, string trackTitle, string album, string year, string genre)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            return Task.Run(() =>
            {
                try
                {
                    using (var tagFile = TagLib.File.Create(filePath))
                    {
                        var tag = (TagLib.Id3v2.Tag)tagFile.GetTag(TagLib.TagTypes.Id3v2, true);

                        tag.Performers = new[] { artist };
                        tag.Title = trackTitle;
                        tag.Track = uint.Parse(trackNumber);
                        tag.Album = album;
                        tag.Year = uint.Parse(year);

                        var genreFrame = new TagLib.Id3v2.TextInformationFrame(ident: TagLib.ByteVector.FromString("TCON"), TagLib.StringType.Latin1);
                        genreFrame.Text = new[] { genre };
                        tag.AddFrame(genreFrame);

                        tagFile.Save();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when setting tags for {trackTitle}: {ex.Message}");
                }
            }, token);
        }

        private Task ExecuteFFMpegCommand(string arguments)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;


            return Task.Run(() =>
            {
                //var ffmpegPath = @"C:\Program Files\ffmpeg-master-latest-win64-gpl\bin\ffmpeg.exe"; 
                var ffmpegPath = Path.Combine(Application.StartupPath, "Resources", "FFmpeg", "ffmpeg.exe");

                var startInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceived);
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    //process.WaitForExit();

                    while (!process.WaitForExit(100))
                    {
                        if (token.IsCancellationRequested)
                        {
                            process.Kill();
                            break;
                        }
                    }
                }

                ((IProgress<ProgressInfo>)progressReporter).Report(new ProgressInfo { Value = 0, Visible = false });

            }, token);
        }

        private Task CombineAudioAndVideo(string videoPath, string audioPath, string outputPath, bool videoAndAudio)
        {
            string arguments;
            totalDuration = "";
            ((IProgress<ProgressInfo>)progressReporter).Report(new ProgressInfo { Value = 0, Visible = true });

            if (videoAndAudio)
                arguments = $"-y -i \"{videoPath}\" -i \"{audioPath}\" -c copy \"{outputPath}\"";
            else
                arguments = $"-y -i \"{audioPath}\" -vn -c:a libmp3lame \"{outputPath}\"";

            return ExecuteFFMpegCommand(arguments);
        }

        private string FormatTimeSpan(TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"hh\:mm\:ss\.fff");
        }

        private async Task<List<string>> spliFileIntoSegments(string toSplitFilePath, List<SongSegment> segments, string splitOutputDirectory, bool videoAndAudio, IProgress<double> progress)
        {
            List<string> outputFiles = new List<string>();

            try
            {
                for (int i = 0; i < segments.Count; i++)
                {
                    var segment = segments[i];
                    TimeSpan duration = segment.EndTime - segment.StartTime;

                    if (duration < TimeSpan.Zero)
                        continue;

                    string startTime = FormatTimeSpan(segment.StartTime);
                    string durationString = FormatTimeSpan(duration);
                    string trackNumber = (i + 1).ToString("D2");
                    string sanitizedTitle = new string(segment.Title.Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray());
                    string outputFileName;
                    if (videoAndAudio)
                        outputFileName = $"{trackNumber}. {sanitizedTitle}.mp4";
                    else
                        outputFileName = $"{trackNumber}. {sanitizedTitle}.mp3";

                    string progressBarMessage = videoAndAudio ? $"  IV: Splitting mp4s:\n {getTruncatedIndexTitleString(i + 1, outputFileName)}" :
                                                                $"  III: Splitting mp3s:\n {getTruncatedIndexTitleString(i + 1, outputFileName)}";

                    string outputFilePath = Path.Combine(splitOutputDirectory, outputFileName);

                    showProgressBarAndOthers(true, progressBarMessage);
                    UpdateProgress((int)((i + 1) * 100 / segments.Count));

                    // Build ffmpeg command arguments
                    string arguments = $"-y -i \"{toSplitFilePath}\" -ss {startTime} -t {durationString} -c copy \"{outputFilePath}\"";

                    // Execute ffmpeg process
                    await ExecuteFFMpegCommand(arguments);
                    // Add the output file path to the list
                    outputFiles.Add(outputFilePath);

                    showProgressBarAndOthers(false, "");
                }

                return outputFiles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred when splitting the file: {ex.Message}");
                return new List<string>();
            }
        }

        public static async Task DownloadThumbnailAsync(string thumbnailUrl, string downloadDirectory, string title, bool itemIsAlbum, bool videoAndAudio)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(thumbnailUrl, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();

                        var uri = new Uri(thumbnailUrl);
                        string path = uri.AbsolutePath;
                        string extension = Path.GetExtension(path);
                        string fileExtension = string.IsNullOrEmpty(extension) ? ".jpg" : extension;
                        string downloadPath;

                        if (itemIsAlbum)
                            downloadPath = Path.Combine(downloadDirectory, $"cover{fileExtension}");
                        else
                            downloadPath = videoAndAudio ? Path.Combine(downloadDirectory, $"{title} - thumbnail{fileExtension}") : Path.Combine(downloadDirectory, $"{title} - cover{fileExtension}");

                        // Stream the content to a file
                        using (var contentStream = await response.Content.ReadAsStreamAsync())
                        using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await contentStream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
            catch { }
        }

        async void downloadAudioVideo(string URL, string downloadPath)
        {
            progressBar.Value = 0;
            var progress = new Progress<double>(percent =>
            {
                // Update the progress bar value
                progressBar.Value = (int)percent;
            });

            foreach (var (videoControl, index) in videoControlReferences.Select((value, index) => (value, index)))
            {
                var (checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox, url) = videoControl;
                // Check if the CheckBox is checked
                if (checkBox.Checked)
                {
                    var videoId = YoutubeExplode.Videos.VideoId.Parse(url);
                    var streamManifest = await ytClient.Videos.Streams.GetManifestAsync(videoId);
                    string selectedResolution = resolutionComboBox.SelectedItem?.ToString() ?? "default_resolution";
                    string filename_raw = titleTextBox.Text;
                    string filename = new string(filename_raw.Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray());
                    var filePath = Path.Combine(downloadPath, filename);
                    var outputFilePath = filePath + ".mp4";

                    var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                    var videoStreamInfo = streamManifest
                                        .GetVideoStreams()  //worked with GetVideoOnlyStreams()
                                        .Where(s => s.VideoQuality.Label == selectedResolution)
                                        .Take(1)
                                        .SingleOrDefault();

                    if (videoStreamInfo != null && audioStreamInfo != null)
                    {
                        var videoTempPath = Path.Combine(downloadPath, $"video.{videoStreamInfo.Container.Name}");
                        var videoStream = await ytClient.Videos.Streams.GetAsync(streamInfo: videoStreamInfo);

                        try
                        {
                            showProgressBarAndOthers(true, $"  I: Video Download for:\n {getTruncatedIndexTitleString(index, videoControl.titleTextBox.Text)}");

                            using (var fileStream = new FileStream(videoTempPath, FileMode.Create, FileAccess.Write))
                            {
                                //await videoStream.CopyToAsync(fileStream);
                                await CopyToAsyncWithProgress(videoStream, fileStream, progress, cancellationTokenSource.Token);
                            }
                            showProgressBarAndOthers(false, "");
                        }
                        catch (OperationCanceledException)
                        {
                            MessageBox.Show("Download was cancelled.");
                            showProgressBarAndOthers(false, "");
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred when downloading video: {ex.Message}");
                            showProgressBarAndOthers(false, "");
                            return;
                        }

                        var audioTempPath = Path.Combine(downloadPath, $"audio.{audioStreamInfo.Container.Name}");
                        var audioStream = await ytClient.Videos.Streams.GetAsync(streamInfo: audioStreamInfo);

                        try
                        {
                            showProgressBarAndOthers(true, $"  II: Audio Download for:\n {getTruncatedIndexTitleString(index, videoControl.titleTextBox.Text)}");

                            using (var fileStream = new FileStream(audioTempPath, FileMode.Create, FileAccess.Write))
                            {
                                //await audioStream.CopyToAsync(fileStream);
                                await CopyToAsyncWithProgress(audioStream, fileStream, progress, cancellationTokenSource.Token);
                            }
                            showProgressBarAndOthers(false, "");
                        }
                        catch (OperationCanceledException)
                        {
                            MessageBox.Show("Download was cancelled.");
                            showProgressBarAndOthers(false, "");
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred when downloading video: {ex.Message}");
                            showProgressBarAndOthers(false, "");
                            return;
                        }

                        showProgressBarAndOthers(true, $"  III: Convert to mp4:\n {getTruncatedIndexTitleString(index, videoControl.titleTextBox.Text)}");

                        await CombineAudioAndVideo(videoTempPath, audioTempPath, outputFilePath, true);

                        showProgressBarAndOthers(false, "");

                        if (downloadParts)
                        {
                            bool keepBigFile = false;
                            var videoDetails = await ytClient.Videos.GetAsync(url);

                            TimeSpan duration = videoDetails.Duration.Value;

                            var segments = getPartsFromDescription(videoDetails.Description, "", duration);

                            using (var form = new SplitterForm(segments, true, duration))
                            {
                                if (form.ShowDialog() == DialogResult.OK)
                                {
                                    segments = form.SongSegments;
                                    keepBigFile = form.keepInitialBigFile;
                                }
                            }

                            showProgressBarAndOthers(true, "  IV: Splitting tracks");

                            try
                            {
                                string partsFolderPath = Path.Combine(downloadPath, Path.GetFileNameWithoutExtension(outputFilePath) + " - Chapters");
                                if (!Directory.Exists(partsFolderPath))
                                {
                                    Directory.CreateDirectory(partsFolderPath);
                                }
                                downloadPath = partsFolderPath;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            List<string> songPathList = await spliFileIntoSegments(outputFilePath, segments, downloadPath, true, progress);

                            showProgressBarAndOthers(false, "");

                            if (!keepBigFile)
                                try { File.Delete(outputFilePath); } catch { }
                        }

                        if (downloadThumbnail == true && ((fromPlaylist == true && index == 0) || fromPlaylist == false))
                            DownloadThumbnailAsync(videoControl.pictureBox.ImageLocation, downloadPath, videoControl.titleTextBox.Text, false, true);

                        // Clean up temporary files
                        try
                        {
                            File.Delete(videoTempPath);
                            File.Delete(audioTempPath);
                        }
                        catch
                        {
                            MessageBox.Show("Couldn't delete the temporary files.");
                        }
                    }
                }
            }
            MessageBox.Show("Download finished!", "YoutubeDownloader");
        }

        async void downloadAudioOnly(string URL, string downloadPath)
        {
            bool playlistIsAlbum = false;
            bool itemIsAlbum = false;
            int itemsChecked = 0;
            string artist = "",
                    album = "",
                    year = "",
                    genre = "";

            progressBar.Value = 0;
            var progress = new Progress<double>(percent =>
            {
                // Update the progress bar value
                progressBar.Value = (int)percent;
            });

            if (videoControlReferences.Count > 1)
                foreach (var videoControl in videoControlReferences)
                {
                    var (checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox, url) = videoControl;
                    if (checkBox.Checked)
                        itemsChecked++;
                }

            if (videoControlReferences.Count > 1 && itemsChecked >= 1)
            {
                DialogResult result = MessageBox.Show("Do the tracks belong to an album?", "Album Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    playlistIsAlbum = true;

                    (artist, album, year, genre) = await getTagsFromURL(URL);

                    using (var tagEditor = new TagEditorForm(artist, album, year, genre))
                    {
                        if (tagEditor.ShowDialog() == DialogResult.OK)
                        {
                            // Get the updated values
                            artist = tagEditor.Artist;
                            album = tagEditor.Album;
                            year = tagEditor.Year;
                            genre = tagEditor.Genre;
                        }
                    }

                    string albumFolderPath = CreateAlbumFolders(downloadPath, artist, album, year);

                    if (albumFolderPath != null)
                        downloadPath = albumFolderPath;
                }

                if (itemsChecked == 0)
                {
                    MessageBox.Show("Please check at least one item.");
                }
            }

            foreach (var (videoControl, index) in videoControlReferences.Select((value, index) => (value, index)))
            {
                itemIsAlbum = false;
                var (checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox, url) = videoControl;

                // Check if the CheckBox is checked
                if (checkBox.Checked)
                {
                    var videoId = YoutubeExplode.Videos.VideoId.Parse(url);
                    var streamManifest = await ytClient.Videos.Streams.GetManifestAsync(videoId);
                    string filename_raw = titleTextBox.Text;
                    string filename = new string(filename_raw.Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray());
                    string trackTitle = filename;
                    if (noLabel.Text.Length <= 1)
                        filename = "0" + noLabel.Text + ". " + filename;
                    else
                        filename = noLabel.Text + ". " + filename;

                    var filePath = Path.Combine(downloadPath, filename);
                    var outputFilePath = filePath + ".mp3";

                    var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                    if (audioStreamInfo != null)
                    {
                        var audioTempPath = Path.Combine(downloadPath, $"audio.{audioStreamInfo.Container.Name}");
                        var audioStream = await ytClient.Videos.Streams.GetAsync(streamInfo: audioStreamInfo);

                        try
                        {
                            showProgressBarAndOthers(true, $"  I: Audio Download for:\n {getTruncatedIndexTitleString(index, videoControl.titleTextBox.Text)}");
                            using (var fileStream = new FileStream(audioTempPath, FileMode.Create, FileAccess.Write))
                            {

                                await CopyToAsyncWithProgress(audioStream, fileStream, progress, cancellationTokenSource.Token);
                            }
                            showProgressBarAndOthers(false, "");
                        }
                        catch (OperationCanceledException)
                        {
                            MessageBox.Show("Download was cancelled.");
                            showProgressBarAndOthers(false, "");
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred when downloading video: {ex.Message}");
                            showProgressBarAndOthers(false, "");
                            return;
                        }

                        showProgressBarAndOthers(true, $"  II: Convert to mp3:\n {getTruncatedIndexTitleString(index, videoControl.titleTextBox.Text)}");

                        await CombineAudioAndVideo("", audioTempPath, outputFilePath, false);

                        showProgressBarAndOthers(false, "");

                        if (playlistIsAlbum)
                        {
                            await SetMp3Tags(outputFilePath, noLabel.Text, artist, trackTitle, album, year, genre);

                        }

                        var videoDetails = await ytClient.Videos.GetAsync(url);

                        if (videoDetails.Duration.Value.Minutes >= 15 || downloadParts == true)
                        {
                            DialogResult result = DialogResult.No;

                            if (videoDetails.Duration.Value.Minutes >= 15 && downloadParts == false)
                                result = MessageBox.Show("This item is pretty long, it seems to be an album. Search for song names and times in the description?", "Album Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes || downloadParts == true)
                            {
                                bool keepBigFile = false;
                                itemIsAlbum = true;
                                (artist, album, year, genre) = await getTagsFromURL(url);

                                using (var tagEditor = new TagEditorForm(artist, album, year, genre))
                                {
                                    if (tagEditor.ShowDialog() == DialogResult.OK)
                                    {
                                        // Get the updated values
                                        artist = tagEditor.Artist;
                                        album = tagEditor.Album;
                                        year = tagEditor.Year;
                                        genre = tagEditor.Genre;
                                    }
                                }

                                string albumFolderPath = CreateAlbumFolders(downloadPath, artist, album, year);

                                if (albumFolderPath != null)
                                    downloadPath = albumFolderPath;

                                TimeSpan duration = videoDetails.Duration.Value;

                                var segments = getPartsFromDescription(videoDetails.Description, artist, duration);

                                using (var form = new SplitterForm(segments, false, duration))
                                {
                                    if (form.ShowDialog() == DialogResult.OK)
                                    {
                                        segments = form.SongSegments;
                                        keepBigFile = form.keepInitialBigFile;
                                    }
                                }

                                showProgressBarAndOthers(false, "");

                                List<string> songPathList = await spliFileIntoSegments(outputFilePath, segments, downloadPath, false, progress);
                                //function definition: spliFileIntoSegments(string toSplitMP3FilePath, List<SongSegment> segments, string splitOutputDirectory, bool audioOnly)

                                Thread.Sleep(500); //delay for the writing of the file to complete
                                int index2 = 0;
                                foreach (string songPath in songPathList)
                                {
                                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Path.GetFileName(songPath));
                                    string title = fileNameWithoutExtension.Substring(fileNameWithoutExtension.IndexOf(' ') + 1);
                                    string segmentArtist = segments[index2].Artist;
                                    index2++;

                                    showProgressBarAndOthers(true, $"  IV: Setting tags for \n {getTruncatedIndexTitleString(index2, fileNameWithoutExtension)}");

                                    UpdateProgress((int)(index2 * 100 / segments.Count));

                                    await SetMp3Tags(songPath, index2.ToString(), segmentArtist, title, album, year, genre);
                                    Thread.Sleep(200); //added this to see the splitting message. ~2 seconds per album isn't much

                                    showProgressBarAndOthers(false, "");
                                }

                                if (!keepBigFile)
                                    try { File.Delete(outputFilePath); } catch { }
                            }
                        }

                        if (downloadThumbnail == true && ((fromPlaylist == true && index == 0) || fromPlaylist == false))
                            DownloadThumbnailAsync(videoControl.pictureBox.ImageLocation, downloadPath, videoControl.titleTextBox.Text, itemIsAlbum, false);
                        // Clean up temporary files
                        try
                        {
                            File.Delete(audioTempPath);
                        }
                        catch
                        {
                            MessageBox.Show("Couldn't delete the temporary files.");
                        }
                    }
                }
            }
            MessageBox.Show("Download finished!", "YoutubeDownloader");
        }

        //Buttons
        private async void fetchButton_Click(object sender, EventArgs e)
        {
            showClearTitleOf(false);
            var videoUrl = youtubeURLTextBox.Text;
            lastFetchedVideoUrl = videoUrl;
            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                MessageBox.Show("Please enter the Youtube URL before proceeding.", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Clear the panel
            ClearPanel();

            string originalText = fetchButton.Text;
            fetchButton.Text = "Fetching";
            fetchButton.Enabled = false;
            downloadButton.Enabled = false;

            //Check the link or the internet connection
            bool isSingleValidURL = false;
            bool isPlayListValidURL = false;

            try { isSingleValidURL = await ytClient.Videos.GetAsync(videoUrl) != null; } catch { }
            try { isPlayListValidURL = await ytClient.Playlists.GetAsync(videoUrl) != null; } catch { }

            if (!(isSingleValidURL || isPlayListValidURL))
            {
                MessageBox.Show("Problems with this link. Check the URL or the internet connection", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fetchButton.Text = originalText;
                fetchButton.Enabled = true;
                downloadButton.Enabled = true;
                return;
            }

            // Update the panel with new video data
            UpdatePanel(await getPanelVideosDetailsAsync(videoUrl));

            fetchButton.Text = originalText;
            fetchButton.Enabled = true;
            downloadButton.Enabled = true;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            // Create a new instance of FolderBrowserDialog
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // Show the FolderBrowserDialog and check if the user clicked OK
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected folder path and assign it to the TextBox
                    txtFolderPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            string originalText = downloadButton.Text;
            downloadButton.Text = "DOWNLOADING";
            downloadButton.Enabled = false;

            var fetchedVideoDetails = GetFetchedVideoDetails();

            var videoUrl = lastFetchedVideoUrl;

            var downloadPath = txtFolderPath.Text;
            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                MessageBox.Show("Please enter the Youtube URL and fetch its details before proceeding.", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                downloadButton.Enabled = true;
                downloadButton.Text = "DOWNLOAD";
                return;
            }

            if (string.IsNullOrWhiteSpace(downloadPath))
            {
                MessageBox.Show("Please select download path before proceeding.", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                downloadButton.Enabled = true;
                downloadButton.Text = "DOWNLOAD";
                return;
            }
            if (videoAndAudioButton.Checked)
            {
                downloadAudioVideo(videoUrl, downloadPath);
            }
            else if (audioOnlyButton.Checked)
            {
                downloadAudioOnly(videoUrl, downloadPath);
            }
            else
            {
                MessageBox.Show("Please select a download option before proceeding.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            downloadButton.Text = originalText;
            downloadButton.Enabled = true;
        }

        private void buttonClearText_Click(object sender, EventArgs e)
        {
            string searchString = textBoxSearch.Text;
            if (textBoxSearch.Text == "")
                return;

            foreach (Control videoPanel in scrollablePanel.Controls)
            {
                if (videoPanel is Panel)
                {
                    foreach (Control control in videoPanel.Controls)
                    {
                        if (control is TextBox titleTextBox)
                        {
                            titleTextBox.Text = titleTextBox.Text.Replace(searchString, string.Empty);
                        }
                    }
                }
            }
        }

        private void audioOnlyButton_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = audioOnlyButton.Checked;
            downloadThumbnail = audioOnlyButton.Checked;
            panelAudioOnly.Visible = audioOnlyButton.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //downloadParts
        {
            if (checkBox1.Checked)
                downloadParts = true;
            else
                downloadParts = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) //downloadThumbnails
        {
            if (checkBox2.Checked)
                downloadThumbnail = true;
            else
                downloadThumbnail = false;
        }

        private void openPathButton_Click(object sender, EventArgs e)
        {
            string path = txtFolderPath.Text;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Please enter a valid folder path.", "Valid Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
