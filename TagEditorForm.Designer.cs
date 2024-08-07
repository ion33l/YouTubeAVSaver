namespace YoutubeDownloader
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
        private System.Windows.Forms.Button btnCancel;

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
            btnCancel = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtArtist
            // 
            txtArtist.Location = new Point(100, 64);
            txtArtist.Margin = new Padding(4, 3, 4, 3);
            txtArtist.Name = "txtArtist";
            txtArtist.Size = new Size(165, 23);
            txtArtist.TabIndex = 0;
            // 
            // txtAlbum
            // 
            txtAlbum.Location = new Point(100, 94);
            txtAlbum.Margin = new Padding(4, 3, 4, 3);
            txtAlbum.Name = "txtAlbum";
            txtAlbum.Size = new Size(165, 23);
            txtAlbum.TabIndex = 1;
            // 
            // txtYear
            // 
            txtYear.Location = new Point(100, 124);
            txtYear.Margin = new Padding(4, 3, 4, 3);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(165, 23);
            txtYear.TabIndex = 2;
            // 
            // txtGenre
            // 
            txtGenre.Location = new Point(100, 154);
            txtGenre.Margin = new Padding(4, 3, 4, 3);
            txtGenre.Name = "txtGenre";
            txtGenre.Size = new Size(165, 23);
            txtGenre.TabIndex = 3;
            // 
            // lblArtist
            // 
            lblArtist.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblArtist.Location = new Point(0, 64);
            lblArtist.Margin = new Padding(4, 0, 4, 0);
            lblArtist.Name = "lblArtist";
            lblArtist.Size = new Size(92, 23);
            lblArtist.TabIndex = 4;
            lblArtist.Text = "Artist";
            lblArtist.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblAlbum
            // 
            lblAlbum.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAlbum.Location = new Point(0, 94);
            lblAlbum.Margin = new Padding(4, 0, 4, 0);
            lblAlbum.Name = "lblAlbum";
            lblAlbum.Size = new Size(92, 23);
            lblAlbum.TabIndex = 5;
            lblAlbum.Text = "Album";
            lblAlbum.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblYear
            // 
            lblYear.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblYear.Location = new Point(0, 124);
            lblYear.Margin = new Padding(4, 0, 4, 0);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(92, 23);
            lblYear.TabIndex = 6;
            lblYear.Text = "Year";
            lblYear.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblGenre
            // 
            lblGenre.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGenre.Location = new Point(0, 154);
            lblGenre.Margin = new Padding(4, 0, 4, 0);
            lblGenre.Name = "lblGenre";
            lblGenre.Size = new Size(92, 23);
            lblGenre.TabIndex = 7;
            lblGenre.Text = "Genre";
            lblGenre.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(67, 204);
            btnOk.Margin = new Padding(4, 3, 4, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 27);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(177, 204);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(13, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(282, 35);
            label1.TabIndex = 8;
            label1.Text = "Found these attributes. Confirm or correct them:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TagEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(308, 260);
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
            Controls.Add(btnCancel);
            Margin = new Padding(4, 3, 4, 3);
            Name = "TagEditorForm";
            Text = "Edit Tags";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
    }
}