using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeAVSaver
{
    public partial class SplitterForm : Form
    {
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private System.Windows.Forms.Timer dragTimer;
        private bool isDragging;
        private bool isAudioAndVideo = false;
        TimeSpan totalDuration;
        public bool keepInitialBigFile;
        int offset = 0;

        public List<SongSegment> SongSegments { get; private set; }

        public SplitterForm(List<SongSegment> songSegments, bool isAudioAndVideo, TimeSpan totalDuration)
        {
            InitializeComponent();
            SongSegments = songSegments;
            keepInitialBigFile = false;

            // Initialize the drag timer
            dragTimer = new System.Windows.Forms.Timer();
            dragTimer.Interval = 10; // 10ms for long press
            dragTimer.Tick += DragTimer_Tick!;
            this.isAudioAndVideo = isAudioAndVideo;
            this.totalDuration = totalDuration;
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.FormClosing += new FormClosingEventHandler(SplitterForm_FormClosing!);

        }

        private void SplitterForm_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.Name = "Index";
            indexColumn.HeaderText = "No";
            indexColumn.ReadOnly = true;
            dataGridView1.Columns.Add(indexColumn);

            dataGridView1.Columns.Add("Start_time", "Start Time");
            dataGridView1.Columns.Add("End_time", "End Time");
            dataGridView1.Columns.Add("Title", "Title");
            dataGridView1.Columns.Add("Artist", "Artist");

            dataGridView1.Columns["Index"].Width = 35;
            dataGridView1.Columns["Start_time"].Width = 80;
            dataGridView1.Columns["End_time"].Width = 80;

            if (!isAudioAndVideo)
            {
                dataGridView1.Columns["Artist"].Width = 130;
                offset = 2;
            }
            else
            {
                dataGridView1.Columns["Artist"].Width = 0;
                dataGridView1.Columns["Artist"].Visible = false;
                offset = -4;
            }
            dataGridView1.Columns["Title"].Width = dataGridView1.Width - offset - dataGridView1.RowHeadersWidth -
                                                   dataGridView1.Columns["Index"].Width - dataGridView1.Columns["Start_time"].Width -
                                                   dataGridView1.Columns["Start_time"].Width - dataGridView1.Columns["Artist"].Width;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowDrop = true;
            dataGridView1.AllowUserToAddRows = false; // Disable the last empty row

            foreach (var segment in SongSegments)
            {
                dataGridView1.Rows.Add(0, segment.StartTime, segment.EndTime, segment.Title, segment.Artist);
            }

            if (SongSegments.Count == 0)
            {
                btnAdd_Click(sender, e);
            }

            UpdateIndexColumn();

            // Event handlers for drag and drop
            dataGridView1.MouseDown += new MouseEventHandler(dataGridView1_MouseDown!);
            dataGridView1.MouseMove += new MouseEventHandler(dataGridView1_MouseMove!);
            dataGridView1.MouseUp += new MouseEventHandler(dataGridView1_MouseUp!);
            dataGridView1.DragOver += new DragEventHandler(dataGridView1_DragOver!);
            dataGridView1.DragDrop += new DragEventHandler(dataGridView1_DragDrop!);
            dataGridView1.KeyDown += new KeyEventHandler(Form1_KeyDown!);
            dataGridView1.SizeChanged += DataGridView1_SizeChanged!;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged!;
        }

        private TimeSpan ParseTime(string inputTime)
        {
            if (TimeSpan.TryParse(inputTime, out TimeSpan time) && time >= TimeSpan.Zero)
            {
                return time;
            }
            return TimeSpan.Zero; // Return 00:00:00 if the input is invalid
        }

        private void UpdateIndexColumn()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Index"].Value = (i + 1).ToString();
                if (dataGridView1.Rows.Count >= 10 && (i + 1) < 10)
                    dataGridView1.Rows[i].Cells["Index"].Value = "0" + dataGridView1.Rows[i].Cells["Index"].Value;
            }
        }
        private void DataGridView1_SizeChanged(object sender, EventArgs e)
        {
            int titleColumnWidth = dataGridView1.Width - offset - dataGridView1.RowHeadersWidth -
                           dataGridView1.Columns["Index"].Width - dataGridView1.Columns["Start_time"].Width -
                           dataGridView1.Columns["End_time"].Width - dataGridView1.Columns["Artist"].Width;

            // Set the width of the "Title" column
            if (titleColumnWidth > 0) // Ensure the width is not negative
            {
                dataGridView1.Columns["Title"].Width = titleColumnWidth;
            }
        }
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Start_time"].Index ||
                e.ColumnIndex == dataGridView1.Columns["End_time"].Index)
            {

                string cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()!;

                TimeSpan sanitizedValue = ParseTime(cellValue!);

                if(e.ColumnIndex == dataGridView1.Columns["Start_time"].Index)
                {
                    TimeSpan existingStop = ParseTime(dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["End_time"].Index].Value.ToString()!);

                    if (sanitizedValue > existingStop)
                        sanitizedValue = existingStop;
                    
                }
                if (e.ColumnIndex == dataGridView1.Columns["End_time"].Index)
                {
                    TimeSpan existingStart = ParseTime(dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["Start_time"].Index].Value.ToString()!);

                    if (sanitizedValue < existingStart)
                        sanitizedValue = existingStart;
                    
                    if (sanitizedValue > totalDuration)
                        sanitizedValue = totalDuration;
                }

                // Set the sanitized value back to the cell
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = sanitizedValue;
            }
        }
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item under the mouse pointer
            rowIndexFromMouseDown = dataGridView1.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown != -1)
            {
                // Start the drag timer
                isDragging = false;
                dragTimer.Start();
            }
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && (e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Ensure the rowIndexFromMouseDown is valid before attempting to access the row
                if (rowIndexFromMouseDown >= 0 && rowIndexFromMouseDown < dataGridView1.Rows.Count)
                {
                    // Start the drag-and-drop operation
                    DragDropEffects dropEffect = dataGridView1.DoDragDrop(
                        dataGridView1.Rows[rowIndexFromMouseDown],
                        DragDropEffects.Move);
                }
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop the drag timer if the mouse is released
            dragTimer.Stop();
        }

        private void DragTimer_Tick(object sender, EventArgs e)
        {
            // Enable dragging after the long press
            isDragging = true;
            dragTimer.Stop();
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            // The row that is being dragged
            var clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            rowIndexOfItemUnderMouseToDrop = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (rowIndexOfItemUnderMouseToDrop >= 0 && rowIndexOfItemUnderMouseToDrop < dataGridView1.Rows.Count)
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    DataGridViewRow? rowToMove = e.Data?.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    if (rowToMove != null && rowIndexFromMouseDown >= 0 && rowIndexFromMouseDown < dataGridView1.Rows.Count)
                    {
                        // Remove the row and re-insert it at the drop location
                        dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
                        dataGridView1.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

                        // Update the SongSegments list accordingly
                        var itemToMove = SongSegments[rowIndexFromMouseDown];
                        SongSegments.RemoveAt(rowIndexFromMouseDown);
                        SongSegments.Insert(rowIndexOfItemUnderMouseToDrop, itemToMove);
                    }
                }
                UpdateIndexColumn();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newRow = new SongSegment
            {
                StartTime = TimeSpan.Zero,
                EndTime = this.totalDuration,
                Title = "New Item",
                Artist = "Artist"
            };

            SongSegments.Add(newRow);
            dataGridView1.Rows.Add(0, newRow.StartTime, newRow.EndTime, newRow.Title, newRow.Artist);

            UpdateIndexColumn();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    SongSegments.RemoveAt(row.Index);
                    dataGridView1.Rows.RemoveAt(row.Index);
                }
                UpdateIndexColumn();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Call the delete function
                btnDelete_Click(sender, e);

                // Optionally, mark the event as handled if needed
                e.Handled = true;
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            SongSegments.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var startTime = TimeSpan.Parse(row.Cells[1].Value.ToString()!);
                var endTime = TimeSpan.Parse(row.Cells[2].Value.ToString()!);
                var title = row.Cells[3].Value.ToString();
                var artist = row.Cells[4].Value.ToString();

                var segment = new SongSegment
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    Title = title,
                    Artist = artist
                };

                SongSegments.Add(segment);
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void SplitterForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Ensure this is how you close the form.
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                keepInitialBigFile = true;
            else
                keepInitialBigFile = false;
        }
    }
}
