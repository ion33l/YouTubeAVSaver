using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeDownloader
{
    public partial class TagEditorForm : Form
    {
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public string Year { get; private set; }
        public string Genre { get; private set; }

        public TagEditorForm(string defaultArtist, string defaultAlbum, string defaultYear, string defaultGenre, string title)
        {
            InitializeComponent();

            // Set default values
            txtArtist.Text = defaultArtist;
            txtAlbum.Text = defaultAlbum;
            txtYear.Text = defaultYear;
            txtGenre.Text = defaultGenre;
            label2.Text = "Editing tags for: " + title;

            // Attach KeyDown event handler to all textboxes
            txtArtist.KeyDown += TextBox_KeyDown;
            txtAlbum.KeyDown += TextBox_KeyDown;
            txtYear.KeyDown += TextBox_KeyDown;
            txtGenre.KeyDown += TextBox_KeyDown;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Get values from text boxes
            Artist = txtArtist.Text;
            Album = txtAlbum.Text;
            Genre = txtGenre.Text;
            Year = txtYear.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Suppress the default beep sound
                e.SuppressKeyPress = true;

                // Call the button click event handler
                btnOk_Click(this, new EventArgs());
            }
        }
    }
}
