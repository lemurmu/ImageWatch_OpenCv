namespace ImageExDemo
{
    partial class DemoDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDrawCircle = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDrawDot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDrawLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDrawRect = new System.Windows.Forms.ToolStripButton();
            this._cvDisplay = new ImageExDemo.CvDisplay();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cvDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonClear,
            this.toolStripSeparator1,
            this.toolStripButtonDrawCircle,
            this.toolStripButtonDrawDot,
            this.toolStripButtonDrawLine,
            this.toolStripButtonDrawRect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(913, 40);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(60, 37);
            this.toolStripButtonOpen.Text = "打开图片";
            this.toolStripButtonOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButtonClear
            // 
            this.toolStripButtonClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClear.Image")));
            this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClear.Name = "toolStripButtonClear";
            this.toolStripButtonClear.Size = new System.Drawing.Size(49, 37);
            this.toolStripButtonClear.Text = "清空UI";
            this.toolStripButtonClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonClear.Click += new System.EventHandler(this.toolStripButtonClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonDrawCircle
            // 
            this.toolStripButtonDrawCircle.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDrawCircle.Image")));
            this.toolStripButtonDrawCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDrawCircle.Name = "toolStripButtonDrawCircle";
            this.toolStripButtonDrawCircle.Size = new System.Drawing.Size(36, 37);
            this.toolStripButtonDrawCircle.Text = "画圆";
            this.toolStripButtonDrawCircle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDrawCircle.Click += new System.EventHandler(this.toolStripButtonDrawCircle_Click);
            // 
            // toolStripButtonDrawDot
            // 
            this.toolStripButtonDrawDot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDrawDot.Image")));
            this.toolStripButtonDrawDot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDrawDot.Name = "toolStripButtonDrawDot";
            this.toolStripButtonDrawDot.Size = new System.Drawing.Size(36, 37);
            this.toolStripButtonDrawDot.Text = "画点";
            this.toolStripButtonDrawDot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDrawDot.Click += new System.EventHandler(this.toolStripButtonDrawDot_Click);
            // 
            // toolStripButtonDrawLine
            // 
            this.toolStripButtonDrawLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDrawLine.Image")));
            this.toolStripButtonDrawLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDrawLine.Name = "toolStripButtonDrawLine";
            this.toolStripButtonDrawLine.Size = new System.Drawing.Size(36, 37);
            this.toolStripButtonDrawLine.Text = "画线";
            this.toolStripButtonDrawLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDrawLine.Click += new System.EventHandler(this.toolStripButtonDrawLine_Click);
            // 
            // toolStripButtonDrawRect
            // 
            this.toolStripButtonDrawRect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDrawRect.Image")));
            this.toolStripButtonDrawRect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDrawRect.Name = "toolStripButtonDrawRect";
            this.toolStripButtonDrawRect.Size = new System.Drawing.Size(48, 37);
            this.toolStripButtonDrawRect.Text = "画矩形";
            this.toolStripButtonDrawRect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDrawRect.Click += new System.EventHandler(this.toolStripButtonDrawRect_Click);
            // 
            // _cvDisplay
            // 
            this._cvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cvDisplay.AutoDisplay = ImageExDemo.CvDisplay.AutoDisplayMode.Original;
            this._cvDisplay.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._cvDisplay.Image = null;
            this._cvDisplay.Location = new System.Drawing.Point(0, 43);
            this._cvDisplay.Name = "_cvDisplay";
            this._cvDisplay.Size = new System.Drawing.Size(913, 536);
            this._cvDisplay.TabIndex = 3;
            this._cvDisplay.TabStop = false;
            // 
            // DemoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 581);
            this.Controls.Add(this._cvDisplay);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DemoDialog";
            this.Text = "CvDisplay Demo";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cvDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDrawCircle;
        private System.Windows.Forms.ToolStripButton toolStripButtonDrawDot;
        private System.Windows.Forms.ToolStripButton toolStripButtonDrawLine;
        private System.Windows.Forms.ToolStripButton toolStripButtonDrawRect;
        private CvDisplay _cvDisplay;
    }
}

