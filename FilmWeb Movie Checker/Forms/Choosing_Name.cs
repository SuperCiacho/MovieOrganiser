using System.Windows.Forms;
using System.Globalization;
using System;
using System.Text;

namespace FilmWeb_Movie_Checker
{
    public partial class ChooseName : Form
    {
        private StringBuilder sb = null;
        public string name;
        private Timer timer = new Timer();
        private int counter = 0;

        public ChooseName(string folder, string file)
        {
            InitializeComponent();

            sb = new StringBuilder();
            timer.Interval = 1;
            timer.Tick += timer_Tick;

            radioButton1.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(file);
            radioButton2.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(folder);

            if (file == folder)
            {
                radioButton2.Hide();
                Title_textBox.Location = radioButton3.Location;
                radioButton3.Location = radioButton2.Location;
                this.Height -= 20;
            }

            //Skalowanie szerokości okienka
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

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var chkBox = sender as RadioButton;

            if (chkBox.Checked)
                sb.Clear().Append(chkBox.Text);
        }

        private void radioButton3_CheckedChanged(object sender, System.EventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (counter < 40)
            {
                if (radioButton3.Checked)
                    this.Height += 5;
                else
                    this.Height -= 5;
                counter += 5;
            }
            else
            {
                Title_textBox.Enabled = Title_textBox.Visible = radioButton3.Checked;
                counter = 0;
                timer.Stop();
            }
        }

        private void Title_textBox_TextChanged(object sender, EventArgs e)
        {
            sb.Clear().Append(Title_textBox.Text);
        }

        private void ChooseName_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked && this.DialogResult == DialogResult.Yes)
            {
                e.Cancel = true;
                MessageBox.Show(this, "Musisz zaznaczyć jedną z opcji!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (sb.Length > 0)
                name = sb.ToString();
        }        
    }
}
