using System;
using System.Windows.Forms;

namespace FilmWeb_Movie_Checker
{
    class UnifikacjaNazw
    {
        public static string[] FolderName(HtmlDocument document)
        {
            const string MovieName = "v:name";
            const string MovieGenres = "/search/film?genreIds";
            const string MovieNote = "v:average";
            const string MovieYear = "filmYear";
            const string HtmlElementProperty = "property";
            const string HtmlAnchor = "href";

            string[] tab = new string[4];
            string cls;
            HtmlElementCollection HtmlCollection = null;

            HtmlElement html_year = document.GetElementById(MovieYear);
            tab[3] = html_year.OuterText;
            tab[3] = tab[3].Replace("(", "").Replace(")", "").Replace(" ", "");
            html_year = null;

            HtmlCollection = document.GetElementsByTagName("a");
            foreach (HtmlElement element in HtmlCollection)
            {
                cls = element.GetAttribute(HtmlElementProperty);
                if (!String.IsNullOrEmpty(cls) && cls.Equals(MovieName))
                    tab[1] = element.OuterText;

                cls = element.GetAttribute(HtmlAnchor);
                if (!String.IsNullOrEmpty(cls) && cls.Contains(MovieGenres))
                    tab[2] += element.OuterText + ',';
            }

            HtmlCollection = document.GetElementsByTagName("strong");

            foreach (HtmlElement element in HtmlCollection)
            {
                cls = element.GetAttribute(HtmlElementProperty);
                if (!String.IsNullOrEmpty(cls) && cls.Equals(MovieNote))
                    tab[0] = element.OuterText.Replace(',', '.').TrimEnd(' ').TrimStart(' ');
            }

            HtmlCollection = null;
            tab[2] = tab[2].TrimEnd(',');

            return tab;
        }

    }
}
