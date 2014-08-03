namespace FilmWeb_Movie_Checker
{
    partial class Adv_filter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Adv_filter));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonT = new System.Windows.Forms.Button();
            this.buttonN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(92, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Czy nazwa zawiera jeszcze jakieś \"śmieci\"?\r\n";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox1.Location = new System.Drawing.Point(12, 46);
            this.textBox1.MaximumSize = new System.Drawing.Size(472, 44);
            this.textBox1.MinimumSize = new System.Drawing.Size(472, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(472, 44);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonT
            // 
            this.buttonT.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonT.Image = global::FilmWeb_Movie_Checker.Properties.Resources.tick;
            this.buttonT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonT.Location = new System.Drawing.Point(96, 111);
            this.buttonT.Name = "buttonT";
            this.buttonT.Size = new System.Drawing.Size(105, 34);
            this.buttonT.TabIndex = 2;
            this.buttonT.Text = "    Tak";
            this.buttonT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonT.UseVisualStyleBackColor = true;
            this.buttonT.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonN
            // 
            this.buttonN.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonN.Image = global::FilmWeb_Movie_Checker.Properties.Resources.remove;
            this.buttonN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonN.Location = new System.Drawing.Point(300, 111);
            this.buttonN.Name = "buttonN";
            this.buttonN.Size = new System.Drawing.Size(105, 34);
            this.buttonN.TabIndex = 3;
            this.buttonN.Text = "    Nie";
            this.buttonN.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonN.UseVisualStyleBackColor = true;
            this.buttonN.Click += new System.EventHandler(this.button_Click);
            // 
            // Adv_filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 172);
            this.Controls.Add(this.buttonN);
            this.Controls.Add(this.buttonT);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(512, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 200);
            this.Name = "Adv_filter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zaawansowane filtrowanie";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonT;
        private System.Windows.Forms.Button buttonN;
        public System.Windows.Forms.TextBox textBox1;
    }
}