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
            this.btnGetLinks = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnGetViewsCount = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearchOlx = new System.Windows.Forms.Button();
            this.lblToParse = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _browser
            // 
            this._browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._browser.FrameEventsPropagateToMainWindow = false;
            this._browser.Location = new System.Drawing.Point(10, 114);
            this._browser.Name = "_browser";
            this._browser.Size = new System.Drawing.Size(2258, 1129);
            this._browser.TabIndex = 0;
            this._browser.UseHttpActivityObserver = false;
            // 
            // btnGetLinks
            // 
            this.btnGetLinks.Location = new System.Drawing.Point(225, 11);
            this.btnGetLinks.Name = "btnGetLinks";
            this.btnGetLinks.Size = new System.Drawing.Size(164, 69);
            this.btnGetLinks.TabIndex = 1;
            this.btnGetLinks.Text = "GetAllLinks";
            this.btnGetLinks.UseVisualStyleBackColor = true;
            this.btnGetLinks.Click += new System.EventHandler(this.btnGetLinks_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnGetViewsCount
            // 
            this.btnGetViewsCount.Location = new System.Drawing.Point(395, 15);
            this.btnGetViewsCount.Name = "btnGetViewsCount";
            this.btnGetViewsCount.Size = new System.Drawing.Size(295, 65);
            this.btnGetViewsCount.TabIndex = 2;
            this.btnGetViewsCount.Text = "GetViewsCount";
            this.btnGetViewsCount.UseVisualStyleBackColor = true;
            this.btnGetViewsCount.Click += new System.EventHandler(this.btnGetViewsCount_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(1022, 28);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1245, 35);
            this.txtSearch.TabIndex = 3;
            // 
            // btnSearchOlx
            // 
            this.btnSearchOlx.Location = new System.Drawing.Point(12, 12);
            this.btnSearchOlx.Name = "btnSearchOlx";
            this.btnSearchOlx.Size = new System.Drawing.Size(207, 65);
            this.btnSearchOlx.TabIndex = 4;
            this.btnSearchOlx.Text = "Search OLX";
            this.btnSearchOlx.UseVisualStyleBackColor = true;
            this.btnSearchOlx.Click += new System.EventHandler(this.btnSearchOlx_Click);
            // 
            // lblToParse
            // 
            this.lblToParse.AutoSize = true;
            this.lblToParse.Location = new System.Drawing.Point(723, 34);
            this.lblToParse.Name = "lblToParse";
            this.lblToParse.Size = new System.Drawing.Size(164, 29);
            this.lblToParse.TabIndex = 5;
            this.lblToParse.Text = "count to parse";
            // 
            // mForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2279, 1254);
            this.Controls.Add(this.lblToParse);
            this.Controls.Add(this.btnSearchOlx);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnGetViewsCount);
            this.Controls.Add(this.btnGetLinks);
            this.Controls.Add(this._browser);
            this.Name = "mForm";
            this.Text = "OlxParser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gecko.GeckoWebBrowser _browser;
        private System.Windows.Forms.Button btnGetLinks;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnGetViewsCount;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearchOlx;
        private System.Windows.Forms.Label lblToParse;
    }
}

