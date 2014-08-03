namespace FilmWeb_Movie_Checker
{
    partial class MAIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAIN));
            this.InputBox = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.Search_Button = new System.Windows.Forms.Button();
            this.FWlogo = new System.Windows.Forms.PictureBox();
            this.Open_Button = new System.Windows.Forms.Button();
            this.pasek_menu = new System.Windows.Forms.MenuStrip();
            this.plik_pasek_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.zakończToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.narzedzia_pasek_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.ścieżkaZPlikamiWideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKontekstoweToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KatalogowanieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.znajdźNaStronieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomoc_pasek_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.instrukcjaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zakończToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrukcjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Dalej_button = new System.Windows.Forms.Button();
            this.Wstecz_button = new System.Windows.Forms.Button();
            this.pasek_statusu = new System.Windows.Forms.StatusStrip();
            this.pasek_postepu_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.pasek_postepu = new System.Windows.Forms.ToolStripProgressBar();
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.Refresh_button = new System.Windows.Forms.Button();
            this.Stop_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FWlogo)).BeginInit();
            this.pasek_menu.SuspendLayout();
            this.pasek_statusu.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputBox
            // 
            this.InputBox.AllowDrop = true;
            this.InputBox.Location = new System.Drawing.Point(53, 24);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(471, 20);
            this.InputBox.TabIndex = 1;
            this.InputBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.MAIN_DragDrop);
            this.InputBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.MAIN_DragEnter);
            this.InputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Obsluga_Entera);
            this.InputBox.MouseEnter += new System.EventHandler(this.Refresh_button_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AllowDrop = true;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 28);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(35, 13);
            this.labelTitle.TabIndex = 6;
            this.labelTitle.Text = "Tytuł:";
            // 
            // Search_Button
            // 
            this.Search_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_Button.Image = global::FilmWeb_Movie_Checker.Properties.Resources.search16;
            this.Search_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Search_Button.Location = new System.Drawing.Point(856, 23);
            this.Search_Button.Name = "Search_Button";
            this.Search_Button.Size = new System.Drawing.Size(75, 23);
            this.Search_Button.TabIndex = 3;
            this.Search_Button.Text = "Szukaj    ";
            this.Search_Button.UseVisualStyleBackColor = true;
            this.Search_Button.Click += new System.EventHandler(this.Search_Click);
            // 
            // FWlogo
            // 
            this.FWlogo.Image = ((System.Drawing.Image)(resources.GetObject("FWlogo.Image")));
            this.FWlogo.Location = new System.Drawing.Point(151, 209);
            this.FWlogo.Name = "FWlogo";
            this.FWlogo.Size = new System.Drawing.Size(737, 182);
            this.FWlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.FWlogo.TabIndex = 7;
            this.FWlogo.TabStop = false;
            // 
            // Open_Button
            // 
            this.Open_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Open_Button.Image = global::FilmWeb_Movie_Checker.Properties.Resources.file16;
            this.Open_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Open_Button.Location = new System.Drawing.Point(937, 23);
            this.Open_Button.Name = "Open_Button";
            this.Open_Button.Size = new System.Drawing.Size(75, 23);
            this.Open_Button.TabIndex = 2;
            this.Open_Button.Text = " Wybierz";
            this.Open_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Open_Button.UseVisualStyleBackColor = true;
            this.Open_Button.Click += new System.EventHandler(this.Open_Click);
            // 
            // pasek_menu
            // 
            this.pasek_menu.AllowDrop = true;
            this.pasek_menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pasek_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plik_pasek_menu,
            this.narzedzia_pasek_menu,
            this.pomoc_pasek_menu});
            this.pasek_menu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.pasek_menu.Location = new System.Drawing.Point(0, 0);
            this.pasek_menu.Name = "pasek_menu";
            this.pasek_menu.Size = new System.Drawing.Size(1024, 23);
            this.pasek_menu.TabIndex = 8;
            this.pasek_menu.Text = "Menu";
            this.pasek_menu.DragDrop += new System.Windows.Forms.DragEventHandler(this.MAIN_DragDrop);
            this.pasek_menu.DragEnter += new System.Windows.Forms.DragEventHandler(this.MAIN_DragEnter);
            // 
            // plik_pasek_menu
            // 
            this.plik_pasek_menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otwórzToolStripMenuItem1,
            this.zakończToolStripMenuItem1});
            this.plik_pasek_menu.Name = "plik_pasek_menu";
            this.plik_pasek_menu.Size = new System.Drawing.Size(38, 19);
            this.plik_pasek_menu.Text = "Plik";
            // 
            // otwórzToolStripMenuItem1
            // 
            this.otwórzToolStripMenuItem1.Name = "otwórzToolStripMenuItem1";
            this.otwórzToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.otwórzToolStripMenuItem1.Text = "Otwórz";
            this.otwórzToolStripMenuItem1.Click += new System.EventHandler(this.Open_Click);
            // 
            // zakończToolStripMenuItem1
            // 
            this.zakończToolStripMenuItem1.Name = "zakończToolStripMenuItem1";
            this.zakończToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.zakończToolStripMenuItem1.Text = "Zakończ";
            this.zakończToolStripMenuItem1.Click += new System.EventHandler(this.zakończToolStripMenuItem_Click);
            // 
            // narzedzia_pasek_menu
            // 
            this.narzedzia_pasek_menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ścieżkaZPlikamiWideoToolStripMenuItem,
            this.menuKontekstoweToolStripMenuItem,
            this.KatalogowanieToolStripMenuItem,
            this.znajdźNaStronieToolStripMenuItem,
            this.opisToolStripMenuItem});
            this.narzedzia_pasek_menu.Name = "narzedzia_pasek_menu";
            this.narzedzia_pasek_menu.Size = new System.Drawing.Size(70, 19);
            this.narzedzia_pasek_menu.Text = "Narzędzia";
            // 
            // ścieżkaZPlikamiWideoToolStripMenuItem
            // 
            this.ścieżkaZPlikamiWideoToolStripMenuItem.Name = "ścieżkaZPlikamiWideoToolStripMenuItem";
            this.ścieżkaZPlikamiWideoToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.ścieżkaZPlikamiWideoToolStripMenuItem.Text = "Ścieżka z plikami wideo";
            this.ścieżkaZPlikamiWideoToolStripMenuItem.ToolTipText = "Określa folder, w którym przechowywane są pliki wideo";
            this.ścieżkaZPlikamiWideoToolStripMenuItem.Click += new System.EventHandler(this.ścieżkaZPlikamiWideo);
            // 
            // menuKontekstoweToolStripMenuItem
            // 
            this.menuKontekstoweToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dodajToolStripMenuItem,
            this.usuńToolStripMenuItem});
            this.menuKontekstoweToolStripMenuItem.Name = "menuKontekstoweToolStripMenuItem";
            this.menuKontekstoweToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.menuKontekstoweToolStripMenuItem.Text = "Menu kontekstowe";
            this.menuKontekstoweToolStripMenuItem.ToolTipText = "Dodaje opcje \"Sprawdź na FilmWeb\" do menu kontekstowego Eksploratora Windos";
            // 
            // dodajToolStripMenuItem
            // 
            this.dodajToolStripMenuItem.Name = "dodajToolStripMenuItem";
            this.dodajToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dodajToolStripMenuItem.Text = "Dodaj";
            this.dodajToolStripMenuItem.Click += new System.EventHandler(this.DodajDoMenuKontekstowego);
            // 
            // usuńToolStripMenuItem
            // 
            this.usuńToolStripMenuItem.Name = "usuńToolStripMenuItem";
            this.usuńToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.usuńToolStripMenuItem.Text = "Usuń";
            this.usuńToolStripMenuItem.Click += new System.EventHandler(this.UsunzMenuKontekstowego);
            // 
            // KatalogowanieToolStripMenuItem
            // 
            this.KatalogowanieToolStripMenuItem.Enabled = false;
            this.KatalogowanieToolStripMenuItem.Name = "KatalogowanieToolStripMenuItem";
            this.KatalogowanieToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.KatalogowanieToolStripMenuItem.Text = "Skataloguj film";
            this.KatalogowanieToolStripMenuItem.ToolTipText = resources.GetString("KatalogowanieToolStripMenuItem.ToolTipText");
            this.KatalogowanieToolStripMenuItem.Click += new System.EventHandler(this.Katalogowanie_Click);
            // 
            // znajdźNaStronieToolStripMenuItem
            // 
            this.znajdźNaStronieToolStripMenuItem.Name = "znajdźNaStronieToolStripMenuItem";
            this.znajdźNaStronieToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.znajdźNaStronieToolStripMenuItem.Text = "Znajdź na stronie";
            this.znajdźNaStronieToolStripMenuItem.Visible = false;
            this.znajdźNaStronieToolStripMenuItem.Click += new System.EventHandler(this.znajdźNaStronieToolStripMenuItem_Click);
            // 
            // opisToolStripMenuItem
            // 
            this.opisToolStripMenuItem.Name = "opisToolStripMenuItem";
            this.opisToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.opisToolStripMenuItem.Text = "Opis";
            this.opisToolStripMenuItem.Click += new System.EventHandler(this.opisToolStripMenuItem_Click);
            // 
            // pomoc_pasek_menu
            // 
            this.pomoc_pasek_menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instrukcjaToolStripMenuItem1,
            this.oProgramieToolStripMenuItem1});
            this.pomoc_pasek_menu.Name = "pomoc_pasek_menu";
            this.pomoc_pasek_menu.Size = new System.Drawing.Size(57, 19);
            this.pomoc_pasek_menu.Text = "Pomoc";
            // 
            // instrukcjaToolStripMenuItem1
            // 
            this.instrukcjaToolStripMenuItem1.Name = "instrukcjaToolStripMenuItem1";
            this.instrukcjaToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.instrukcjaToolStripMenuItem1.Text = "Instrukcja";
            this.instrukcjaToolStripMenuItem1.Click += new System.EventHandler(this.instrukcjaToolStripMenuItem_Click);
            // 
            // oProgramieToolStripMenuItem1
            // 
            this.oProgramieToolStripMenuItem1.Name = "oProgramieToolStripMenuItem1";
            this.oProgramieToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.oProgramieToolStripMenuItem1.Text = "O Programie...";
            this.oProgramieToolStripMenuItem1.Click += new System.EventHandler(this.oProgramieToolStripMenuItem_Click);
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otwórzToolStripMenuItem,
            this.zakończToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // otwórzToolStripMenuItem
            // 
            this.otwórzToolStripMenuItem.Name = "otwórzToolStripMenuItem";
            this.otwórzToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.otwórzToolStripMenuItem.Text = "Otwórz";
            // 
            // zakończToolStripMenuItem
            // 
            this.zakończToolStripMenuItem.Name = "zakończToolStripMenuItem";
            this.zakończToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.zakończToolStripMenuItem.Text = "Zakończ";
            this.zakończToolStripMenuItem.Click += new System.EventHandler(this.zakończToolStripMenuItem_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instrukcjaToolStripMenuItem,
            this.oProgramieToolStripMenuItem});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // instrukcjaToolStripMenuItem
            // 
            this.instrukcjaToolStripMenuItem.Name = "instrukcjaToolStripMenuItem";
            this.instrukcjaToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.instrukcjaToolStripMenuItem.Text = "Instrukcja";
            this.instrukcjaToolStripMenuItem.Click += new System.EventHandler(this.instrukcjaToolStripMenuItem_Click);
            // 
            // oProgramieToolStripMenuItem
            // 
            this.oProgramieToolStripMenuItem.Name = "oProgramieToolStripMenuItem";
            this.oProgramieToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.oProgramieToolStripMenuItem.Text = "O programie";
            this.oProgramieToolStripMenuItem.Click += new System.EventHandler(this.oProgramieToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // Dalej_button
            // 
            this.Dalej_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Dalej_button.Image = global::FilmWeb_Movie_Checker.Properties.Resources.forward_arrow;
            this.Dalej_button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Dalej_button.Location = new System.Drawing.Point(775, 23);
            this.Dalej_button.Name = "Dalej_button";
            this.Dalej_button.Size = new System.Drawing.Size(75, 23);
            this.Dalej_button.TabIndex = 4;
            this.Dalej_button.Text = "Dalej     ";
            this.Dalej_button.UseVisualStyleBackColor = true;
            this.Dalej_button.Click += new System.EventHandler(this.Dalej_button_Click);
            // 
            // Wstecz_button
            // 
            this.Wstecz_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Wstecz_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Wstecz_button.Image = global::FilmWeb_Movie_Checker.Properties.Resources.back_arrow;
            this.Wstecz_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Wstecz_button.Location = new System.Drawing.Point(532, 23);
            this.Wstecz_button.Name = "Wstecz_button";
            this.Wstecz_button.Size = new System.Drawing.Size(75, 23);
            this.Wstecz_button.TabIndex = 5;
            this.Wstecz_button.Text = "Wstecz";
            this.Wstecz_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Wstecz_button.UseVisualStyleBackColor = true;
            this.Wstecz_button.Click += new System.EventHandler(this.Wstecz_button_Click);
            // 
            // pasek_statusu
            // 
            this.pasek_statusu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasek_postepu_Label,
            this.pasek_postepu});
            this.pasek_statusu.Location = new System.Drawing.Point(0, 540);
            this.pasek_statusu.Name = "pasek_statusu";
            this.pasek_statusu.Size = new System.Drawing.Size(1024, 22);
            this.pasek_statusu.TabIndex = 9;
            this.pasek_statusu.Text = "Pasek Statusu";
            // 
            // pasek_postepu_Label
            // 
            this.pasek_postepu_Label.Name = "pasek_postepu_Label";
            this.pasek_postepu_Label.Size = new System.Drawing.Size(51, 17);
            this.pasek_postepu_Label.Text = "Gotowe!";
            // 
            // pasek_postepu
            // 
            this.pasek_postepu.Name = "pasek_postepu";
            this.pasek_postepu.Size = new System.Drawing.Size(100, 16);
            this.pasek_postepu.ToolTipText = "Wskazuje procent wczytania strony";
            this.pasek_postepu.Visible = false;
            // 
            // Browser
            // 
            this.Browser.AllowWebBrowserDrop = false;
            this.Browser.Location = new System.Drawing.Point(1, 54);
            this.Browser.MinimumSize = new System.Drawing.Size(800, 300);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(1024, 488);
            this.Browser.TabIndex = 10;
            this.Browser.Visible = false;
            this.Browser.WebBrowserShortcutsEnabled = false;
            this.Browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.Browser_DocumentCompleted);
            this.Browser.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.Browser_ProgressChanged);
            // 
            // Refresh_button
            // 
            this.Refresh_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Refresh_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Refresh_button.Image = global::FilmWeb_Movie_Checker.Properties.Resources.refresh1;
            this.Refresh_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Refresh_button.Location = new System.Drawing.Point(613, 23);
            this.Refresh_button.Name = "Refresh_button";
            this.Refresh_button.Size = new System.Drawing.Size(75, 23);
            this.Refresh_button.TabIndex = 11;
            this.Refresh_button.Text = "Odśwież";
            this.Refresh_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Refresh_button.UseVisualStyleBackColor = true;
            this.Refresh_button.Click += new System.EventHandler(this.Refresh_button_Click);
            // 
            // Stop_button
            // 
            this.Stop_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Stop_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Stop_button.Image = global::FilmWeb_Movie_Checker.Properties.Resources.error1;
            this.Stop_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Stop_button.Location = new System.Drawing.Point(694, 23);
            this.Stop_button.Name = "Stop_button";
            this.Stop_button.Size = new System.Drawing.Size(75, 23);
            this.Stop_button.TabIndex = 12;
            this.Stop_button.Text = "Stop   ";
            this.Stop_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Stop_button.UseVisualStyleBackColor = true;
            this.Stop_button.Click += new System.EventHandler(this.Stop_button_Click);
            // 
            // MAIN
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 562);
            this.Controls.Add(this.Stop_button);
            this.Controls.Add(this.Refresh_button);
            this.Controls.Add(this.pasek_menu);
            this.Controls.Add(this.pasek_statusu);
            this.Controls.Add(this.Wstecz_button);
            this.Controls.Add(this.Dalej_button);
            this.Controls.Add(this.Open_Button);
            this.Controls.Add(this.FWlogo);
            this.Controls.Add(this.Browser);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.Search_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.pasek_menu;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "MAIN";
            this.Text = "FilmWeb Movie Checker";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MAIN_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MAIN_DragEnter);
            this.Resize += new System.EventHandler(this.MAIN_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.FWlogo)).EndInit();
            this.pasek_menu.ResumeLayout(false);
            this.pasek_menu.PerformLayout();
            this.pasek_statusu.ResumeLayout(false);
            this.pasek_statusu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button Search_Button;
        private System.Windows.Forms.PictureBox FWlogo;
        private System.Windows.Forms.Button Open_Button;
        private System.Windows.Forms.MenuStrip pasek_menu;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otwórzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrukcjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button Dalej_button;
        private System.Windows.Forms.Button Wstecz_button;
        private System.Windows.Forms.ToolStripMenuItem plik_pasek_menu;
        private System.Windows.Forms.ToolStripMenuItem otwórzToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pomoc_pasek_menu;
        private System.Windows.Forms.ToolStripMenuItem instrukcjaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip pasek_statusu;
        private System.Windows.Forms.ToolStripProgressBar pasek_postepu;
        private System.Windows.Forms.ToolStripStatusLabel pasek_postepu_Label;
        private System.Windows.Forms.ToolStripMenuItem narzedzia_pasek_menu;
        private System.Windows.Forms.ToolStripMenuItem ścieżkaZPlikamiWideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuKontekstoweToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem;
        public System.Windows.Forms.TextBox InputBox;
        public System.Windows.Forms.WebBrowser Browser;
        private System.Windows.Forms.ToolStripMenuItem KatalogowanieToolStripMenuItem;
        private System.Windows.Forms.Button Refresh_button;
        private System.Windows.Forms.Button Stop_button;
        private System.Windows.Forms.ToolStripMenuItem znajdźNaStronieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opisToolStripMenuItem;
        
    }
}