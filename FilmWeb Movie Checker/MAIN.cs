using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FilmWeb_Movie_Checker
{
    public partial class MAIN : Form
    {
        #region Zmienne
        #region Adresy
        private string FW_serial = "http://www.filmweb.pl/search/serial?q=";
        private string FW_film = "http://www.filmweb.pl/search/film?q=";
        private string FW_adres = "http://www.filmweb.pl/search?q=";
        #endregion
        public DirectoryInfo File_Path = null; 
        private string rok = null;
        private string[] ext = new string[] { "avi", "mkv", "rm", "rmvb", "mp4", "3gp","flv", "mpeg", "mpg", "mov"};
        private bool dir = false;
        #endregion
        #region Funkcje
        private void WhiteMarksDeleter(TextBox obiekt)
        {
            while (obiekt.Text[obiekt.Text.Length - 1] == ' ') obiekt.Text = obiekt.Text.Remove(obiekt.Text.Length - 1);
        }
        private void filtruj()
        {
            #region First Filtration

            if (dir && InputBox.Text.Contains("{"))
            {
                InputBox.Text = InputBox.Text.Remove(InputBox.Text.IndexOf('{')).Remove(0,7);
            }

            if (Regex.IsMatch(InputBox.Text, "\\d{1}x\\d{2}"))
            {
                InputBox.Text = Regex.Replace(InputBox.Text, "\\d{1}x\\d{2}", "s0");
            }

            if (InputBox.Text.Contains("s0"))
            {
                InputBox.Text = InputBox.Text.Remove(InputBox.Text.IndexOf("s0"));
                FW_adres = FW_serial;
            }
            else
            {
                if (InputBox.Text.Contains("s1"))
                {
                    InputBox.Text = InputBox.Text.Remove(InputBox.Text.IndexOf("s1"));
                    FW_adres = FW_serial;
                }
                else FW_adres = FW_film;
            }

            for (int i = DateTime.Now.Year + 1; i >= 1950; i--)
            {
                if (InputBox.Text.Contains(i.ToString()))
                {
                    InputBox.Text = InputBox.Text.Remove(InputBox.Text.IndexOf(Convert.ToString(i)) - 1);
                    rok = i.ToString();
                    break;
                }
            }

            InputBox.Text = InputBox.Text.Replace('.', ' ').Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace("_", " ");
            WhiteMarksDeleter(InputBox);

            #endregion
            #region Advanced Filter
            Adv_filter okno = new Adv_filter();
            okno.textBox1.Text = InputBox.Text;
            if (okno.ShowDialog() == DialogResult.Yes)
            {
                if (InputBox.Text.Contains("xvid"))
                    InputBox.Text = InputBox.Text.Replace("xvid", "");

                if (InputBox.Text.Contains("dvdrip"))
                    InputBox.Text = InputBox.Text.Replace("dvdrip", "");

                if (InputBox.Text.Contains("bdrip"))
                    InputBox.Text = InputBox.Text.Replace("bdrip", "");

                if (InputBox.Text.Contains("brrip"))
                    InputBox.Text = InputBox.Text.Replace("brrip", "");

                if (InputBox.Text.Contains("bluray"))
                    InputBox.Text = InputBox.Text.Replace("bluray", "");

                if (InputBox.Text.Contains("720p") || InputBox.Text.Contains("1080p") || InputBox.Text.Contains("480p") || InputBox.Text.Contains("x264"))
                {
                    InputBox.Text = InputBox.Text.Replace("480p", "");
                    InputBox.Text = InputBox.Text.Replace("720p", "");
                    InputBox.Text = InputBox.Text.Replace("1080p", "");
                    InputBox.Text = InputBox.Text.Replace("x264", "");
                }

                if (InputBox.Text.Contains("dubb"))
                {
                    InputBox.Text = InputBox.Text.Replace("dubb", "");
                    InputBox.Text = InputBox.Text.Replace("dubbing", "");
                }

                if (InputBox.Text.Contains("lektor"))
                    InputBox.Text = InputBox.Text.Replace("lektor", "");

                if (InputBox.Text.Contains("hdtv"))
                    InputBox.Text = InputBox.Text.Replace("hdtv", "");

                if (InputBox.Text.Contains("pdtv"))
                    InputBox.Text = InputBox.Text.Replace("pdtv", "");

                if (InputBox.Text.Contains("ac3"))
                    InputBox.Text = InputBox.Text.Replace("ac3", "");

                if (InputBox.Text.IndexOf('-') <= 5)
                    InputBox.Text = InputBox.Text.Remove(0, InputBox.Text.IndexOf('-') + 1);
                else
                {
                    if (InputBox.Text.IndexOf('-') > 5)
                        InputBox.Text = InputBox.Text.Remove(InputBox.Text.IndexOf('-'));
                }

                if (System.Text.RegularExpressions.Regex.IsMatch(InputBox.Text, "cd\\d{1}"))
                {
                    InputBox.Text = System.Text.RegularExpressions.Regex.Replace(InputBox.Text, "cd\\d{1}", "");
                }

                WhiteMarksDeleter(InputBox);

                if (InputBox.Text.Contains(" pl"))
                {
                    if (InputBox.Text[InputBox.Text.Length - 2] == 'p' && InputBox.Text[InputBox.Text.Length - 1] == 'l')
                        InputBox.Text = InputBox.Text.Remove(InputBox.Text.Length - 3);
                }

                if (InputBox.Text.Contains(" en"))
                {
                    if (InputBox.Text[InputBox.Text.Length - 2] == 'e' && InputBox.Text[InputBox.Text.Length - 1] == 'n')
                        InputBox.Text = InputBox.Text.Remove(InputBox.Text.Length - 3);
                }

            #endregion
                #region Last Improvments
                Last_chance pop = new Last_chance();
                pop.textBox1.Text = InputBox.Text;
                pop.ShowDialog();
                if (pop.textBox1.ReadOnly == false)
                {
                    InputBox.Text = pop.textBox1.Text;
                    pop.textBox1.ReadOnly = true;
                }
                if (pop.DialogResult == DialogResult.OK)
                {
                    okno.Dispose();
                    pop.Dispose();
                }
            }
            else if (okno.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                this.Close();
                #endregion

            okno.Dispose();
        }
        private void MAIN_Resize(object sender, EventArgs e)
        {
            Browser.Width = this.Width - 20;
            Browser.Height = this.Height - 115;
            InputBox.Width = Wstecz_button.Location.X - 60;
            if (FWlogo != null)
                FWlogo.Location = new System.Drawing.Point((this.Width / 2) - (FWlogo.Width / 2), (this.Height / 2) - (FWlogo.Height / 2));
        }
        private void Show_Browser(bool var)
        {
            if (var == false)
            {
                Browser.Visible = true;
                FWlogo.Visible = false;
            }
        }
        private void Hide_Browser(bool var)
        {
            if (var == true)
            {
                Browser.Visible = false;
                FWlogo.Visible = true;
            }
        }
        private void Search(string address)
        {
            File_Path = new System.IO.DirectoryInfo(address);
            Choosing_Name cn = new Choosing_Name(File_Path.Parent.ToString(), File_Path.Name.Replace(File_Path.Extension, ""));
            cn.ShowDialog();
            InputBox.Text = cn.name;
            if (InputBox.Text[0] != '#')
            {
                InputBox.Text = InputBox.Text.Remove(InputBox.Text.Length - 4);
            }
            filtruj();
            Search_Click(InputBox.Text, EventArgs.Empty);
        }
        #region Drag&Drop
        private void MAIN_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }
        private void MAIN_DragDrop(object sender, DragEventArgs e)
        {
            string[] FilePath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            Search(FilePath[0]);
        }
        #endregion
        #endregion

        public MAIN(string adres)
        {
            InitializeComponent();
            #region Inicjalizacja Menu Kontekstowego
            if (Properties.Settings.Default.Context_Menu)
            {
                dodajToolStripMenuItem.Enabled = false;
                usuńToolStripMenuItem.Enabled = true;
            }
            else
            {
                dodajToolStripMenuItem.Enabled = true;
                usuńToolStripMenuItem.Enabled = false;
            }
            #endregion

            znajdźNaStronieToolStripMenuItem.Visible = false;

            if (adres == "") Browser.Visible = false;
            else
            {
                Search(adres);
            }
        }

        #region Obsługa Przeglądarki
        private void Search_Click(object sender, EventArgs e)
        {

            
            if (InputBox.Text != "")
            {
                Hide_Browser(Browser.Visible);

                if (rok == null)
                    Browser.Navigate(FW_adres + InputBox.Text);
                else
                {
                    Browser.Navigate(FW_adres + InputBox.Text + "&startYear=" + rok + " &endYear=" + rok);
                    rok = null;
                }
            }
            else
                MessageBox.Show("Wprowadz nazwę filmu bądź serialu", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Properties.Settings.Default.Location;
            open.Filter = "Pliki wideo|*.avi;*.mkv;*.rm;*.rmvb;*.mp4;*.mov;*.3gp;*.flv;*.mpeg;*.mpg|Wszystke pliki|*.*";
            open.ShowDialog();
            try
            {
                if (open.SafeFileName != "")
                {
                    Search(open.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Pewnie to nic groźnego, zwykłe niedopatrzenie...", "ale nie rób tak nigdy więcej ;P");
            }
            open.Dispose();
        }
        private void Wstecz_button_Click(object sender, EventArgs e)
        {
            if(Browser.CanGoBack)
                Browser.GoBack();
        }
        private void Dalej_button_Click(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
                Browser.GoForward();
        }
        private void Refresh_button_Click(object sender, EventArgs e)
        {
            Browser.Refresh();
        }
        private void Stop_button_Click(object sender, EventArgs e)
        {
            Browser.Stop();
        }
        private void Browser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            KatalogowanieToolStripMenuItem.Enabled = false;
            pasek_postepu.Visible = true;
            if (e.CurrentProgress <= e.MaximumProgress)
            {
                pasek_postepu.Maximum = Convert.ToInt32(e.MaximumProgress) + 1;
                pasek_postepu.Value = Convert.ToInt32(e.CurrentProgress) + 1;
            }
            pasek_postepu_Label.Text = "Wczytywanie...";

            if (pasek_postepu.Value == pasek_postepu.Maximum)
            {
                pasek_postepu_Label.Text = "Gotowe!";
                pasek_postepu.Visible = false;
                if(FW_adres == FW_film)
                    KatalogowanieToolStripMenuItem.Enabled = true;
            }
        }
        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath.Contains("search"))
            {
                System.Collections.Generic.List<string> Link_List = new System.Collections.Generic.List<string>();
                HtmlElementCollection ec = Browser.Document.GetElementsByTagName("a");
                foreach (HtmlElement element in ec)
                {
                    string cls = element.GetAttribute("className");
                    if (String.IsNullOrEmpty(cls) || !cls.Equals("searchResultTitle"))
                        continue;

                    Link_List.Add(element.GetAttribute("href"));

                    if (Link_List.Count > 1)
                    {
                        Show_Browser(Browser.Visible);
                        break;
                    }
                }

                if (Link_List.Count == 1)
                {
                    Browser.Navigate(Link_List[0]);
                    Show_Browser(Browser.Visible);
                }

                Link_List.Clear();
                Link_List = null;
            }
        }
        private void Obsluga_Entera(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) Search_Click(sender, e);
        }
        #endregion

        #region Pasek Narzędzi
        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            if (ab.ShowDialog() == DialogResult.OK) ab.Dispose();
        }
        private void instrukcjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Info info = new Info();
           if(info.ShowDialog() == DialogResult.OK) info.Dispose();
        } 
        private void ścieżkaZPlikamiWideo(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowNewFolderButton = false;
            fb.Description = "Lokalizacja plików wideo...";
            fb.SelectedPath = Properties.Settings.Default.Location;
            if (fb.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.Location = fb.SelectedPath;
                Properties.Settings.Default.Save();
            }
            fb.Dispose();
        }
        private void Katalogowanie_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(File_Path.Parent.ToString(), "\\d{1}.\\d{1} - \\w"))
            {
                char napisy_dostepne = 'n';
                string[] elements = UnifikacjaNazw.FolderName(Browser);
                OkreslanieWersji ow = new OkreslanieWersji();
                ow.ShowDialog();

                try
                {
                    string nazwa = elements[0] + " - " + elements[1] + " {" + elements[2] + "}[" + elements[3] + "][" + ow.ListBox.Text + ']';
                    char[] char_tab = new char[9] { '/', ':', '*', '?', '\u0022', '\u005c', '>', '<', '|' };
                    foreach (char c in char_tab)
                    {
                        nazwa = nazwa.Replace(c.ToString(), String.Empty);
                    }
                    DirectoryInfo folder = new DirectoryInfo(Properties.Settings.Default.Location);
                    DirectoryInfo new_path = new DirectoryInfo(Properties.Settings.Default.Location + '/' + nazwa);

                    if (!new_path.Exists)
                    {
                        try
                        {
                            folder.CreateSubdirectory(nazwa);
                            File.Move(File_Path.ToString(), new_path.ToString() + "\\" + File_Path.Name);
                            StreamWriter opis = new StreamWriter(new_path.ToString() + "\\opis.txt");
                            opis.Write(elements[4]);
                            opis.Close();
                        }

                        catch(Exception Ex)
                        {
                           MessageBox.Show(Ex.Message + "\n\nPlik nie został przeniesiony", "Błąd", MessageBoxButtons.OK);
                        }

                        #region Napisy
                        string[] subtitles = { ".txt", ".sub", ".srt" };
                        foreach (string s in subtitles)
                        {
                            FileInfo napisy = new FileInfo(File_Path.ToString().Remove(File_Path.ToString().Length - 4) + s);
                            if (napisy.Exists)
                            {
                                File.Move(napisy.ToString(), new_path.ToString() + '/' + napisy.Name);
                                napisy_dostepne = 't';
                                break;
                            }

                            napisy = null;
                        }
                        switch (napisy_dostepne)
                        {
                            case 't': MessageBox.Show("Folder został stworzony a film wraz z napisami do niego przeniesiony!\n" + nazwa); break;
                            case 'n': MessageBox.Show("Folder został stworzony a film do niego przeniesiony!\n" + nazwa); break;
                        }
                        #endregion
                    }

                    folder = new_path = null;
                    elements = null;
                    char_tab = null;
                    nazwa = null;
                    ow.Dispose();
                }
                
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "\n");
                }
            }
            else
                MessageBox.Show("Film już został skatalogowany", "Błąd!", MessageBoxButtons.OK);
        }
        private void opisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HtmlElementCollection ec = Browser.Document.GetElementsByTagName("span");
            string cls;
            foreach (System.Windows.Forms.HtmlElement element in ec)
            {
                cls = element.GetAttribute("property");

                if (!String.IsNullOrEmpty(cls) && cls.Equals("v:summary"))
                {
                    StreamWriter opis = new StreamWriter(File_Path.ToString().Replace(File_Path.Name, "") + "\\opis.txt");
                    if (!element.OuterText.Contains("Na razie nikt nie dodał"))
                    {
                        string tresc = element.OuterText;
                        if (tresc.Substring(tresc.Length - 7).Contains("więcej"))
                            tresc = tresc.Remove(tresc.Length - 7);
                        opis.Write(tresc);
                        opis.Close();
                    }
                    else
                    {
                        MessageBox.Show("FilmWeb nie dysponuje opisem tego filmu", "BRAK OPISU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                }
            }
        }
               
        #endregion

        #region Menu Kontekstowe
        private void DodajDoMenuKontekstowego(object sender, EventArgs e)
        {
            foreach (string s in ext)
                MenuKontekstowe.AddToContex(s, s + "file", "Sprawdź na FilmWeb", "fwmoviechecker", Application.ExecutablePath);
            dodajToolStripMenuItem.Enabled = false;
            usuńToolStripMenuItem.Enabled = true;
            MessageBox.Show("Dodano!");
        }
        private void UsunzMenuKontekstowego(object sender, EventArgs e)
        {
            foreach (string s in ext)
                MenuKontekstowe.RemoveFromContex(s, s + "file", "fwmoviechecker");
            dodajToolStripMenuItem.Enabled = true;
            usuńToolStripMenuItem.Enabled = false;
            MessageBox.Show("Usunięto!");
        }
        #endregion

        #region Under Development

        private void znajdźNaStronieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //** Kurestwo sypie błędami **

            Find_Text ft = new Find_Text();
            ft.document = Browser.Document;
            ft.Show();
        }



        #endregion

       

    }
}
