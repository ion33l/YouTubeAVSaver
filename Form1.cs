using AngleSharp.Dom;

using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using System.Diagnostics;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        public YoutubeClient ytClient;
        private List<(bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution, string[] Sizes, string url)> videoControlValues =
                new List<(bool, int, string, string, string[], string, string[], string)>();
        private List<(CheckBox checkBox, Label noLabel, TextBox titleTextBox, PictureBox pictureBox, ComboBox resolutionComboBox, string url)> videoControlReferences =
            new List<(CheckBox, Label, TextBox, PictureBox, ComboBox, string)>();
        public Form1()
        {
            InitializeComponent();
            ytClient = new YoutubeClient();
            panelAudioOnly.Visible = false;
            progressBar.Visible = false;
            ffmpegConvertBar.Visible = false;
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

        private (bool Check, int No, string Title, string Thumbnail, string SelectedResolution)[] GetFetchedVideoDetails()
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

        async Task<(bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution, string[] Sizes, string url)[]> getPanelVideosDetailsAsync(string url)
        {
            if (url.Contains("playlist"))
            {
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
                                            .Where(s => s.Container == YoutubeExplode.Videos.Streams.Container.Mp4)
                                            .OrderByDescending(s => s.VideoQuality)
                                            .DistinctBy(s => s.VideoQuality.Label, StringComparer.OrdinalIgnoreCase)
                                            .Select(s => new {
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

                    videoControlValues.Add((true, i + 1, playlistVideos[i].Title, playlistVideos[i].Thumbnails[0].Url, resolutions.ToArray(), resolutions.ToArray()[0], sizes.ToArray(), playlistVideos[i].Url));
                }
                progressBar.Visible = false;
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
                                        .Where(s => s.Container == YoutubeExplode.Videos.Streams.Container.Mp4)
                                        .OrderByDescending(s => s.VideoQuality)
                                        .DistinctBy(s => s.VideoQuality.Label, StringComparer.OrdinalIgnoreCase)
                                        .Select(s => new {
                                            Resolution = s.VideoQuality.Label,
                                            videoSize = ((s.Size.Bytes + audioStreamInfo.audioSize)/ (1024.0 * 1024.0)).ToString("0.##") + " MB"
                                        })
                                        .ToList();

                string[] resolutions = resolutionSizeList.Select(rs => rs.Resolution).ToArray();
                string[] sizes = resolutionSizeList.Select(rs => rs.videoSize).ToArray();
                for (int j = 0; j < sizes.Length; j++)
                {
                    if (resolutions[j] == null)
                        sizes[j] = "N/A";
                }

                videoControlValues.Add((true, 1, video.Title, video.Thumbnails[0].Url, resolutions.ToArray(), resolutions.ToArray()[0], sizes.ToArray(), url));
            }

            return videoControlValues.ToArray();
        }

        private void audioOnlyButton_CheckedChanged(object sender, EventArgs e)
        {
            panelAudioOnly.Visible = audioOnlyButton.Checked;
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

            for (int i = 0; i < videos.Length; i++)
            {
                var video = videos[i];

                // Choose the background color based on the index
                Color backgroundColor = (i % 2 == 0) ? evenColor : oddColor;

                // Container Panel for each video entry
                Panel videoPanel = new Panel
                {
                    Location = new Point(10, i * 80 + 10),
                    Size = new Size(523, 80),
                    BackColor = backgroundColor
                };
                scrollablePanel.Controls.Add(videoPanel);

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
                    AutoSize = true,
                    BackColor = backgroundColor
                };
                videoPanel.Controls.Add(noLabel);

                // Title (TextBox)
                TextBox titleTextBox = new TextBox
                {
                    Location = new Point(50, 25),
                    Width = 250,
                    Text = video.Title,
                    BackColor = white
                };
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
                videoPanel.Controls.Add(pictureBox);

                // Resolution (ComboBox)
                ComboBox resolutionComboBox = new ComboBox
                {
                    Location = new Point(400, 25),
                    Width = 55,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BackColor = white
                };

                // Add unique resolution options for each video
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
                    Location = new Point(460, 27),
                    Text = correspondingSize,
                    AutoSize = true,
                    BackColor = backgroundColor
                };

                // Update the Label initially
                UpdateSizeLabel(resolutionComboBox.SelectedIndex, video.Sizes, sizeLabel);


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

                // Store control references
                videoControlReferences.Add((checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox, url));
            }
        }
        void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null) return;

            // Example of parsing the time value from the ffmpeg output
            // The actual output string format may vary
            string output = e.Data;
            if (output.Contains("time="))
            {
                ffmpegConvertBar.Visible = true;
                // Parse the 'time=' field, example: "time=00:01:23.45"
                string timeString = output.Substring(output.IndexOf("time=") + 5, 11);
                TimeSpan currentTime = TimeSpan.Parse(timeString);

                // Assuming you know the total duration (for example, 2 minutes = 120 seconds)
                TimeSpan totalDuration = new TimeSpan(0, 2, 0);

                // Calculate the progress percentage
                int progressPercentage = (int)(currentTime.TotalSeconds / totalDuration.TotalSeconds * 100);

                // Update the progress bar (must be done on the UI thread)
                ffmpegConvertBar.Invoke(new Action(() => ffmpegConvertBar.Value = progressPercentage));
                //ffmpegConvertBar.Invoke((MethodInvoker)(() => ffmpegConvertBar.Value = progressPercentage));
                ffmpegConvertBar.Visible = false;
            }
        }

        private Task CombineAudioAndVideo(string videoPath, string audioPath, string outputPath, bool audioAndVideo)
        {
            return Task.Run(() =>
            {
                //var ffmpegPath = @"C:\Program Files\ffmpeg-master-latest-win64-gpl\bin\ffmpeg.exe"; 
                var ffmpegPath = Path.Combine(Application.StartupPath, "Resources", "FFmpeg", "ffmpeg.exe");
                string arguments;
                if (audioAndVideo) 
                    arguments = $"-y -i \"{videoPath}\" -i \"{audioPath}\" -c copy \"{outputPath}\"";
                else
                    arguments = $"-y -i \"{audioPath}\" -vn -c:a libmp3lame \"{outputPath}\"";
                
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
                    process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                    process.ErrorDataReceived += (sender, e) => Console.WriteLine("ERROR: " + e.Data);
                    //process.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceived);
                    //process.ErrorDataReceived += OnDataReceived;
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                }
            });
        }

        async void downloadAudioVideo(string URL, string downloadPath)
        {
            foreach (var videoControl in videoControlReferences)
            {
                var (checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox, url) = videoControl;
              
                    // Check if the CheckBox is checked
                if (checkBox.Checked)
                {
                    progressBar.Visible = true;

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
                        var videoTempPath =  Path.Combine(downloadPath, $"video.{videoStreamInfo.Container.Name}");
                        var audioTempPath =  Path.Combine(downloadPath, $"audio.{audioStreamInfo.Container.Name}");

                        var videoStream = await ytClient.Videos.Streams.GetAsync(streamInfo: videoStreamInfo);
                        var audioStream = await ytClient.Videos.Streams.GetAsync(streamInfo: audioStreamInfo);

                        using (var fileStream = new FileStream(videoTempPath, FileMode.Create, FileAccess.Write))
                        {
                            await videoStream.CopyToAsync(fileStream);
                        }
                        using (var fileStream = new FileStream(audioTempPath, FileMode.Create, FileAccess.Write))
                        {
                            await audioStream.CopyToAsync(fileStream);
                        }

                        await CombineAudioAndVideo(videoTempPath, audioTempPath, outputFilePath, true);

                        progressBar.Visible = false;
                        // Clean up temporary files
                        //File.Delete(videoTempPath);
                        //File.Delete(audioTempPath);
                    }
                }
            }
        }

        async void downloadAudioOnly(string URL, string downloadPath)
        {
            foreach (var videoControl in videoControlReferences)
            {
                var (checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox, url) = videoControl;

                // Check if the CheckBox is checked
                if (checkBox.Checked)
                {
                    var videoId = YoutubeExplode.Videos.VideoId.Parse(url);
                    var streamManifest = await ytClient.Videos.Streams.GetManifestAsync(videoId);
                    string filename_raw = titleTextBox.Text;
                    string filename = new string(filename_raw.Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray());
                    var filePath = Path.Combine(downloadPath, filename);
                    var outputFilePath = filePath + ".mp3";

                    var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                    if (audioStreamInfo != null) {
                        var audioTempPath = Path.Combine(downloadPath, $"audio.{audioStreamInfo.Container.Name}");
                        var audioStream = await ytClient.Videos.Streams.GetAsync(streamInfo: audioStreamInfo);

                        using (var fileStream = new FileStream(audioTempPath, FileMode.Create, FileAccess.Write))
                        {
                            await audioStream.CopyToAsync(fileStream);
                        }
                        await CombineAudioAndVideo("", audioTempPath, outputFilePath, false);
                    }

                    // Clean up temporary files
                    //File.Delete(audioTempPath);
                }
            }
        }

        private async void fetchButton_Click(object sender, EventArgs e)
        {
            var videoUrl = youtubeURLTextBox.Text;
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

        private void downloadButton_Click(object sender, EventArgs e)
        {
            string originalText = downloadButton.Text;
            downloadButton.Text = "DOWNLOADING";
            downloadButton.Enabled = false;

            var fetchedVideoDetails = GetFetchedVideoDetails();

            var videoUrl = youtubeURLTextBox.Text;
            var downloadPath = txtFolderPath.Text;
            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                MessageBox.Show("Please enter the Youtube URL before proceeding.", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                //MessageBox.Show("Video download initiated.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (audioOnlyButton.Checked)
            {
                downloadAudioOnly(videoUrl, downloadPath);

                //MessageBox.Show("Audio download initiated.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a download option before proceeding.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            downloadButton.Text = originalText;
            downloadButton.Enabled = true;

        }
    }
}
