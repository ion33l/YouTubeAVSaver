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
            cancelButton = new Button();
            videoAndAudioButton = new RadioButton();
            audioOnlyButton = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label11 = new Label();
            label12 = new Label();
            scrollablePanel = new Panel();
            panelAudioOnly = new Panel();
            label14 = new Label();
            progressBar = new ProgressBar();
            fetchButton = new Button();
            label13 = new Label();
            labelOperation = new Label();
            labelClearTitleOf = new Label();
            textBoxSearch = new TextBox();
            buttonClearText = new Button();
            label15 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            labelPlaylist = new Label();
            openPathButton = new Button();
            panelAudioOnly.SuspendLayout();
            SuspendLayout();
            // 
            // youtubeURLTextBox
            // 
            youtubeURLTextBox.Location = new Point(136, 46);
            youtubeURLTextBox.Name = "youtubeURLTextBox";
            youtubeURLTextBox.Size = new Size(400, 23);
            youtubeURLTextBox.TabIndex = 0;
            youtubeURLTextBox.KeyDown += youtubeURLTextBox_KeyDown;
            // 
            // downloadButton
            // 
            downloadButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            downloadButton.Location = new Point(284, 570);
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new Size(120, 40);
            downloadButton.TabIndex = 1;
            downloadButton.Text = "DOWNLOAD";
            downloadButton.UseVisualStyleBackColor = true;
            downloadButton.Click += downloadButton_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(136, 138);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(400, 23);
            txtFolderPath.TabIndex = 2;
            // 
            // browseButton
            // 
            browseButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            browseButton.Location = new Point(546, 129);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 3;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cancelButton.Location = new Point(546, 20);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // videoAndAudioButton
            // 
            videoAndAudioButton.AutoSize = true;
            videoAndAudioButton.Checked = true;
            videoAndAudioButton.Location = new Point(152, 93);
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
            audioOnlyButton.Location = new Point(274, 93);
            audioOnlyButton.Name = "audioOnlyButton";
            audioOnlyButton.Size = new Size(85, 19);
            audioOnlyButton.TabIndex = 5;
            audioOnlyButton.Text = "Audio Only";
            audioOnlyButton.UseVisualStyleBackColor = true;
            audioOnlyButton.CheckedChanged += audioOnlyButton_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 43);
            label1.Name = "label1";
            label1.Size = new Size(106, 30);
            label1.TabIndex = 6;
            label1.Text = "    Youtube URL:\n (single or playlist)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(22, 144);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 7;
            label2.Text = "Download Path:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(446, 216);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 9;
            label3.Text = "Resolution";
            // 
            // label4
            // 
            label4.BackColor = Color.Black;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(22, 78);
            label4.Name = "label4";
            label4.Size = new Size(599, 1);
            label4.TabIndex = 11;
            label4.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(22, 93);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 12;
            label5.Text = "Download Type:";
            // 
            // label6
            // 
            label6.BackColor = Color.Black;
            label6.BorderStyle = BorderStyle.Fixed3D;
            label6.Location = new Point(22, 124);
            label6.Name = "label6";
            label6.Size = new Size(599, 1);
            label6.TabIndex = 13;
            label6.Text = "label6";
            // 
            // label7
            // 
            label7.BackColor = Color.Black;
            label7.BorderStyle = BorderStyle.Fixed3D;
            label7.Location = new Point(22, 180);
            label7.Name = "label7";
            label7.Size = new Size(599, 1);
            label7.TabIndex = 14;
            label7.Text = "label7";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(241, 186);
            label8.Name = "label8";
            label8.Size = new Size(151, 15);
            label8.TabIndex = 15;
            label8.Text = "VIDEO(S) TO DOWNLOAD";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(28, 216);
            label9.Name = "label9";
            label9.Size = new Size(28, 15);
            label9.TabIndex = 10;
            label9.Text = "Get";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(84, 216);
            label11.Name = "label11";
            label11.Size = new Size(32, 15);
            label11.TabIndex = 12;
            label11.Text = "Title";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(55, 216);
            label12.Name = "label12";
            label12.Size = new Size(26, 15);
            label12.TabIndex = 16;
            label12.Text = "No.";
            // 
            // scrollablePanel
            // 
            scrollablePanel.AutoScroll = true;
            scrollablePanel.BackColor = SystemColors.Control;
            scrollablePanel.Location = new Point(21, 231);
            scrollablePanel.Name = "scrollablePanel";
            scrollablePanel.Size = new Size(599, 300);
            scrollablePanel.TabIndex = 17;
            // 
            // panelAudioOnly
            // 
            panelAudioOnly.Controls.Add(label14);
            panelAudioOnly.Location = new Point(442, 215);
            panelAudioOnly.Name = "panelAudioOnly";
            panelAudioOnly.Size = new Size(161, 319);
            panelAudioOnly.TabIndex = 19;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(9, 1);
            label14.Name = "label14";
            label14.Size = new Size(109, 15);
            label14.TabIndex = 20;
            label14.Text = "Best Audio Quality";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(136, 20);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(400, 23);
            progressBar.TabIndex = 0;
            // 
            // fetchButton
            // 
            fetchButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fetchButton.Location = new Point(546, 46);
            fetchButton.Name = "fetchButton";
            fetchButton.Size = new Size(75, 23);
            fetchButton.TabIndex = 18;
            fetchButton.Text = "Fetch";
            fetchButton.UseVisualStyleBackColor = true;
            fetchButton.Click += fetchButton_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(525, 216);
            label13.Name = "label13";
            label13.Size = new Size(30, 15);
            label13.TabIndex = 19;
            label13.Text = "Size";
            // 
            // labelOperation
            // 
            labelOperation.AutoSize = true;
            labelOperation.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelOperation.Location = new Point(136, 2);
            labelOperation.Name = "labelOperation";
            labelOperation.Size = new Size(0, 15);
            labelOperation.TabIndex = 20;
            // 
            // labelClearTitleOf
            // 
            labelClearTitleOf.AutoSize = true;
            labelClearTitleOf.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelClearTitleOf.Location = new Point(17, 550);
            labelClearTitleOf.Name = "labelClearTitleOf";
            labelClearTitleOf.Size = new Size(80, 15);
            labelClearTitleOf.TabIndex = 21;
            labelClearTitleOf.Text = "Clear titles of:";
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(101, 547);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(100, 23);
            textBoxSearch.TabIndex = 22;
            // 
            // buttonClearText
            // 
            buttonClearText.Location = new Point(207, 546);
            buttonClearText.Name = "buttonClearText";
            buttonClearText.Size = new Size(50, 23);
            buttonClearText.TabIndex = 23;
            buttonClearText.Text = "Clear";
            buttonClearText.UseVisualStyleBackColor = true;
            buttonClearText.Click += buttonClearText_Click;
            // 
            // label15
            // 
            label15.BackColor = Color.Black;
            label15.BorderStyle = BorderStyle.Fixed3D;
            label15.Location = new Point(363, 83);
            label15.Name = "label15";
            label15.Size = new Size(1, 40);
            label15.TabIndex = 24;
            label15.Text = "label15";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(371, 83);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(229, 19);
            checkBox1.TabIndex = 25;
            checkBox1.Text = "Download parts/chapters? (start - end)";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(371, 104);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(207, 19);
            checkBox2.TabIndex = 26;
            checkBox2.Text = "Download thumnail (cover image)";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // labelPlaylist
            // 
            labelPlaylist.AutoSize = true;
            labelPlaylist.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPlaylist.Location = new Point(241, 201);
            labelPlaylist.Name = "labelPlaylist";
            labelPlaylist.Size = new Size(0, 15);
            labelPlaylist.TabIndex = 27;
            // 
            // openPathButton
            // 
            openPathButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            openPathButton.Location = new Point(546, 154);
            openPathButton.Name = "openPathButton";
            openPathButton.Size = new Size(74, 23);
            openPathButton.TabIndex = 28;
            openPathButton.Text = "Open Path";
            openPathButton.UseVisualStyleBackColor = true;
            openPathButton.Click += openPathButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 620);
            Controls.Add(openPathButton);
            Controls.Add(labelPlaylist);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label15);
            Controls.Add(buttonClearText);
            Controls.Add(textBoxSearch);
            Controls.Add(labelClearTitleOf);
            Controls.Add(labelOperation);
            Controls.Add(panelAudioOnly);
            Controls.Add(label13);
            Controls.Add(progressBar);
            Controls.Add(label3);
            Controls.Add(fetchButton);
            Controls.Add(scrollablePanel);
            Controls.Add(label12);
            Controls.Add(label9);
            Controls.Add(label11);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(audioOnlyButton);
            Controls.Add(videoAndAudioButton);
            Controls.Add(browseButton);
            Controls.Add(cancelButton);
            Controls.Add(txtFolderPath);
            Controls.Add(downloadButton);
            Controls.Add(youtubeURLTextBox);
            Name = "Form1";
            Text = "YoutubeDownloader";
            panelAudioOnly.ResumeLayout(false);
            panelAudioOnly.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox youtubeURLTextBox;
        private TextBox txtFolderPath;
        private Button downloadButton;
        private Button browseButton;
        private Button cancelButton;
        private RadioButton videoAndAudioButton;
        private RadioButton audioOnlyButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label11;
        private Label label12;
        private Panel scrollablePanel;
        private Panel panelAudioOnly;
        private Button fetchButton;
        private ProgressBar progressBar;
        private Label label13;
        private Label label14;
        private Label labelOperation;
        private Label labelClearTitleOf;
        private TextBox textBoxSearch;
        private Button buttonClearText;
        private Label label15;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Label labelPlaylist;
        private Button openPathButton;
    }
}
