using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        private readonly YoutubeClient ytClient;
        private string selectedResolution;

        public Form1()
        {
            InitializeComponent();
            ytClient = new YoutubeClient();
        }

        static void downloadAudioVideo(string URL, string downloadPath)
        {


        }

        static void downloadAudioOnly(string URL, string downloadPath)
        {

        }

        private async void getDetailsButton_Click(object sender, EventArgs e)
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

                var streamManifest = await ytClient.Videos.Streams.GetManifestAsync(video.Id);
                var streamInfos = streamManifest.GetMuxedStreams();

                listResolutions.Items.Clear();
                /*foreach (var streamInfo in streamInfos)
                {
                    listResolutions.Items.Add($"{streamInfo.VideoQualityLabel} - {streamInfo.Container.Name}");
                }
                */
                if (listResolutions.Items.Count > 0)
                {
                    listResolutions.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No available qualities found.");
                }
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
