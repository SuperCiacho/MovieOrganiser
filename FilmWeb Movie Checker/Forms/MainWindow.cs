using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using System.Security.Principal;

namespace FilmWeb_Movie_Checker
{
    public partial class MainWindow : Form
    {
        #region Zmienne
        #region Adresy
        private const string FW_Serial = "http://www.filmweb.pl/search/serial?q=";
        private const string FW_Film = "http://www.filmweb.pl/search/film?q=";
        private const string FW_GeneralSearch = "http://www.filmweb.pl/search?q=";
        private string FW_Adres {get; set; }
        #endregion
        public DirectoryInfo FilePath { get; set; }
        private string Year { get; set; }
        private string HD { get; set; }
        private static readonly string[] Extensions = new string[] { "avi", "mp4", "mkv", "rm", "rmvb", "3gp", "flv", "mpeg", "mpg", "mov" };
        private static readonly string[] Subtitles = new string[] { ".txt", ".sub", ".srt" };
        private static readonly List<string> Keywords = new List<string>() { "xvid", "dvdrip", "x264", "bdrip", "brrip", "bluray", "dubbing", "dubb", "lektor", "hdtv", "pdtv", "ac3", "dts", "(", ")", "[", "]" };
        private readonly List<Char> InvalidChars;
        #endregion

        #region Funkcje 
        private void MAIN_Resize(object sender, EventArgs e)
        {
            Browser.Width = this.Width - 20;
            Browser.Height = this.Height - 115;
            InputBox.Width = Wstecz_button.Location.X - 60;
            if (FWlogo != null)
                FWlogo.Location = new System.Drawing.Point((this.Width / 2) - (FWlogo.Width / 2), (this.Height / 2) - (FWlogo.Height / 2));
        }
        private void Show_Browser(bool show)
        {
            Browser.Visible = show;
            FWlogo.Visible = !show;
        }
        private void ChoosingName(string adres)
        {
            FilePath = new DirectoryInfo(adres);
            ChooseName Filter = new ChooseName(NameFilter(FilePath.Parent.ToString().ToLower()), NameFilter(Path.GetFileNameWithoutExtension(adres).ToLower()));
            var result = Filter.ShowDialog();
            if (result == DialogResult.Yes) InputBox.Text = Filter.name;
            else return;

            Search();
        }
        private void Search()
        {
            if (InputBox.Text != String.Empty)
            {
                KatalogowanieToolStripMenuItem.Enabled = false;
                opisToolStripMenuItem.Enabled = false;

                Show_Browser(false);

                if (Year == null) Browser.Navigate(FW_Adres + InputBox.Text);
                else
                {
                    Browser.Navigate(String.Format("{0}{1}&startYear={2}&endYear={2}",FW_Adres, InputBox.Text, Year));
                    Year = null;
                }
            }
            else
                MessageBox.Show("Wprowadz nazwę filmu bądź serialu", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private string NameFilter(string name)
        {
            if (name == string.Empty) return name;

            int tmpInt = 0;
            string tmpString = null;

            //if (Regex.IsMatch(name, @"\d{3}p{1}|\d{4}p{1}"))
            if (Regex.IsMatch(name, @"\d{3,4}p{1}"))
                HD = Regex.Match(name, @"\d{3,4}p{1}").Value;

            if (Regex.IsMatch(name, @"\d{4}"))
            {
                tmpString = Regex.Match(name, @"\d{4}").Value;
                if (int.Parse(tmpString) > 1900)
                {
                    name = name.Remove(name.IndexOf(tmpString) - 1);
                    Year = tmpString;
                }
            }

            StringBuilder sb = new StringBuilder(name);

            if (Regex.IsMatch(name, @"^.{0,5}-{1}|-{1}.*$"))
                sb.Replace(Regex.Match(name, @"^.{0,5}-{1}|-{1}.*$").Value, "");

            if (Regex.IsMatch(name, @"\{{1}.*\}{1}"))
                sb.Replace(Regex.Match(name, @"\{{1}.*\}{1}").Value, "");

            Keywords.ForEach(x => sb.Replace(x, " "));
            sb.Replace('.', ' ').Replace('_', ' ');

            if (HD != null)
                sb.Replace(HD, string.Empty);

            if (Regex.IsMatch(name, "\\d{1}x\\d{2}"))
                sb.Replace(Regex.Match(name, "\\d{1}x\\d{2}").Value, "s0");

            name = sb.ToString();

            if (name.Contains("s0"))
            {
                tmpInt = name.IndexOf("s0");
                name = name.Remove(tmpInt);
                FW_Adres = FW_Serial;
            }
            else if (name.Contains("s1"))
            {
                tmpInt = name.IndexOf("s1");
                name = name.Remove(tmpInt);
                FW_Adres = FW_Serial;
            }
            else FW_Adres = FW_Film;

            if (Regex.IsMatch(name, "cd\\d{1}"))
            {
                name = Regex.Replace(name, "cd\\d{1}", "");
            }

            if (Regex.IsMatch(name, " pl$| en$"))
            {
                name = name.Remove(name.Length - 3, 3);
            }

            name = name.TrimStart(' ');
            name = name.TrimEnd(' ');

            tmpString = null;
            tmpInt = 0;

            return name;
        }
        private void CatalogFile()
        {
            if (!Regex.IsMatch(FilePath.Parent.ToString(), @"^\d{1}.\d{1} - \w"))
            {
                bool subsAvailable = false;
                string[] elements = UnifikacjaNazw.FolderName(Browser.Document);
                using (var translationTechnique = new TranslationTechnique())
                {
                    if (translationTechnique.ShowDialog() == DialogResult.Cancel) return;

                    try
                    {
                        string type = translationTechnique.SelectedValue;
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("{0} - {1} [{2}][{3}][{4}]", elements[0], elements[1], elements[2], elements[3], type);
                        if (HD != null) sb.Append(String.Format("[{0}]", HD));

                        InvalidChars.ForEach(c => sb.Replace(c.ToString(), String.Empty));


                        string nazwa = sb.ToString();                        
                        string new_path = Path.Combine(Properties.Settings.Default.Location, nazwa);

                        if (!Directory.Exists(new_path))
                        {
                            try
                            {
                                new DirectoryInfo(Properties.Settings.Default.Location).CreateSubdirectory(nazwa);
                                FilePath.MoveTo(Path.Combine(new_path, FilePath.Name));
                                Download_Description(new_path);

                                #region Napisy
                                if (type == "Napisy")
                                    foreach (string s in Subtitles)
                                    {
                                        string subs = Path.ChangeExtension(FilePath.ToString(), s);
                                        if (File.Exists(subs))
                                        {
                                            File.Move(subs, Path.Combine(new_path, Path.GetFileName(subs)));
                                            subsAvailable = true;
                                            subs = null;
                                            break;
                                        }
                                    }

                                MessageBox.Show(String.Format("Folder został stworzony, a film{1} do niego przeniesiony!\n{0}", nazwa,
                                    subsAvailable ? " wraz z napisami" : String.Empty));

                                #endregion
                                
                                var result = MessageBox.Show("Czy chcesz usunąć poprzedni folder, w któtrym znajdował się film?", "Usuwanie poprzedniego folderu", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    DeleteParentDirectory();
                                }
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show(Ex.Message + "\n\nPlik nie został przeniesiony", "Błąd", MessageBoxButtons.OK);
                                Directory.Delete(new_path);
                            }
                            new_path = null;
                            elements = null;
                            nazwa = null;
                            HD = null;
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message + "\n");
                    }
                }
            }
            else
                MessageBox.Show("Film już został skatalogowany", "Błąd!", MessageBoxButtons.OK);
        }

        private void DeleteParentDirectory()
        {
            var parent = FilePath.Parent;
            var filesInDir = parent.GetFiles();
            var dirsInDir = parent.GetDirectories();
            bool deleteFlag = filesInDir.Length == 0 && dirsInDir.Length == 0;

            if (!deleteFlag)
            {
                var fileList = new StringBuilder();

                if (dirsInDir.Length > 0)
                {
                    fileList.Append("Foldery:");
                    foreach (var elem in dirsInDir) fileList.AppendLine("\t" + elem.Name);
                }

                if (filesInDir.Length > 0)
                {
                    fileList.Append("Pliki:");
                    foreach (var elem in filesInDir) fileList.AppendLine("\t" + elem.Name);
                }

                 var result = MessageBox.Show("Folder nie jest pusty. Znajdują się tam poniższe pliki i foldery:\n" + fileList.ToString()
                    + "\nCzy wciąż chcesz go usunąć?", "Folder nie jest pusty.", MessageBoxButtons.YesNo);

                deleteFlag = result == DialogResult.Yes;
            }

            if (deleteFlag) parent.Delete(true);
        }
        private void Download_Description(string path)
        {
            HtmlElementCollection ec = Browser.Document.GetElementsByTagName("p");
            foreach (HtmlElement element in ec)
            {
                string cls = element.GetAttribute("property");

                if (!String.IsNullOrEmpty(cls) && cls.Equals("v:summary"))
                    if (!element.OuterText.Contains("Na razie nikt nie dodał"))
                    {
                        using (var description = new StreamWriter(path + @"\opis.txt"))
                        {
                            string content = element.OuterText;
                            if (content.Substring(content.Length - 7).Contains("więcej"))
                                content = content.Remove(content.Length - 7);
                            description.Write(content);
                            description.Flush();
                            description.Close();
                        }
                        break;
                    }
            }
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
            ChoosingName(FilePath[0]);
        }
        #endregion
        #endregion

        public MainWindow(string adres)
        {
            InitializeComponent();
            #region Inicjalizacja Menu Kontekstowego
            bool isContextMenuAdded = Properties.Settings.Default.Context_Menu;
            dodajToolStripMenuItem.Enabled = !isContextMenuAdded;
            usuńToolStripMenuItem.Enabled = isContextMenuAdded;
            #endregion

            znajdźNaStronieToolStripMenuItem.Visible = false;

            InvalidChars = new System.Collections.Generic.List<char>();
            InvalidChars.AddRange(Path.GetInvalidPathChars());
            InvalidChars.AddRange(Path.GetInvalidFileNameChars());

            FW_Adres = FW_GeneralSearch;

            if (adres == string.Empty) Browser.Visible = false;
            else ChoosingName(adres);
        }

        #region Obsługa Przeglądarki
        private void Search_Click(object sender, EventArgs e)
        {
            Search();
        }
        private void Open_Click(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Properties.Settings.Default.Location;
                open.Filter = "Pliki wideo|*.avi;*.mkv;*.rm;*.rmvb;*.mp4;*.mov;*.3gp;*.flv;*.mpeg;*.mpg|Wszystke pliki|*.*";

                if (open.ShowDialog() == DialogResult.OK)
                    ChoosingName(open.FileName);
            }
        }
        private void Wstecz_button_Click(object sender, EventArgs e)
        {
            if(Browser.CanGoBack) Browser.GoBack();
        }
        private void Dalej_button_Click(object sender, EventArgs e)
        {
            if (Browser.CanGoForward) Browser.GoForward();
        }
        private void Refresh_button_Click(object sender, EventArgs e)
        {
            Browser.Refresh();
            pasek_postepu.Visible = true;
            pasek_postepu_Label.Text = "Wczytywanie...";
        }
        private void Stop_button_Click(object sender, EventArgs e)
        {
            Browser.Stop();
        }
        private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            pasek_postepu.Visible = true;
            pasek_postepu_Label.Text = "Wczytywanie...";
        }
        private void Browser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            try
            {
                pasek_postepu.Maximum = (int)e.MaximumProgress;
                pasek_postepu.Value = (int)e.CurrentProgress;
            }
            catch { }
        }
        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            pasek_postepu_Label.Text = "Gotowe!";
            pasek_postepu.Visible = false;
            if (FW_Adres == FW_Film)
            {
                KatalogowanieToolStripMenuItem.Enabled = true;
                opisToolStripMenuItem.Enabled = true;
            }

            if (e.Url.AbsolutePath.Contains("search"))
            {
                var Link_List = new System.Collections.Generic.List<string>(2);
                var anchorsCollection = Browser.Document.GetElementsByTagName("a");

                foreach (HtmlElement anchor in anchorsCollection)
                {
                    string cls = anchor.GetAttribute("className");
                    if (String.IsNullOrEmpty(cls) || !cls.Equals("hdr hdr-medium hitTitle"))
                        continue;
                    
                    Link_List.Add(anchor.GetAttribute("href"));

                    if (Link_List.Count > 1) break;
                }

                if (Link_List.Count == 1) Browser.Navigate(Link_List[0]);

                Show_Browser(true);

                Link_List.Clear();
                Link_List = null;
                anchorsCollection = null;
            }
        }
        private void Obsluga_Entera(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) Search();
        }
        #endregion

        #region Pasek Narzędzi
        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
        private void instrukcjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Info().ShowDialog();
        } 
        private void ścieżkaZPlikamiWideo(object sender, EventArgs e)
        {
            using (var fb = new FolderBrowserDialog())
            {
                fb.ShowNewFolderButton = false;
                fb.Description = "Lokalizacja plików wideo...";
                fb.SelectedPath = Properties.Settings.Default.Location;
                if (fb.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.Location = fb.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }
        private void Katalogowanie_Click(object sender, EventArgs e)
        {
            CatalogFile();
        }
        private void opisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Download_Description(FilePath.ToString().Replace(FilePath.Name, String.Empty));
        }
               
        #endregion

        #region Menu Kontekstowe - do dopracowania
        private void DodajDoMenuKontekstowego(object sender, EventArgs e)
        {
            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                foreach (string ext in Extensions)
                    MenuKontekstowe.AddToContex(ext, ext + "file", "Sprawdź na FilmWeb", "fwmoviechecker", Application.ExecutablePath);
                dodajToolStripMenuItem.Enabled = false;
                usuńToolStripMenuItem.Enabled = true;
                MessageBox.Show("Dodano!");
            }
            else
            {
                MessageBox.Show("Aby dodać opcje do menu kontekstowego wymagane są prawa administratora.", "Brak uprawnień", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void UsunzMenuKontekstowego(object sender, EventArgs e)
        {
            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                foreach (string ext in Extensions)
                    MenuKontekstowe.RemoveFromContex(ext, ext + "file", "fwmoviechecker");
                dodajToolStripMenuItem.Enabled = true;
                usuńToolStripMenuItem.Enabled = false;
                MessageBox.Show("Usunięto!");
            }
            else
            {
                MessageBox.Show("Aby usunąć opcje z menu kontekstowego wymagane są prawa administratora.", "Brak uprawnień", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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
