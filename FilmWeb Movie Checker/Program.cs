using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace FilmWeb_Movie_Checker
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());

        }
    }
}
