using AngleSharp.Dom;
using System.Windows.Forms;
using VideoLibrary;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        public YoutubeClient ytClient;
        private List<(bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution)> videoControlValues =
                new List<(bool, int, string, string, string[], string)>();
        private List<(CheckBox checkBox, Label noLabel, TextBox titleTextBox, PictureBox pictureBox, ComboBox resolutionComboBox)> videoControlReferences =
            new List<(CheckBox, Label, TextBox, PictureBox, ComboBox)>();
        public Form1()
        {
            InitializeComponent();
            ytClient = new YoutubeClient();
            panelAudioOnly.Visible = false;
            progressBar.Visible = false;
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

            foreach (var (checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox) in videoControlReferences)
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

        async Task<(bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution)[]> getPanelVideosDetailsAsync(string url)
        {
            //TODO - try not to repeat that declarations
            //List<YoutubeExplode.Playlists.PlaylistVideo> videosToProcess = new List<YoutubeExplode.Playlists.PlaylistVideo>();

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
                    var resolutions = videoStreams
                        .Select(s => s.VideoQuality.Label)
                        .Distinct();
                    //.OrderBy(r => r);
 
                    videoControlValues.Add((true, i + 1, playlistVideos[i].Title, playlistVideos[i].Thumbnails[0].Url, resolutions.ToArray(), resolutions.ToArray()[0]));                   
                }
                progressBar.Visible = false;
            }
            else
            {
                var video = await ytClient.Videos.GetAsync(url);
                var videoId = YoutubeExplode.Videos.VideoId.Parse(url);
                var streamManifest = await ytClient.Videos.Streams.GetManifestAsync(videoId);
                var videoStreams = streamManifest.GetVideoStreams();
                var resolutions = videoStreams
                    .Select(s => s.VideoQuality.Label)
                    .Distinct();
                //.OrderBy(r => r);
                videoControlValues.Add((true, 1, video.Title, video.Thumbnails[0].Url, resolutions.ToArray(), resolutions.ToArray()[0]));
            }

            return videoControlValues.ToArray();
        }

        private void audioOnlyButton_CheckedChanged(object sender, EventArgs e)
        {
            panelAudioOnly.Visible = audioOnlyButton.Checked;
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

        private void UpdatePanel((bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution)[] videos)
        {
            Color oddColor = Color.FromArgb(250, 250, 250); // Light grey for odd entries
            Color evenColor = Color.FromArgb(220, 220, 220); // Slightly darker grey for even entries
            Color white = Color.White;

            for (int i = 0; i < videos.Length; i++)
            {
                var video = videos[i];
                //Image thumbnailImage = await DownloadImageAsync(video.Thumbnail);

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
                    Width = 300,
                    Text = video.Title,
                    BackColor = white
                };
                videoPanel.Controls.Add(titleTextBox);

                // Thumbnail (PictureBox)
                PictureBox pictureBox = new PictureBox
                {
                    Location = new Point(380, 25),
                    Size = new Size(50, 50),
                    ImageLocation = video.Thumbnail, // TODO - Use actual thumbnail path
                    //Image = thumbnailImage,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = backgroundColor
                };
                videoPanel.Controls.Add(pictureBox);

                // Resolution (ComboBox)
                ComboBox resolutionComboBox = new ComboBox
                {
                    Location = new Point(460, 25),
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

                // Store control references
                videoControlReferences.Add((checkBox, noLabel, titleTextBox, pictureBox, resolutionComboBox));
            }
        }

        static void downloadAudioVideo(string URL, string downloadPath)
        {


        }

        static void downloadAudioOnly(string URL, string downloadPath)
        {  }

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
            //TODO - add a progress bar

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
            downloadButton.Text = "Downloading";
            downloadButton.Enabled = false;

            var fetchedVideoDetails = GetFetchedVideoDetails();
            foreach (var value in fetchedVideoDetails)
            {
                MessageBox.Show($"Check: {value.Check}, No: {value.No}, Title: {value.Title}, Thumbnail: {value.Thumbnail}, Resolution: {value.SelectedResolution}");
            }

            var videoUrl = youtubeURLTextBox.Text;
            var downloadPath = txtFolderPath.Text;
            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                MessageBox.Show("Please enter the Youtube URL before proceeding.", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(downloadPath))
            {
                MessageBox.Show("Please select download path before proceeding.", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (videoAndAudioButton.Checked)
            {
                downloadAudioVideo(videoUrl, downloadPath);


                MessageBox.Show(scrollablePanel.ToString(), "scrollablePanel", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Video download initiated.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (audioOnlyButton.Checked)
            {
                downloadAudioOnly(videoUrl, downloadPath);
                MessageBox.Show("Audio download initiated.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
