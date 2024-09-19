namespace YouTubeAVSaver
{
    partial class TagEditorForm
    {
        private System.Windows.Forms.TextBox txtArtist;
        private System.Windows.Forms.TextBox txtAlbum;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtGenre;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.Label lblAlbum;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Button btnOk;

        private void InitializeComponent()
        {
            txtArtist = new TextBox();
            txtAlbum = new TextBox();
            txtYear = new TextBox();
            txtGenre = new TextBox();
            lblArtist = new Label();
            lblAlbum = new Label();
            lblYear = new Label();
            lblGenre = new Label();
            btnOk = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtArtist
            // 
            txtArtist.Location = new Point(114, 85);
            txtArtist.Margin = new Padding(5, 4, 5, 4);
            txtArtist.Name = "txtArtist";
            txtArtist.Size = new Size(188, 27);
            txtArtist.TabIndex = 0;
            // 
            // txtAlbum
            // 
            txtAlbum.Location = new Point(114, 125);
            txtAlbum.Margin = new Padding(5, 4, 5, 4);
            txtAlbum.Name = "txtAlbum";
            txtAlbum.Size = new Size(188, 27);
            txtAlbum.TabIndex = 1;
            // 
            // txtYear
            // 
            txtYear.Location = new Point(114, 165);
            txtYear.Margin = new Padding(5, 4, 5, 4);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(188, 27);
            txtYear.TabIndex = 2;
            // 
            // txtGenre
            // 
            txtGenre.Location = new Point(114, 205);
            txtGenre.Margin = new Padding(5, 4, 5, 4);
            txtGenre.Name = "txtGenre";
            txtGenre.Size = new Size(188, 27);
            txtGenre.TabIndex = 3;
            // 
            // lblArtist
            // 
            lblArtist.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblArtist.Location = new Point(0, 85);
            lblArtist.Margin = new Padding(5, 0, 5, 0);
            lblArtist.Name = "lblArtist";
            lblArtist.Size = new Size(105, 31);
            lblArtist.TabIndex = 7;
            lblArtist.Text = "Artist";
            lblArtist.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblAlbum
            // 
            lblAlbum.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAlbum.Location = new Point(0, 125);
            lblAlbum.Margin = new Padding(5, 0, 5, 0);
            lblAlbum.Name = "lblAlbum";
            lblAlbum.Size = new Size(105, 31);
            lblAlbum.TabIndex = 5;
            lblAlbum.Text = "Album";
            lblAlbum.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblYear
            // 
            lblYear.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblYear.Location = new Point(0, 165);
            lblYear.Margin = new Padding(5, 0, 5, 0);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(105, 31);
            lblYear.TabIndex = 8;
            lblYear.Text = "Year";
            lblYear.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblGenre
            // 
            lblGenre.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGenre.Location = new Point(0, 205);
            lblGenre.Margin = new Padding(5, 0, 5, 0);
            lblGenre.Name = "lblGenre";
            lblGenre.Size = new Size(105, 31);
            lblGenre.TabIndex = 9;
            lblGenre.Text = "Genre";
            lblGenre.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(110, 270);
            btnOk.Margin = new Padding(5, 4, 5, 4);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(101, 36);
            btnOk.TabIndex = 4;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 25);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(322, 24);
            label1.TabIndex = 6;
            label1.Text = "Found these attributes. Confirm or correct them:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(13, 1);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(322, 24);
            label2.TabIndex = 10;
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TagEditorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(352, 347);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtArtist);
            Controls.Add(txtAlbum);
            Controls.Add(txtYear);
            Controls.Add(txtGenre);
            Controls.Add(lblArtist);
            Controls.Add(lblAlbum);
            Controls.Add(lblYear);
            Controls.Add(lblGenre);
            Controls.Add(btnOk);
            Margin = new Padding(5, 4, 5, 4);
            Name = "TagEditorForm";
            Text = "Edit Tags";
            Load += TagEditorForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
    }
}