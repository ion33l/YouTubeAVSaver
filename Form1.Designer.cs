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
            label10 = new Label();
            textBoxSearch = new TextBox();
            buttonClearText = new Button();
            label15 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            panelAudioOnly.SuspendLayout();
            SuspendLayout();
            // 
            // youtubeURLTextBox
            // 
            youtubeURLTextBox.Location = new Point(156, 42);
            youtubeURLTextBox.Margin = new Padding(3, 4, 3, 4);
            youtubeURLTextBox.Name = "youtubeURLTextBox";
            youtubeURLTextBox.Size = new Size(450, 27);
            youtubeURLTextBox.TabIndex = 0;
            youtubeURLTextBox.KeyDown += youtubeURLTextBox_KeyDown;
            // 
            // downloadButton
            // 
            downloadButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            downloadButton.Location = new Point(324, 728);
            downloadButton.Margin = new Padding(3, 4, 3, 4);
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new Size(137, 53);
            downloadButton.TabIndex = 1;
            downloadButton.Text = "DOWNLOAD";
            downloadButton.UseVisualStyleBackColor = true;
            downloadButton.Click += downloadButton_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(156, 165);
            txtFolderPath.Margin = new Padding(3, 4, 3, 4);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(450, 27);
            txtFolderPath.TabIndex = 2;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(624, 164);
            browseButton.Margin = new Padding(3, 4, 3, 4);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(86, 31);
            browseButton.TabIndex = 3;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cancelButton.Location = new Point(624, 3);
            cancelButton.Margin = new Padding(3, 4, 3, 4);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(86, 31);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // videoAndAudioButton
            // 
            videoAndAudioButton.AutoSize = true;
            videoAndAudioButton.Checked = true;
            videoAndAudioButton.Location = new Point(174, 105);
            videoAndAudioButton.Margin = new Padding(3, 4, 3, 4);
            videoAndAudioButton.Name = "videoAndAudioButton";
            videoAndAudioButton.Size = new Size(142, 24);
            videoAndAudioButton.TabIndex = 4;
            videoAndAudioButton.TabStop = true;
            videoAndAudioButton.Text = "Video and Audio";
            videoAndAudioButton.UseVisualStyleBackColor = true;
            // 
            // audioOnlyButton
            // 
            audioOnlyButton.AutoSize = true;
            audioOnlyButton.Location = new Point(313, 105);
            audioOnlyButton.Margin = new Padding(3, 4, 3, 4);
            audioOnlyButton.Name = "audioOnlyButton";
            audioOnlyButton.Size = new Size(104, 24);
            audioOnlyButton.TabIndex = 5;
            audioOnlyButton.Text = "Audio Only";
            audioOnlyButton.UseVisualStyleBackColor = true;
            audioOnlyButton.CheckedChanged += audioOnlyButton_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(25, 46);
            label1.Name = "label1";
            label1.Size = new Size(104, 20);
            label1.TabIndex = 6;
            label1.Text = "Youtube URL:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(25, 173);
            label2.Name = "label2";
            label2.Size = new Size(120, 20);
            label2.TabIndex = 7;
            label2.Text = "Download Path:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(510, 269);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 9;
            label3.Text = "Resolution";
            // 
            // label4
            // 
            label4.BackColor = Color.Black;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(25, 85);
            label4.Name = "label4";
            label4.Size = new Size(685, 1);
            label4.TabIndex = 11;
            label4.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(25, 105);
            label5.Name = "label5";
            label5.Size = new Size(121, 20);
            label5.TabIndex = 12;
            label5.Text = "Download Type:";
            // 
            // label6
            // 
            label6.BackColor = Color.Black;
            label6.BorderStyle = BorderStyle.Fixed3D;
            label6.Location = new Point(25, 147);
            label6.Name = "label6";
            label6.Size = new Size(685, 1);
            label6.TabIndex = 13;
            label6.Text = "label6";
            // 
            // label7
            // 
            label7.BackColor = Color.Black;
            label7.BorderStyle = BorderStyle.Fixed3D;
            label7.Location = new Point(25, 221);
            label7.Name = "label7";
            label7.Size = new Size(685, 1);
            label7.TabIndex = 14;
            label7.Text = "label7";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(275, 235);
            label8.Name = "label8";
            label8.Size = new Size(191, 20);
            label8.TabIndex = 15;
            label8.Text = "VIDEO(S) TO DOWNLOAD";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(32, 269);
            label9.Name = "label9";
            label9.Size = new Size(34, 20);
            label9.TabIndex = 10;
            label9.Text = "Get";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(96, 269);
            label11.Name = "label11";
            label11.Size = new Size(40, 20);
            label11.TabIndex = 12;
            label11.Text = "Title";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(63, 269);
            label12.Name = "label12";
            label12.Size = new Size(34, 20);
            label12.TabIndex = 16;
            label12.Text = "No.";
            // 
            // scrollablePanel
            // 
            scrollablePanel.AutoScroll = true;
            scrollablePanel.BackColor = SystemColors.Control;
            scrollablePanel.Location = new Point(24, 289);
            scrollablePanel.Margin = new Padding(3, 4, 3, 4);
            scrollablePanel.Name = "scrollablePanel";
            scrollablePanel.Size = new Size(685, 400);
            scrollablePanel.TabIndex = 17;
            // 
            // panelAudioOnly
            // 
            panelAudioOnly.Controls.Add(label14);
            panelAudioOnly.Location = new Point(505, 268);
            panelAudioOnly.Margin = new Padding(3, 4, 3, 4);
            panelAudioOnly.Name = "panelAudioOnly";
            panelAudioOnly.Size = new Size(184, 425);
            panelAudioOnly.TabIndex = 19;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(10, 1);
            label14.Name = "label14";
            label14.Size = new Size(140, 20);
            label14.TabIndex = 20;
            label14.Text = "Best Audio Quality";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(156, 3);
            progressBar.Margin = new Padding(3, 4, 3, 4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(450, 31);
            progressBar.TabIndex = 0;
            // 
            // fetchButton
            // 
            fetchButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fetchButton.Location = new Point(624, 42);
            fetchButton.Margin = new Padding(3, 4, 3, 4);
            fetchButton.Name = "fetchButton";
            fetchButton.Size = new Size(86, 31);
            fetchButton.TabIndex = 18;
            fetchButton.Text = "Fetch";
            fetchButton.UseVisualStyleBackColor = true;
            fetchButton.Click += fetchButton_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(600, 269);
            label13.Name = "label13";
            label13.Size = new Size(36, 20);
            label13.TabIndex = 19;
            label13.Text = "Size";
            // 
            // labelOperation
            // 
            labelOperation.AutoSize = true;
            labelOperation.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelOperation.Location = new Point(8, 12);
            labelOperation.Name = "labelOperation";
            labelOperation.Size = new Size(0, 20);
            labelOperation.TabIndex = 20;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(19, 235);
            label10.Name = "label10";
            label10.Size = new Size(100, 20);
            label10.TabIndex = 21;
            label10.Text = "Clear title of:";
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(115, 231);
            textBoxSearch.Margin = new Padding(3, 4, 3, 4);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(85, 27);
            textBoxSearch.TabIndex = 22;
            // 
            // buttonClearText
            // 
            buttonClearText.Location = new Point(206, 229);
            buttonClearText.Margin = new Padding(3, 4, 3, 4);
            buttonClearText.Name = "buttonClearText";
            buttonClearText.Size = new Size(57, 31);
            buttonClearText.TabIndex = 23;
            buttonClearText.Text = "Clear";
            buttonClearText.UseVisualStyleBackColor = true;
            buttonClearText.Click += buttonClearText_Click;
            // 
            // label15
            // 
            label15.BackColor = Color.Black;
            label15.BorderStyle = BorderStyle.Fixed3D;
            label15.Location = new Point(415, 92);
            label15.Name = "label15";
            label15.Size = new Size(1, 53);
            label15.TabIndex = 24;
            label15.Text = "label15";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(424, 92);
            checkBox1.Margin = new Padding(3, 4, 3, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(288, 24);
            checkBox1.TabIndex = 25;
            checkBox1.Text = "Download parts/chapters? (start - end)";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(424, 120);
            checkBox2.Margin = new Padding(3, 4, 3, 4);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(258, 24);
            checkBox2.TabIndex = 26;
            checkBox2.Text = "Download thumnail (cover image)";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 803);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label15);
            Controls.Add(buttonClearText);
            Controls.Add(textBoxSearch);
            Controls.Add(label10);
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
            Margin = new Padding(3, 4, 3, 4);
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
        private Label label10;
        private TextBox textBoxSearch;
        private Button buttonClearText;
        private Label label15;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
    }
}
