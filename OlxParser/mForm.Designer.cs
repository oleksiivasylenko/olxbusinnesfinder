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
            this._browserSearchLinks = new Gecko.GeckoWebBrowser();
            this.tmrRestarter = new System.Windows.Forms.Timer(this.components);
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
            this.ddSettings = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.txtSettingsName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nmrStep = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpenTop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmrTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrStep)).BeginInit();
            this.SuspendLayout();
            // 
            // _browserSearchLinks
            // 
            this._browserSearchLinks.FrameEventsPropagateToMainWindow = false;
            this._browserSearchLinks.Location = new System.Drawing.Point(12, 771);
            this._browserSearchLinks.Name = "_browserSearchLinks";
            this._browserSearchLinks.Size = new System.Drawing.Size(398, 122);
            this._browserSearchLinks.TabIndex = 0;
            this._browserSearchLinks.UseHttpActivityObserver = false;
            // 
            // tmrRestarter
            // 
            this.tmrRestarter.Enabled = true;
            this.tmrRestarter.Interval = 5000;
            this.tmrRestarter.Tick += new System.EventHandler(this.tmrRestarter_Tick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(566, 91);
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
            this.txtUrl.Location = new System.Drawing.Point(77, 34);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(1377, 35);
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
            this.lstBoxStatus.Size = new System.Drawing.Size(490, 526);
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
            this.btnClearStatuses.Location = new System.Drawing.Point(1213, 91);
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
            this.lstBoxTop.Location = new System.Drawing.Point(427, 488);
            this.lstBoxTop.Name = "lstBoxTop";
            this.lstBoxTop.Size = new System.Drawing.Size(763, 700);
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
            this.btnClearSettings.Size = new System.Drawing.Size(296, 67);
            this.btnClearSettings.TabIndex = 21;
            this.btnClearSettings.Text = "ClearCurentSettings";
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
            this.lblOrdersLoaded.Location = new System.Drawing.Point(12, 146);
            this.lblOrdersLoaded.Name = "lblOrdersLoaded";
            this.lblOrdersLoaded.Size = new System.Drawing.Size(176, 29);
            this.lblOrdersLoaded.TabIndex = 23;
            this.lblOrdersLoaded.Text = "orders to parse";
            // 
            // ddSettings
            // 
            this.ddSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSettings.FormattingEnabled = true;
            this.ddSettings.Location = new System.Drawing.Point(12, 237);
            this.ddSettings.Name = "ddSettings";
            this.ddSettings.Size = new System.Drawing.Size(388, 37);
            this.ddSettings.TabIndex = 24;
            this.ddSettings.DropDown += new System.EventHandler(this.ddSettings_DropDown);
            this.ddSettings.SelectedIndexChanged += new System.EventHandler(this.ddSettings_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 29);
            this.label1.TabIndex = 25;
            this.label1.Text = "SetSettingToWork";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(12, 379);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(345, 53);
            this.btnSaveSettings.TabIndex = 26;
            this.btnSaveSettings.Text = "SaveToNewSettings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // txtSettingsName
            // 
            this.txtSettingsName.Location = new System.Drawing.Point(12, 322);
            this.txtSettingsName.Name = "txtSettingsName";
            this.txtSettingsName.Size = new System.Drawing.Size(388, 35);
            this.txtSettingsName.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(410, 29);
            this.label2.TabIndex = 28;
            this.label2.Text = "NewSettingName(without extension!)";
            // 
            // nmrStep
            // 
            this.nmrStep.Location = new System.Drawing.Point(169, 461);
            this.nmrStep.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nmrStep.Name = "nmrStep";
            this.nmrStep.Size = new System.Drawing.Size(120, 35);
            this.nmrStep.TabIndex = 29;
            this.nmrStep.ValueChanged += new System.EventHandler(this.nmrStep_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 463);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 29);
            this.label3.TabIndex = 30;
            this.label3.Text = "SelectStep";
            // 
            // btnOpenTop
            // 
            this.btnOpenTop.Location = new System.Drawing.Point(427, 220);
            this.btnOpenTop.Name = "btnOpenTop";
            this.btnOpenTop.Size = new System.Drawing.Size(408, 79);
            this.btnOpenTop.TabIndex = 31;
            this.btnOpenTop.Text = "Open Top In New Window";
            this.btnOpenTop.UseVisualStyleBackColor = true;
            this.btnOpenTop.Click += new System.EventHandler(this.btnOpenTop_Click);
            // 
            // mForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2144, 1219);
            this.Controls.Add(this.btnOpenTop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nmrStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSettingsName);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddSettings);
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
            this.Controls.Add(this._browserSearchLinks);
            this.Name = "mForm";
            this.Text = "OlxParser";
            ((System.ComponentModel.ISupportInitialize)(this.nmrTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gecko.GeckoWebBrowser _browserSearchLinks;
        private System.Windows.Forms.Timer tmrRestarter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblToParse;
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
        private System.Windows.Forms.ComboBox ddSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.TextBox txtSettingsName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmrStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOpenTop;
    }
}

