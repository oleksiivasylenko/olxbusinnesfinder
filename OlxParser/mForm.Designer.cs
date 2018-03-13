namespace OlxParser
{
    partial class mForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._browser = new Gecko.GeckoWebBrowser();
            this.tmrRestarter = new System.Windows.Forms.Timer(this.components);
            this.tmrPageLoaded = new System.Windows.Forms.Timer(this.components);
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblToParse = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lstBoxStatus = new System.Windows.Forms.ListBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblSearchFor = new System.Windows.Forms.Label();
            this.btnClearStatuses = new System.Windows.Forms.Button();
            this.lstBoxTop = new System.Windows.Forms.ListBox();
            this.nmrTop = new System.Windows.Forms.NumericUpDown();
            this.btnBuildTop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.chkDescending = new System.Windows.Forms.CheckBox();
            this.btnClearSettings = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblOrdersLoaded = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmrTop)).BeginInit();
            this.SuspendLayout();
            // 
            // _browser
            // 
            this._browser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._browser.FrameEventsPropagateToMainWindow = false;
            this._browser.Location = new System.Drawing.Point(12, 464);
            this._browser.Name = "_browser";
            this._browser.Size = new System.Drawing.Size(398, 455);
            this._browser.TabIndex = 0;
            this._browser.UseHttpActivityObserver = false;
            // 
            // tmrRestarter
            // 
            this.tmrRestarter.Enabled = true;
            this.tmrRestarter.Interval = 3000;
            this.tmrRestarter.Tick += new System.EventHandler(this.tmrRestarter_Tick);
            // 
            // tmrPageLoaded
            // 
            this.tmrPageLoaded.Enabled = true;
            this.tmrPageLoaded.Interval = 10000;
            this.tmrPageLoaded.Tick += new System.EventHandler(this.tmrPageLoaded_Tick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(532, 91);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(364, 35);
            this.txtSearch.TabIndex = 3;
            // 
            // lblToParse
            // 
            this.lblToParse.AutoSize = true;
            this.lblToParse.Location = new System.Drawing.Point(12, 91);
            this.lblToParse.Name = "lblToParse";
            this.lblToParse.Size = new System.Drawing.Size(164, 29);
            this.lblToParse.TabIndex = 5;
            this.lblToParse.Text = "count to parse";
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Location = new System.Drawing.Point(77, 34);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(1405, 35);
            this.txtUrl.TabIndex = 6;
            // 
            // lstBoxStatus
            // 
            this.lstBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBoxStatus.FormattingEnabled = true;
            this.lstBoxStatus.ItemHeight = 29;
            this.lstBoxStatus.Location = new System.Drawing.Point(1213, 169);
            this.lstBoxStatus.Name = "lstBoxStatus";
            this.lstBoxStatus.Size = new System.Drawing.Size(989, 1019);
            this.lstBoxStatus.TabIndex = 7;
            this.lstBoxStatus.SelectedIndexChanged += new System.EventHandler(this.lstBoxStatus_SelectedIndexChanged);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(11, 34);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(50, 29);
            this.lblUrl.TabIndex = 8;
            this.lblUrl.Text = "Url:";
            // 
            // lblSearchFor
            // 
            this.lblSearchFor.AutoSize = true;
            this.lblSearchFor.Location = new System.Drawing.Point(431, 91);
            this.lblSearchFor.Name = "lblSearchFor";
            this.lblSearchFor.Size = new System.Drawing.Size(95, 29);
            this.lblSearchFor.TabIndex = 9;
            this.lblSearchFor.Text = "Search:";
            // 
            // btnClearStatuses
            // 
            this.btnClearStatuses.Location = new System.Drawing.Point(1213, 75);
            this.btnClearStatuses.Name = "btnClearStatuses";
            this.btnClearStatuses.Size = new System.Drawing.Size(230, 66);
            this.btnClearStatuses.TabIndex = 10;
            this.btnClearStatuses.Text = "Clear Statuses";
            this.btnClearStatuses.UseVisualStyleBackColor = true;
            this.btnClearStatuses.Click += new System.EventHandler(this.btnClearStatuses_Click);
            // 
            // lstBoxTop
            // 
            this.lstBoxTop.FormattingEnabled = true;
            this.lstBoxTop.ItemHeight = 29;
            this.lstBoxTop.Location = new System.Drawing.Point(427, 198);
            this.lstBoxTop.Name = "lstBoxTop";
            this.lstBoxTop.Size = new System.Drawing.Size(763, 990);
            this.lstBoxTop.TabIndex = 12;
            this.lstBoxTop.SelectedIndexChanged += new System.EventHandler(this.lstBoxTop_SelectedIndexChanged);
            // 
            // nmrTop
            // 
            this.nmrTop.Location = new System.Drawing.Point(427, 152);
            this.nmrTop.Name = "nmrTop";
            this.nmrTop.Size = new System.Drawing.Size(120, 35);
            this.nmrTop.TabIndex = 13;
            this.nmrTop.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmrTop.ValueChanged += new System.EventHandler(this.nmrTop_ValueChanged);
            // 
            // btnBuildTop
            // 
            this.btnBuildTop.Location = new System.Drawing.Point(566, 135);
            this.btnBuildTop.Name = "btnBuildTop";
            this.btnBuildTop.Size = new System.Drawing.Size(203, 52);
            this.btnBuildTop.TabIndex = 15;
            this.btnBuildTop.Text = "Build List";
            this.btnBuildTop.UseVisualStyleBackColor = true;
            this.btnBuildTop.Click += new System.EventHandler(this.btnDrawPopular_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(17, 938);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(211, 61);
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(17, 1022);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(211, 67);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chkDescending
            // 
            this.chkDescending.AutoSize = true;
            this.chkDescending.Checked = true;
            this.chkDescending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDescending.Location = new System.Drawing.Point(797, 145);
            this.chkDescending.Name = "chkDescending";
            this.chkDescending.Size = new System.Drawing.Size(174, 33);
            this.chkDescending.TabIndex = 20;
            this.chkDescending.Text = "Descending";
            this.chkDescending.UseVisualStyleBackColor = true;
            this.chkDescending.CheckedChanged += new System.EventHandler(this.chkDescending_CheckedChanged);
            // 
            // btnClearSettings
            // 
            this.btnClearSettings.Location = new System.Drawing.Point(22, 1112);
            this.btnClearSettings.Name = "btnClearSettings";
            this.btnClearSettings.Size = new System.Drawing.Size(206, 67);
            this.btnClearSettings.TabIndex = 21;
            this.btnClearSettings.Text = "ClearSettings";
            this.btnClearSettings.UseVisualStyleBackColor = true;
            this.btnClearSettings.Click += new System.EventHandler(this.btnClearSettings_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(258, 952);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(79, 29);
            this.lblStatus.TabIndex = 22;
            this.lblStatus.Text = "Status";
            // 
            // lblOrdersLoaded
            // 
            this.lblOrdersLoaded.AutoSize = true;
            this.lblOrdersLoaded.Location = new System.Drawing.Point(17, 169);
            this.lblOrdersLoaded.Name = "lblOrdersLoaded";
            this.lblOrdersLoaded.Size = new System.Drawing.Size(83, 29);
            this.lblOrdersLoaded.TabIndex = 23;
            this.lblOrdersLoaded.Text = "orders";
            // 
            // mForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2526, 1219);
            this.Controls.Add(this.lblOrdersLoaded);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClearSettings);
            this.Controls.Add(this.chkDescending);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBuildTop);
            this.Controls.Add(this.nmrTop);
            this.Controls.Add(this.lstBoxTop);
            this.Controls.Add(this.btnClearStatuses);
            this.Controls.Add(this.lblSearchFor);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lstBoxStatus);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lblToParse);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this._browser);
            this.Name = "mForm";
            this.Text = "OlxParser";
            ((System.ComponentModel.ISupportInitialize)(this.nmrTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gecko.GeckoWebBrowser _browser;
        private System.Windows.Forms.Timer tmrRestarter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblToParse;
        private System.Windows.Forms.Timer tmrPageLoaded;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.ListBox lstBoxStatus;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Label lblSearchFor;
        private System.Windows.Forms.Button btnClearStatuses;
        private System.Windows.Forms.ListBox lstBoxTop;
        private System.Windows.Forms.NumericUpDown nmrTop;
        private System.Windows.Forms.Button btnBuildTop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox chkDescending;
        private System.Windows.Forms.Button btnClearSettings;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblOrdersLoaded;
    }
}

