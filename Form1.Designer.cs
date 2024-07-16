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
            listResolutions = new ListBox();
            label3 = new Label();
            getDetailsButton = new Button();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // youtubeURLTextBox
            // 
            youtubeURLTextBox.Location = new Point(120, 27);
            youtubeURLTextBox.Name = "youtubeURLTextBox";
            youtubeURLTextBox.Size = new Size(350, 23);
            youtubeURLTextBox.TabIndex = 0;
            // 
            // downloadButton
            // 
            downloadButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            downloadButton.Location = new Point(259, 466);
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new Size(98, 42);
            downloadButton.TabIndex = 1;
            downloadButton.Text = "DOWNLOAD";
            downloadButton.UseVisualStyleBackColor = true;
            downloadButton.Click += downloadButton_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(120, 124);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(350, 23);
            txtFolderPath.TabIndex = 2;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(486, 125);
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
            videoAndAudioButton.Location = new Point(198, 79);
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
            audioOnlyButton.Location = new Point(346, 79);
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
            label1.Location = new Point(22, 30);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 6;
            label1.Text = "Youtube URL:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(22, 130);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 7;
            label2.Text = "Download Path:";
            // 
            // listResolutions
            // 
            listResolutions.FormattingEnabled = true;
            listResolutions.ItemHeight = 15;
            listResolutions.Location = new Point(437, 176);
            listResolutions.Name = "listResolutions";
            listResolutions.Size = new Size(85, 19);
            listResolutions.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(357, 176);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 9;
            label3.Text = "Resolutions:";
            // 
            // getDetailsButton
            // 
            getDetailsButton.Location = new Point(486, 28);
            getDetailsButton.Name = "getDetailsButton";
            getDetailsButton.Size = new Size(75, 23);
            getDetailsButton.TabIndex = 10;
            getDetailsButton.Text = "Get details";
            getDetailsButton.UseVisualStyleBackColor = true;
            getDetailsButton.Click += getDetailsButton_Click;
            // 
            // label4
            // 
            label4.BackColor = Color.Black;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(45, 64);
            label4.Name = "label4";
            label4.Size = new Size(500, 1);
            label4.TabIndex = 11;
            label4.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(22, 79);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 12;
            label5.Text = "Download Type:";
            // 
            // label6
            // 
            label6.BackColor = Color.Black;
            label6.BorderStyle = BorderStyle.Fixed3D;
            label6.Location = new Point(45, 110);
            label6.Name = "label6";
            label6.Size = new Size(500, 1);
            label6.TabIndex = 13;
            label6.Text = "label6";
            // 
            // label7
            // 
            label7.BackColor = Color.Black;
            label7.BorderStyle = BorderStyle.Fixed3D;
            label7.Location = new Point(42, 166);
            label7.Name = "label7";
            label7.Size = new Size(500, 1);
            label7.TabIndex = 14;
            label7.Text = "label7";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(259, 176);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 15;
            label8.Text = "VIDEO(S)";
            // 
            // panel1
            // 
            panel1.Location = new Point(45, 213);
            panel1.Name = "panel1";
            panel1.Size = new Size(477, 161);
            panel1.TabIndex = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 561);
            Controls.Add(panel1);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(getDetailsButton);
            Controls.Add(label3);
            Controls.Add(listResolutions);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(audioOnlyButton);
            Controls.Add(videoAndAudioButton);
            Controls.Add(browseButton);
            Controls.Add(txtFolderPath);
            Controls.Add(downloadButton);
            Controls.Add(youtubeURLTextBox);
            Name = "Form1";
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
        private ListBox listResolutions;
        private Label label3;
        private Button getDetailsButton;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Panel panel1;
    }
}
