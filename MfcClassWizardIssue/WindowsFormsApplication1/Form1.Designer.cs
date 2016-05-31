namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.userControl21 = new CSActiveXUserControl2.UserControl2();
            this.userControl11 = new CSActiveXUserControl1.UserControl1();
            this.userControl51 = new CSActiveXUserControl3.UserControl5();
            this.userControl41 = new CSActiveXUserControl3.UserControl4();
            this.userControl31 = new CSActiveXUserControl3.UserControl3();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(70, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // userControl21
            // 
            this.userControl21.AutoSize = true;
            this.userControl21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.userControl21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.userControl21.Location = new System.Drawing.Point(70, 121);
            this.userControl21.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControl21.Name = "userControl21";
            this.userControl21.Size = new System.Drawing.Size(122, 41);
            this.userControl21.TabIndex = 1;
            // 
            // userControl11
            // 
            this.userControl11.AutoSize = true;
            this.userControl11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.userControl11.Location = new System.Drawing.Point(70, 36);
            this.userControl11.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(122, 41);
            this.userControl11.TabIndex = 0;
            // 
            // userControl51
            // 
            this.userControl51.Location = new System.Drawing.Point(70, 351);
            this.userControl51.Name = "userControl51";
            this.userControl51.Size = new System.Drawing.Size(117, 34);
            this.userControl51.TabIndex = 4;
            // 
            // userControl41
            // 
            this.userControl41.AutoSize = true;
            this.userControl41.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.userControl41.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.userControl41.Location = new System.Drawing.Point(70, 278);
            this.userControl41.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControl41.Name = "userControl41";
            this.userControl41.Size = new System.Drawing.Size(122, 41);
            this.userControl41.TabIndex = 3;
            // 
            // userControl31
            // 
            this.userControl31.AutoSize = true;
            this.userControl31.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.userControl31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.userControl31.Location = new System.Drawing.Point(70, 197);
            this.userControl31.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControl31.Name = "userControl31";
            this.userControl31.Size = new System.Drawing.Size(122, 41);
            this.userControl31.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 515);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.userControl51);
            this.Controls.Add(this.userControl41);
            this.Controls.Add(this.userControl31);
            this.Controls.Add(this.userControl21);
            this.Controls.Add(this.userControl11);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CSActiveXUserControl1.UserControl1 userControl11;
        private CSActiveXUserControl2.UserControl2 userControl21;
        private CSActiveXUserControl3.UserControl3 userControl31;
        private CSActiveXUserControl3.UserControl4 userControl41;
        private CSActiveXUserControl3.UserControl5 userControl51;
        private System.Windows.Forms.Button button1;
    }
}

