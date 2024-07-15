namespace YoutubeDownloader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            youtubeURLTextBox = new TextBox();
            downloadButton = new Button();
            txtFolderPath = new TextBox();
            browseButton = new Button();
            videoAndAudioButton = new RadioButton();
            audioOnlyButton = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // youtubeURLTextBox
            // 
            youtubeURLTextBox.Location = new Point(136, 83);
            youtubeURLTextBox.Name = "youtubeURLTextBox";
            youtubeURLTextBox.Size = new Size(459, 23);
            youtubeURLTextBox.TabIndex = 0;
            // 
            // downloadButton
            // 
            downloadButton.Location = new Point(601, 83);
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new Size(75, 23);
            downloadButton.TabIndex = 1;
            downloadButton.Text = "Download";
            downloadButton.UseVisualStyleBackColor = true;
            downloadButton.Click += downloadButton_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(134, 156);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(461, 23);
            txtFolderPath.TabIndex = 2;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(601, 157);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 3;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // videoAndAudioButton
            // 
            videoAndAudioButton.AutoSize = true;
            videoAndAudioButton.Location = new Point(682, 72);
            videoAndAudioButton.Name = "videoAndAudioButton";
            videoAndAudioButton.Size = new Size(113, 19);
            videoAndAudioButton.TabIndex = 4;
            videoAndAudioButton.TabStop = true;
            videoAndAudioButton.Text = "Video and Audio";
            videoAndAudioButton.UseVisualStyleBackColor = true;
            // 
            // audioOnlyButton
            // 
            audioOnlyButton.AutoSize = true;
            audioOnlyButton.Location = new Point(682, 97);
            audioOnlyButton.Name = "audioOnlyButton";
            audioOnlyButton.Size = new Size(85, 19);
            audioOnlyButton.TabIndex = 5;
            audioOnlyButton.TabStop = true;
            audioOnlyButton.Text = "Audio Only";
            audioOnlyButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(24, 86);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 6;
            label1.Text = "Youtube URL:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(24, 159);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 7;
            label2.Text = "Download path:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 315);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(audioOnlyButton);
            Controls.Add(videoAndAudioButton);
            Controls.Add(browseButton);
            Controls.Add(txtFolderPath);
            Controls.Add(downloadButton);
            Controls.Add(youtubeURLTextBox);
            Name = "YoutubeDownloader";
            Text = "YoutubeDownloader";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox youtubeURLTextBox;
        private Button downloadButton;
        private TextBox txtFolderPath;
        private Button browseButton;
        private RadioButton videoAndAudioButton;
        private RadioButton audioOnlyButton;
        private Label label1;
        private Label label2;
    }
}
