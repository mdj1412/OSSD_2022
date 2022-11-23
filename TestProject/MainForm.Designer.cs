namespace TestProject
{
    partial class MainForm
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.createButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdnTime = new System.Windows.Forms.RadioButton();
            this.rdnAlone = new System.Windows.Forms.RadioButton();
            this.klas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 200;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 60);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1458, 652);
            this.pictureBox.TabIndex = 17;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_PreviewKeyDown);
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(23, 12);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(100, 30);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "생성하기";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButten_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdnTime);
            this.groupBox1.Controls.Add(this.rdnAlone);
            this.groupBox1.Location = new System.Drawing.Point(129, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 55);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "gamemode";
            // 
            // rdnTime
            // 
            this.rdnTime.AutoSize = true;
            this.rdnTime.Location = new System.Drawing.Point(286, 20);
            this.rdnTime.Name = "rdnTime";
            this.rdnTime.Size = new System.Drawing.Size(93, 29);
            this.rdnTime.TabIndex = 1;
            this.rdnTime.TabStop = true;
            this.rdnTime.Text = "타임어택";
            this.rdnTime.UseVisualStyleBackColor = true;
            // 
            // rdnAlone
            // 
            this.rdnAlone.AutoSize = true;
            this.rdnAlone.Location = new System.Drawing.Point(124, 20);
            this.rdnAlone.Name = "rdnAlone";
            this.rdnAlone.Size = new System.Drawing.Size(93, 29);
            this.rdnAlone.TabIndex = 0;
            this.rdnAlone.TabStop = true;
            this.rdnAlone.Text = "혼자놀기";
            this.rdnAlone.UseVisualStyleBackColor = true;
            // 
            // klas
            // 
            this.klas.Location = new System.Drawing.Point(580, 16);
            this.klas.Name = "klas";
            this.klas.Size = new System.Drawing.Size(75, 32);
            this.klas.TabIndex = 19;
            this.klas.TabStop = false;
            this.klas.Text = "klas";
            this.klas.UseVisualStyleBackColor = true;
            this.klas.Visible = false;
            this.klas.Click += new System.EventHandler(this.klas_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1482, 753);
            this.Controls.Add(this.klas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.createButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "미로 길 찾기";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdnTime;
        private System.Windows.Forms.RadioButton rdnAlone;
        private System.Windows.Forms.Button klas;
    }
}

