using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilmWeb_Movie_Checker
{
    public partial class Choosing_Name : Form
    {
        public string name = null;

        public Choosing_Name(string folder, string file)
        {
            InitializeComponent();
            name = radioButton1.Text = file;
            radioButton2.Text = folder;
            if (radioButton2.Size.Width > radioButton1.Size.Width)
            {
                if (radioButton2.Size.Width - 24 > 300)
                    this.Width = radioButton2.Size.Width + 24; 
            }
            else
            {
                if (radioButton1.Size.Width - 24 > 300)
                    this.Width = radioButton1.Size.Width + 24;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                if (radioButton1.Checked == true)
                    name = radioButton1.Text;
                else
                    name = "#" + radioButton2.Text;
            }

            this.Close();
        }
    }
}
