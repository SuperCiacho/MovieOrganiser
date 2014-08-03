using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mshtml;

namespace FilmWeb_Movie_Checker
{
    public partial class Find_Text : Form
    {
        public HtmlDocument document;
        private IHTMLTxtRange rng;

        public Find_Text()
        {
            InitializeComponent();
        }

        private string GetSelection()
        {
            IHTMLDocument2 doc = (IHTMLDocument2)document.DomDocument;
            IHTMLSelectionObject sel = doc.selection;
            IHTMLTxtRange range = (IHTMLTxtRange)sel.createRange();
            return range.text;
        }

        private bool FindFirst(string text)
        {
            IHTMLDocument2 doc = (IHTMLDocument2)document.DomDocument;
            IHTMLSelectionObject sel = (IHTMLSelectionObject)doc.selection;
            sel.empty(); // get an empty selection, so we start from the beginning
            rng = (IHTMLTxtRange)sel.createRange();

            if (rng.findText(text, 1, 0))
            {
                rng.select();
                return true;
            }
            return false;

        }

        private bool FindNext(string text)
        {
            IHTMLDocument2 doc = (IHTMLDocument2)document.DomDocument;
            IHTMLSelectionObject sel = (IHTMLSelectionObject)doc.selection;
            rng = (IHTMLTxtRange)sel.createRange();
            rng.collapse(false); // collapse the current selection so we start from the end of the previous range
            
            if (rng.findText(text, 1000000000, 0))
            {
                rng.select();
                return true;
            }
            return false;
        }

        private bool FindPrev(string text)
        {
            IHTMLDocument2 doc = (IHTMLDocument2)document.DomDocument;
            IHTMLSelectionObject sel = (IHTMLSelectionObject)doc.selection;
            //rng = (IHTMLTxtRange)sel.createRange();
            rng.collapse(false); // collapse the current selection so we start from the end of the previous range

            if (rng.findText(text, 1000000000, 1))
            {
                rng.select();
                return true;
            }
            return false;
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            FindNext(textBox1.Text);    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (document != null)
            {
                GetSelection();
                FindFirst(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FindPrev(textBox1.Text);
        }
    }
}
