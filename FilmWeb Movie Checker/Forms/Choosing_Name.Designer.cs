namespace FilmWeb_Movie_Checker
{
    partial class ChooseName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseName));
            this.AF_label = new System.Windows.Forms.Label();
            this.Title_textBox = new System.Windows.Forms.TextBox();
            this.buttonT = new System.Windows.Forms.Button();
            this.buttonN = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // AF_label
            // 
            this.AF_label.AutoSize = true;
            this.AF_label.Location = new System.Drawing.Point(93, 9);
            this.AF_label.Name = "AF_label";
            this.AF_label.Size = new System.Drawing.Size(145, 13);
            this.AF_label.TabIndex = 0;
            this.AF_label.Text = "Wybierz poprawny tytuł filmu:";
            // 
            // Title_textBox
            // 
            this.Title_textBox.Enabled = false;
            this.Title_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Title_textBox.Location = new System.Drawing.Point(22, 115);
            this.Title_textBox.MaximumSize = new System.Drawing.Size(300, 30);
            this.Title_textBox.MinimumSize = new System.Drawing.Size(300, 30);
            this.Title_textBox.Name = "Title_textBox";
            this.Title_textBox.Size = new System.Drawing.Size(300, 26);
            this.Title_textBox.TabIndex = 1;
            this.Title_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Title_textBox.Visible = false;
            this.Title_textBox.TextChanged += new System.EventHandler(this.Title_textBox_TextChanged);
            // 
            // buttonT
            // 
            this.buttonT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonT.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonT.Image = global::FilmWeb_Movie_Checker.Properties.Resources.tick;
            this.buttonT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonT.Location = new System.Drawing.Point(47, 115);
            this.buttonT.Name = "buttonT";
            this.buttonT.Size = new System.Drawing.Size(89, 34);
            this.buttonT.TabIndex = 2;
            this.buttonT.Text = "  &Tak";
            this.buttonT.UseVisualStyleBackColor = true;
            // 
            // buttonN
            // 
            this.buttonN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonN.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonN.Image = global::FilmWeb_Movie_Checker.Properties.Resources.remove;
            this.buttonN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonN.Location = new System.Drawing.Point(200, 115);
            this.buttonN.Name = "buttonN";
            this.buttonN.Size = new System.Drawing.Size(89, 34);
            this.buttonN.TabIndex = 3;
            this.buttonN.Text = "  &Nie";
            this.buttonN.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(22, 36);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(22, 62);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(22, 88);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(56, 17);
            this.radioButton3.TabIndex = 6;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Żaden";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // ChooseName
            // 
            this.AcceptButton = this.buttonT;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonN;
            this.ClientSize = new System.Drawing.Size(340, 156);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.buttonN);
            this.Controls.Add(this.buttonT);
            this.Controls.Add(this.Title_textBox);
            this.Controls.Add(this.AF_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zaawansowane filtrowanie";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChooseName_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AF_label;
        private System.Windows.Forms.Button buttonT;
        private System.Windows.Forms.Button buttonN;
        public System.Windows.Forms.TextBox Title_textBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}