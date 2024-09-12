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

            label2.Text = "Edit tags for: " + ((title.Length > 30) ? title.Substring(0, 30) + ".." : title);
            //return (newTitle.Length > 60) ? newTitle.Substring(0, 60) + ".." : newTitle;

            // Set default values
            txtArtist.Text = defaultArtist;
            txtAlbum.Text = defaultAlbum;
            txtYear.Text = defaultYear;
            txtGenre.Text = defaultGenre;

            Artist = defaultArtist;
            Album = defaultAlbum;
            Year = defaultYear;
            Genre = defaultGenre;

            // Attach KeyDown event handler to all textboxes
            txtArtist.KeyDown += TextBox_KeyDown!;
            txtAlbum.KeyDown += TextBox_KeyDown!;
            txtYear.KeyDown += TextBox_KeyDown!;
            txtGenre.KeyDown += TextBox_KeyDown!;
            this.FormClosing += new FormClosingEventHandler(TagEditorForm_FormClosing!);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Get values from text boxes
            Artist = txtArtist.Text;
            Album = txtAlbum.Text;
            Year = txtYear.Text;
            Genre = txtGenre.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void TagEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK) 
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure?\n\rThis cancels all the progress and all the future operations",
                    "Confirm Cancellling",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // This cancels the form closing
                }
            }
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

        private void TagEditorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
