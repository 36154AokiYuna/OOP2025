namespace RSSReader {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            btRssGet = new Button();
            lbTitles = new ListBox();
            wvRssLink = new Microsoft.Web.WebView2.WinForms.WebView2();
            btGoBack = new Button();
            btGoForward = new Button();
            comboBox1 = new ComboBox();
            btRssAdd = new Button();
            tbFavAdd = new TextBox();
            label1 = new Label();
            ssMessageArea = new StatusStrip();
            tsslbMessage = new ToolStripStatusLabel();
            btRssDel = new Button();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            ssMessageArea.SuspendLayout();
            SuspendLayout();
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(588, 11);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(60, 33);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbTitles.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lbTitles.FormattingEnabled = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(12, 101);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(636, 130);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = Color.White;
            wvRssLink.Location = new Point(12, 237);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(636, 272);
            wvRssLink.TabIndex = 3;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.NavigationCompleted += wvRssLink_NavigationCompleted;
            // 
            // btGoBack
            // 
            btGoBack.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btGoBack.Location = new Point(12, 11);
            btGoBack.Name = "btGoBack";
            btGoBack.Size = new Size(46, 34);
            btGoBack.TabIndex = 4;
            btGoBack.Text = "戻る";
            btGoBack.UseVisualStyleBackColor = true;
            btGoBack.Click += button1_Click;
            // 
            // btGoForward
            // 
            btGoForward.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btGoForward.Location = new Point(64, 11);
            btGoForward.Name = "btGoForward";
            btGoForward.Size = new Size(48, 34);
            btGoForward.TabIndex = 5;
            btGoForward.Text = "進む";
            btGoForward.UseVisualStyleBackColor = true;
            btGoForward.Click += btGoForward_Click;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "主要", "経済", "IT" });
            comboBox1.Location = new Point(118, 15);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(464, 29);
            comboBox1.TabIndex = 6;
            // 
            // btRssAdd
            // 
            btRssAdd.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssAdd.Location = new Point(523, 62);
            btRssAdd.Name = "btRssAdd";
            btRssAdd.Size = new Size(59, 29);
            btRssAdd.TabIndex = 7;
            btRssAdd.Text = "登録";
            btRssAdd.UseVisualStyleBackColor = true;
            btRssAdd.Click += btRssAdd_Click;
            // 
            // tbFavAdd
            // 
            tbFavAdd.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbFavAdd.Location = new Point(128, 62);
            tbFavAdd.Name = "tbFavAdd";
            tbFavAdd.Size = new Size(389, 29);
            tbFavAdd.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(12, 65);
            label1.Name = "label1";
            label1.Size = new Size(110, 21);
            label1.TabIndex = 9;
            label1.Text = "お気に入り登録";
            // 
            // ssMessageArea
            // 
            ssMessageArea.Items.AddRange(new ToolStripItem[] { tsslbMessage });
            ssMessageArea.Location = new Point(0, 522);
            ssMessageArea.Name = "ssMessageArea";
            ssMessageArea.Size = new Size(661, 22);
            ssMessageArea.TabIndex = 10;
            ssMessageArea.Text = "statusStrip1";
            // 
            // tsslbMessage
            // 
            tsslbMessage.Name = "tsslbMessage";
            tsslbMessage.Size = new Size(0, 17);
            // 
            // btRssDel
            // 
            btRssDel.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssDel.Location = new Point(590, 61);
            btRssDel.Name = "btRssDel";
            btRssDel.Size = new Size(59, 29);
            btRssDel.TabIndex = 11;
            btRssDel.Text = "削除";
            btRssDel.UseVisualStyleBackColor = true;
            btRssDel.Click += btRssDel_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(661, 544);
            Controls.Add(btRssDel);
            Controls.Add(ssMessageArea);
            Controls.Add(label1);
            Controls.Add(tbFavAdd);
            Controls.Add(btRssAdd);
            Controls.Add(comboBox1);
            Controls.Add(btGoForward);
            Controls.Add(btGoBack);
            Controls.Add(wvRssLink);
            Controls.Add(lbTitles);
            Controls.Add(btRssGet);
            Name = "Form1";
            Text = "RSSリーダー";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            ssMessageArea.ResumeLayout(false);
            ssMessageArea.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvRssLink;
        private Button btGoBack;
        private Button btGoForward;
        private ComboBox comboBox1;
        private Button btRssAdd;
        private TextBox tbFavAdd;
        private Label label1;
        private StatusStrip ssMessageArea;
        private ToolStripStatusLabel tsslbMessage;
        private Button btRssDel;
    }
}
