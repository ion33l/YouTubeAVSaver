namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        static void downloadAudioVideo(string URL, string downloadPath)
        {

        }
        static void downloadAudioOnly(string URL, string downloadPath)
        {

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            if (txtFolderPath.Text == "")
            {
                MessageBox.Show("Please select download path before proceeding.", "Download Path Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (videoAndAudioButton.Checked)
            {
                downloadAudioVideo(youtubeURLTextBox.Text, txtFolderPath.Text);

                MessageBox.Show("Video download initiated.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (audioOnlyButton.Checked)
            {
                downloadAudioOnly(youtubeURLTextBox.Text, txtFolderPath.Text);
                MessageBox.Show("Audio download initiated.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a download option before proceeding.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
