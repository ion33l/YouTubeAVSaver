using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        private readonly YoutubeClient ytClient;

        public Form1()
        {
            InitializeComponent();
            ytClient = new YoutubeClient();
            //this.Controls.Add(scrollablePanel);

            /*Panel scrollablePanel = new Panel
            {
                AutoScroll = true,
                Width = 500,
                Height = 200,
                BackColor = Color.White,
                Location = new Point(45, 226)
            };
            this.Controls.Add(scrollablePanel);*/

        }

        private void ClearPanel()
        {
            scrollablePanel.Controls.Clear();
        }

        private void UpdatePanel((bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution)[] videos)
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
            }
        }

        static void downloadAudioVideo(string URL, string downloadPath)
        {


        }

        static void downloadAudioOnly(string URL, string downloadPath)
        {

        }

        private async void fetchButton_Click(object sender, EventArgs e)
        {
            var videoUrl = youtubeURLTextBox.Text;
            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                MessageBox.Show("Please enter the Youtube URL before proceeding.", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var video = await ytClient.Videos.GetAsync(videoUrl);
                //listResolutions.Text = $"Video Title: {video.Title}";
                //MessageBox.Show(listResolutions.Text);
                /*
                var streamManifest = await ytClient.Videos.Streams.GetManifestAsync(video.Id);
                var streamInfos = streamManifest.GetMuxedStreams();

                listResolutions.Items.Clear();
                foreach (var streamInfo in streamInfos)
                {
                    listResolutions.Items.Add($"{streamInfo.VideoQualityLabel} - {streamInfo.Container.Name}");
                }
                
                if (listResolutions.Items.Count > 0)
                {
                    listResolutions.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No available qualities found.");
                }*/

                // Example video data
                var videos = new (bool Check, int No, string Title, string Thumbnail, string[] Resolutions, string SelectedResolution)[]
                {
                    (true, 1, "Video 1", "path_to_thumbnail1.jpg", new string[] { "480p", "720p", "1080p" }, "720p"),
                    //(false, 2, "Video 2", "path_to_thumbnail2.jpg", new string[] { "720p", "1080p", "1440p" }, "1080p"),
                    //(true, 3, "Video 3", "path_to_thumbnail3.jpg", new string[] { "480p", "1080p", "4K" }, "1440p"),
                    //(false, 4, "Video 4", "path_to_thumbnail2.jpg", new string[] { "720p", "1080p", "1440p" }, "1080p"),
                    //(false, 5, "Video 5", "path_to_thumbnail2.jpg", new string[] { "720p", "1080p", "1440p" }, "1080p"),
                    //(false, 6, "Video 6", "path_to_thumbnail2.jpg", new string[] { "720p", "1080p", "1440p" }, "1080p"),

                };

                // Clear the panel
                ClearPanel();

                // Update the panel with new video data
                UpdatePanel(videos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching video qualities: {ex.Message}");
            }
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
        }
    }
}
