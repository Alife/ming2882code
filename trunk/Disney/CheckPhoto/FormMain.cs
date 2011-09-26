using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace CheckPhoto
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent(); 
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            PhotoSizeBLL photoSize = new PhotoSizeBLL(Application.StartupPath + "\\PhotoSize.xml");
            cbbRobe.DataSource = photoSize.GetRobeList();
            cbbRobe.DisplayMember = "size";
            cbbRobe.ValueMember = "width";
            cbbCover.DataSource = photoSize.GetCoverList();
            cbbCover.DisplayMember = "size";
            cbbCover.ValueMember = "width";
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "请选择文件夹";
            if (!string.IsNullOrEmpty(txtFolder.Text))
                folderBrowserDialog1.SelectedPath = @txtFolder.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;
                if (!string.IsNullOrEmpty(folderName))
                    txtFolder.Text = folderName;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolder.Text))
            {
                MessageBox.Show("请打开文件夹");
                return;
            }
            progressBar1.Value = 0;
            gvFile.Rows.Clear();
            Thread thread = new Thread(new ThreadStart(GetBindFile));
            thread.IsBackground = true;
            thread.Start();
        }
        private delegate void BindFile();
        private void GetBindFile()
        {
            if (InvokeRequired)
                Invoke(new BindFile(GetBindFile));
            else
            {
                #region 执行方法
                showProgressBar = new ShowProgressBar(Show_ProgressBar);
                string folderName = txtFolder.Text;
                var robePhotoSize = (PhotoSize)cbbRobe.SelectedItem;
                var coverPhotoSize = (PhotoSize)cbbCover.SelectedItem;
                bool isYear = cbYear.Checked;
                if (!isYear)
                {
                    if (robePhotoSize.width == 0 && coverPhotoSize.width == 0)
                    {
                        MessageBox.Show("请选择尺寸");
                        return;
                    }
                }
                string[] arrFiles = Directory.GetFiles(folderName, "*.jpg", SearchOption.AllDirectories);
                for (int i = 0; i < arrFiles.Length; i++)
                {
                    FileInfo fileinfo = new FileInfo(arrFiles[i]);
                    try
                    {
                        Image img = Image.FromFile(arrFiles[i]);
                        int width = img.Width, height = img.Height;
                        img.Dispose();
                        if (robePhotoSize.width > 0)
                        {
                            if (width == robePhotoSize.width && height == robePhotoSize.height)
                                continue;
                        }
                        if (coverPhotoSize.width > 0)
                        {
                            if (width == coverPhotoSize.width && height == coverPhotoSize.height)
                                continue;
                        }
                        if (isYear)
                        {
                            if (width == 3048 && height == 4572)
                                continue;
                        }
                        DataGridViewRow grow = gvFile.Rows[gvFile.Rows.Add()];
                        grow.Cells[0].Value = fileinfo.Name;
                        grow.Cells[1].Value = fileinfo.FullName;
                        grow.Cells[2].Value = width;
                        grow.Cells[3].Value = height;
                        gvFile.Refresh();
                    }
                    catch { }
                    showProgressBar(arrFiles.Length, i + 1);
                }
                #endregion
            }
        }
        private delegate void ShowProgressBar(long total, long current);
        private event ShowProgressBar showProgressBar;
        private void Show_ProgressBar(long total, long current)
        {
            if (InvokeRequired)
                Invoke(new ShowProgressBar(Show_ProgressBar), new object[] { total, current });
            else
            {
                progressBar1.Maximum = (int)total;
                progressBar1.Value = (int)current;
                labPercent.Text = (int)((current / (double)total) * 100.0) + "%";
                labPercent.Refresh();
            }
        }

        private void gvFile_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              gvFile.RowHeadersWidth - 4,
              e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                gvFile.RowHeadersDefaultCellStyle.Font,
                rectangle,
                gvFile.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
