namespace FilmWeb_Movie_Checker
{
    partial class OkreslanieWersji
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OkreslanieWersji));
            this.label1 = new System.Windows.Forms.Label();
            this.ListBox = new System.Windows.Forms.CheckedListBox();
            this.Apply_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Posiadana przez Ciebie wersja filmu posiada:";
            // 
            // ListBox
            // 
            this.ListBox.FormattingEnabled = true;
            this.ListBox.Items.AddRange(new object[] {
            "Napisy",
            "Napisy - brak",
            "Lektor",
            "Dubbing",
            "PL"});
            this.ListBox.Location = new System.Drawing.Point(13, 32);
            this.ListBox.Name = "ListBox";
            this.ListBox.Size = new System.Drawing.Size(137, 79);
            this.ListBox.TabIndex = 1;
            this.ListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListBox_ItemCheck);
            // 
            // Apply_button
            // 
            this.Apply_button.Image = global::FilmWeb_Movie_Checker.Properties.Resources.tick;
            this.Apply_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Apply_button.Location = new System.Drawing.Point(170, 52);
            this.Apply_button.Name = "Apply_button";
            this.Apply_button.Size = new System.Drawing.Size(90, 32);
            this.Apply_button.TabIndex = 2;
            this.Apply_button.Text = " Zatwierdź";
            this.Apply_button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Apply_button.UseVisualStyleBackColor = true;
            this.Apply_button.Click += new System.EventHandler(this.Apply_button_Click);
            // 
            // OkreslanieWersji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 120);
            this.Controls.Add(this.Apply_button);
            this.Controls.Add(this.ListBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OkreslanieWersji";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Określanie wersji filmu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckedListBox ListBox;
        private System.Windows.Forms.Button Apply_button;
    }
}