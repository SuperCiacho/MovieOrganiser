namespace FilmWeb_Movie_Checker
{
    public partial class Last_chance : System.Windows.Forms.Form
    {
        public Last_chance()
        {
            InitializeComponent();
        }

        private void buttonT_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void buttonT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) buttonT_Click(sender, e);
        }
    }
}
