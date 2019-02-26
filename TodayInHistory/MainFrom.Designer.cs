namespace TodayInHistory
{
    partial class MainFrom
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.textBoxAppKey = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "AppKey:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(273, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "获取";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxContent
            // 
            this.textBoxContent.Location = new System.Drawing.Point(12, 39);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContent.Size = new System.Drawing.Size(414, 125);
            this.textBoxContent.TabIndex = 7;
            // 
            // textBoxAppKey
            // 
            this.textBoxAppKey.Location = new System.Drawing.Point(67, 12);
            this.textBoxAppKey.Name = "textBoxAppKey";
            this.textBoxAppKey.Size = new System.Drawing.Size(200, 21);
            this.textBoxAppKey.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(354, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "下载图片";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 177);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxAppKey);
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainFrom";
            this.Text = "获取历史上的今天";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Parame;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxContent;
        private System.Windows.Forms.TextBox textBoxAppKey;
        private System.Windows.Forms.Button button2;
    }
}

