using System;
using System.Windows.Forms;

namespace FilmWeb_Movie_Checker
{
    public partial class TranslationTechnique : Form
    {
        public string SelectedValue { get; private set; }

        public TranslationTechnique()
        {
            InitializeComponent();
        }

        private void Apply_button_Click(object sender, EventArgs e)
        {
            if (ListBox.CheckedItems.Count == 0)
                MessageBox.Show("Nie zaznaczyłeś żadnej z możliwości!", "Niedopatrzenie!", MessageBoxButtons.OK);
            else
            {
                SelectedValue = ListBox.Text;
                this.Close();
            }
        }

        private void ListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ListBox.CheckedItems.Count == 1)
            {
                Boolean isCheckedItemBeingUnchecked = (e.CurrentValue == CheckState.Checked);
                if (isCheckedItemBeingUnchecked)
                {
                    e.NewValue = CheckState.Checked;
                }
                else
                {
                    Int32 checkedItemIndex = ListBox.CheckedIndices[0];
                    ListBox.ItemCheck -= ListBox_ItemCheck;
                    ListBox.SetItemChecked(checkedItemIndex, false);
                    ListBox.ItemCheck += ListBox_ItemCheck;
                }

                return;
            }
        }

        private void Obsluga_Entera(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) Apply_button_Click(null, null);
        }
    }
}
