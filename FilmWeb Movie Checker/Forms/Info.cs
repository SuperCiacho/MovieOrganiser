namespace FilmWeb_Movie_Checker
{
    public partial class Info : System.Windows.Forms.Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
