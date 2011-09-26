namespace CheckPhoto
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labPercent = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbCover = new System.Windows.Forms.ComboBox();
            this.cbbRobe = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gvFile = new System.Windows.Forms.DataGridView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cbYear = new System.Windows.Forms.CheckBox();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Height = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFile)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gvFile, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 426);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbYear);
            this.panel1.Controls.Add(this.labPercent);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.btnOpenFolder);
            this.panel1.Controls.Add(this.txtFolder);
            this.panel1.Controls.Add(this.btnCheck);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbbCover);
            this.panel1.Controls.Add(this.cbbRobe);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 84);
            this.panel1.TabIndex = 0;
            // 
            // labPercent
            // 
            this.labPercent.AutoSize = true;
            this.labPercent.Location = new System.Drawing.Point(488, 63);
            this.labPercent.Name = "labPercent";
            this.labPercent.Size = new System.Drawing.Size(17, 12);
            this.labPercent.TabIndex = 10;
            this.labPercent.Text = "0%";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(102, 58);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(380, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(524, 3);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFolder.TabIndex = 8;
            this.btnOpenFolder.Text = "打开文件夹";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(102, 3);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(416, 21);
            this.txtFolder.TabIndex = 7;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(524, 58);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 6;
            this.btnCheck.Text = "检查";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "检查进度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(237, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "封面尺寸：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "礼服尺寸：";
            // 
            // cbbCover
            // 
            this.cbbCover.FormattingEnabled = true;
            this.cbbCover.Location = new System.Drawing.Point(309, 31);
            this.cbbCover.Name = "cbbCover";
            this.cbbCover.Size = new System.Drawing.Size(121, 20);
            this.cbbCover.TabIndex = 3;
            this.cbbCover.Text = "请选择尺寸";
            // 
            // cbbRobe
            // 
            this.cbbRobe.FormattingEnabled = true;
            this.cbbRobe.Location = new System.Drawing.Point(102, 30);
            this.cbbRobe.Name = "cbbRobe";
            this.cbbRobe.Size = new System.Drawing.Size(121, 20);
            this.cbbRobe.TabIndex = 3;
            this.cbbRobe.Text = "请选择尺寸";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择文件路径：";
            // 
            // gvFile
            // 
            this.gvFile.AllowUserToAddRows = false;
            this.gvFile.AllowUserToDeleteRows = false;
            this.gvFile.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.gvFile.ColumnHeadersHeight = 24;
            this.gvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.FilePath,
            this.Width,
            this.Height});
            this.gvFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvFile.Location = new System.Drawing.Point(3, 93);
            this.gvFile.Name = "gvFile";
            this.gvFile.RowTemplate.Height = 23;
            this.gvFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvFile.Size = new System.Drawing.Size(618, 330);
            this.gvFile.TabIndex = 1;
            this.gvFile.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gvFile_RowPostPaint);
            // 
            // cbYear
            // 
            this.cbYear.AutoSize = true;
            this.cbYear.Location = new System.Drawing.Point(447, 33);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(97, 16);
            this.cbYear.TabIndex = 11;
            this.cbYear.Text = "是否检查年历";
            this.cbYear.UseVisualStyleBackColor = true;
            // 
            // FileName
            // 
            this.FileName.HeaderText = "文件名";
            this.FileName.Name = "FileName";
            // 
            // FilePath
            // 
            this.FilePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FilePath.HeaderText = "文件路径";
            this.FilePath.Name = "FilePath";
            // 
            // Width
            // 
            this.Width.FillWeight = 80F;
            this.Width.HeaderText = "宽";
            this.Width.Name = "Width";
            this.Width.Width = 80;
            // 
            // Height
            // 
            this.Height.FillWeight = 80F;
            this.Height.HeaderText = "高";
            this.Height.Name = "Height";
            this.Height.Width = 80;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 426);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "迪士尼童话世界相册图片检查";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbRobe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ComboBox cbbCover;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.DataGridView gvFile;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labPercent;
        private System.Windows.Forms.CheckBox cbYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Width;
        private System.Windows.Forms.DataGridViewTextBoxColumn Height;


    }
}

