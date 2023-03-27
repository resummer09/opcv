
namespace open0322
{
    partial class Image_win
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.picOriginal = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.열기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenImg = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReset = new System.Windows.Forms.Button();
            this.picResult = new System.Windows.Forms.PictureBox();
            this.btnBlur = new System.Windows.Forms.Button();
            this.btnGray = new System.Windows.Forms.Button();
            this.btnBin = new System.Windows.Forms.Button();
            this.btnTest1 = new System.Windows.Forms.Button();
            this.txtReadbox = new System.Windows.Forms.TextBox();
            this.btnEdge = new System.Windows.Forms.Button();
            this.btnBoxCheck = new System.Windows.Forms.Button();
            this.btnContours = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).BeginInit();
            this.SuspendLayout();
            // 
            // picOriginal
            // 
            this.picOriginal.Location = new System.Drawing.Point(39, 155);
            this.picOriginal.Name = "picOriginal";
            this.picOriginal.Size = new System.Drawing.Size(800, 800);
            this.picOriginal.TabIndex = 0;
            this.picOriginal.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.열기ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1904, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 열기ToolStripMenuItem
            // 
            this.열기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpenImg});
            this.열기ToolStripMenuItem.Name = "열기ToolStripMenuItem";
            this.열기ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.열기ToolStripMenuItem.Text = "열기";
            // 
            // MenuOpenImg
            // 
            this.MenuOpenImg.Name = "MenuOpenImg";
            this.MenuOpenImg.Size = new System.Drawing.Size(110, 22);
            this.MenuOpenImg.Text = "이미지";
            this.MenuOpenImg.Click += new System.EventHandler(this.MenuOpenImg_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(904, 594);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "초기화";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // picResult
            // 
            this.picResult.Location = new System.Drawing.Point(1044, 155);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(800, 800);
            this.picResult.TabIndex = 4;
            this.picResult.TabStop = false;
            // 
            // btnBlur
            // 
            this.btnBlur.Location = new System.Drawing.Point(904, 193);
            this.btnBlur.Name = "btnBlur";
            this.btnBlur.Size = new System.Drawing.Size(75, 23);
            this.btnBlur.TabIndex = 5;
            this.btnBlur.Text = "가우시안 블러";
            this.btnBlur.UseVisualStyleBackColor = true;
            this.btnBlur.Click += new System.EventHandler(this.btnBlur_Click);
            // 
            // btnGray
            // 
            this.btnGray.Location = new System.Drawing.Point(904, 231);
            this.btnGray.Name = "btnGray";
            this.btnGray.Size = new System.Drawing.Size(75, 23);
            this.btnGray.TabIndex = 6;
            this.btnGray.Text = "흑백화";
            this.btnGray.UseVisualStyleBackColor = true;
            this.btnGray.Click += new System.EventHandler(this.btnGray_Click);
            // 
            // btnBin
            // 
            this.btnBin.Location = new System.Drawing.Point(904, 273);
            this.btnBin.Name = "btnBin";
            this.btnBin.Size = new System.Drawing.Size(75, 23);
            this.btnBin.TabIndex = 7;
            this.btnBin.Text = "이진화";
            this.btnBin.UseVisualStyleBackColor = true;
            this.btnBin.Click += new System.EventHandler(this.btnBin_Click);
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(904, 524);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(75, 23);
            this.btnTest1.TabIndex = 9;
            this.btnTest1.Text = "글자인식";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
            // 
            // txtReadbox
            // 
            this.txtReadbox.Location = new System.Drawing.Point(1044, 107);
            this.txtReadbox.Name = "txtReadbox";
            this.txtReadbox.Size = new System.Drawing.Size(800, 21);
            this.txtReadbox.TabIndex = 10;
            // 
            // btnEdge
            // 
            this.btnEdge.Location = new System.Drawing.Point(904, 312);
            this.btnEdge.Name = "btnEdge";
            this.btnEdge.Size = new System.Drawing.Size(75, 23);
            this.btnEdge.TabIndex = 8;
            this.btnEdge.Text = "경계추출";
            this.btnEdge.UseVisualStyleBackColor = true;
            this.btnEdge.Click += new System.EventHandler(this.btnEdge_Click);
            // 
            // btnBoxCheck
            // 
            this.btnBoxCheck.Location = new System.Drawing.Point(904, 408);
            this.btnBoxCheck.Name = "btnBoxCheck";
            this.btnBoxCheck.Size = new System.Drawing.Size(75, 23);
            this.btnBoxCheck.TabIndex = 11;
            this.btnBoxCheck.Text = "영역찾기";
            this.btnBoxCheck.UseVisualStyleBackColor = true;
            this.btnBoxCheck.Click += new System.EventHandler(this.btnBoxCheck_Click);
            // 
            // btnContours
            // 
            this.btnContours.Location = new System.Drawing.Point(904, 355);
            this.btnContours.Name = "btnContours";
            this.btnContours.Size = new System.Drawing.Size(75, 23);
            this.btnContours.TabIndex = 12;
            this.btnContours.Text = "윤곽선찾기";
            this.btnContours.UseVisualStyleBackColor = true;
            this.btnContours.Click += new System.EventHandler(this.btnContours_Click);
            // 
            // Image_win
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.btnContours);
            this.Controls.Add(this.btnBoxCheck);
            this.Controls.Add(this.txtReadbox);
            this.Controls.Add(this.btnTest1);
            this.Controls.Add(this.btnEdge);
            this.Controls.Add(this.btnBin);
            this.Controls.Add(this.btnGray);
            this.Controls.Add(this.btnBlur);
            this.Controls.Add(this.picResult);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.picOriginal);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Image_win";
            this.Text = "Image_method";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picOriginal;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenImg;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.PictureBox picResult;
        private System.Windows.Forms.Button btnBlur;
        private System.Windows.Forms.Button btnGray;
        private System.Windows.Forms.Button btnBin;
        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.TextBox txtReadbox;
        private System.Windows.Forms.Button btnEdge;
        private System.Windows.Forms.Button btnBoxCheck;
        private System.Windows.Forms.Button btnContours;
    }
}

