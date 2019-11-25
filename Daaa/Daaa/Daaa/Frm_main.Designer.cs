namespace Daaa
{
    partial class Frm_main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.全景管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全景录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商品录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传至ossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全景管理ToolStripMenuItem,
            this.商品管理ToolStripMenuItem,
            this.上传ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 全景管理ToolStripMenuItem
            // 
            this.全景管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全景录入ToolStripMenuItem});
            this.全景管理ToolStripMenuItem.Name = "全景管理ToolStripMenuItem";
            this.全景管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.全景管理ToolStripMenuItem.Text = "全景管理";
            // 
            // 全景录入ToolStripMenuItem
            // 
            this.全景录入ToolStripMenuItem.Name = "全景录入ToolStripMenuItem";
            this.全景录入ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全景录入ToolStripMenuItem.Text = "全景录入";
            this.全景录入ToolStripMenuItem.Click += new System.EventHandler(this.全景录入ToolStripMenuItem_Click);
            // 
            // 商品管理ToolStripMenuItem
            // 
            this.商品管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品录入ToolStripMenuItem});
            this.商品管理ToolStripMenuItem.Name = "商品管理ToolStripMenuItem";
            this.商品管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.商品管理ToolStripMenuItem.Text = "商品管理";
            // 
            // 商品录入ToolStripMenuItem
            // 
            this.商品录入ToolStripMenuItem.Name = "商品录入ToolStripMenuItem";
            this.商品录入ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.商品录入ToolStripMenuItem.Text = "商品录入";
            this.商品录入ToolStripMenuItem.Click += new System.EventHandler(this.商品录入ToolStripMenuItem_Click);
            // 
            // 上传ToolStripMenuItem
            // 
            this.上传ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上传至ossToolStripMenuItem,
            this.上传图片ToolStripMenuItem});
            this.上传ToolStripMenuItem.Name = "上传ToolStripMenuItem";
            this.上传ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.上传ToolStripMenuItem.Text = "上传";
            // 
            // 上传至ossToolStripMenuItem
            // 
            this.上传至ossToolStripMenuItem.Name = "上传至ossToolStripMenuItem";
            this.上传至ossToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.上传至ossToolStripMenuItem.Text = "上传至oss";
            this.上传至ossToolStripMenuItem.Click += new System.EventHandler(this.上传至ossToolStripMenuItem_Click);
            // 
            // 上传图片ToolStripMenuItem
            // 
            this.上传图片ToolStripMenuItem.Name = "上传图片ToolStripMenuItem";
            this.上传图片ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.上传图片ToolStripMenuItem.Text = "上传图片";
            this.上传图片ToolStripMenuItem.Click += new System.EventHandler(this.上传图片ToolStripMenuItem_Click);
            // 
            // Frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Frm_main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主界面";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全景管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全景录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商品录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上传ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上传至ossToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上传图片ToolStripMenuItem;
    }
}