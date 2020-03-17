using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace C968_WGU
{
    /// <summary>
    /// Interaction logic for EditPartForm.xaml
    /// </summary>
    public partial class EditPartForm : Window
    {
        public src.Inventory EditRestoreInventory;
        public src.Inventory EditWorkingInventory;
        public src.Validator EditPrtValidator = new src.Validator();
        public int editID;
        public int editIndex;
        public bool localEdit;

        // Constructor
        public EditPartForm(int partNum)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            editID = partNum;
        }

        // On Window Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EditIDFinder();
            EditPartPreFiller();
        }

        // Switches Label Based on Part Type
        private void EditIHInput_Checked(object sender, RoutedEventArgs e)
        { EditSwitcherLabel.Content = "Machine ID: "; EditSwitcherInput.Text = ""; }

        private void EditOSInput_Checked(object sender, RoutedEventArgs e)
        { EditSwitcherLabel.Content = "Company Name: "; EditSwitcherInput.Text = ""; }

        // Finds Index to Modify
        private void EditIDFinder()
        {
            if (localEdit == true)
            {
                for (int i = 0; i < EditWorkingInventory.AllIHParts.Count; i++)
                {
                    if (editID == EditWorkingInventory.AllIHParts[i].partID)
                    {
                        editIndex = EditWorkingInventory.AllIHParts.IndexOf(EditWorkingInventory.AllIHParts[i]);
                        Console.WriteLine($"Index Selected: {editIndex}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < EditWorkingInventory.AllOSParts.Count; i++)
                {
                    if (editID == EditWorkingInventory.AllOSParts[i].partID)
                    {
                        editIndex = EditWorkingInventory.AllOSParts.IndexOf(EditWorkingInventory.AllOSParts[i]);
                        Console.WriteLine($"Index Selected: {editIndex}");
                    }
                }
            }
        }

        // Pre Fills Form
        private void EditPartPreFiller()
        {
            EditPrtIDLabel.Content = "Part ID: " + editID;

            if (localEdit == true)
            {
                EditIHInput.IsChecked = true;
                EditNameInput.Text = EditWorkingInventory.AllIHParts[editIndex].partName;
                EditPriceInput.Text = EditWorkingInventory.AllIHParts[editIndex].partPrice.ToString();
                EditQtyInput.Text = EditWorkingInventory.AllIHParts[editIndex].partsInStock.ToString();
                EditMinInput.Text = EditWorkingInventory.AllIHParts[editIndex].partMin.ToString();
                EditMaxInput.Text = EditWorkingInventory.AllIHParts[editIndex].partMax.ToString();
                EditSwitcherInput.Text = EditWorkingInventory.AllIHParts[editIndex].machineID.ToString();
            }
            else
            {
                EditOSInput.IsChecked = true;
                EditNameInput.Text = EditWorkingInventory.AllOSParts[editIndex].partName;
                EditPriceInput.Text = EditWorkingInventory.AllOSParts[editIndex].partPrice.ToString();
                EditQtyInput.Text = EditWorkingInventory.AllOSParts[editIndex].partsInStock.ToString();
                EditMinInput.Text = EditWorkingInventory.AllOSParts[editIndex].partMin.ToString();
                EditMaxInput.Text = EditWorkingInventory.AllOSParts[editIndex].partMax.ToString();
                EditSwitcherInput.Text = EditWorkingInventory.AllOSParts[editIndex].companyName;
            }
        }

        /*
        Button Methods 
        */

        // Save Edited Part
        private void SaveEditBtn_Click(object sender, RoutedEventArgs e)
        {
            bool finalLocal = false;
            if (EditIHInput.IsChecked == true) 
            { finalLocal = true; }

            string[] inputFormTxt = { EditNameInput.Text, EditPriceInput.Text, EditQtyInput.Text, EditMinInput.Text, EditMaxInput.Text, EditSwitcherInput.Text };
            EditPrtValidator.isLocalPart = finalLocal;
            bool isValidInput_e = EditPrtValidator.IsValidForm(false, inputFormTxt);

            if (isValidInput_e == false)
            { MessageBox.Show("Error Invalid Form Data"); }
            else
            {
                src.Part.BasePart EditedPart = new src.Part.BasePart();

                EditedPart.basePartID = editID;
                EditedPart.basePartName = EditNameInput.Text;
                EditedPart.basePartPrice = Decimal.Parse(EditPriceInput.Text) + 0.00M;
                EditedPart.basePartQty = Int32.Parse(EditQtyInput.Text);
                EditedPart.basePartMin = Int32.Parse(EditMinInput.Text);
                EditedPart.basePartMax = Int32.Parse(EditMaxInput.Text);

                if (finalLocal == true)
                {
                    src.InHouse editIH = new src.InHouse(EditedPart, Int32.Parse(EditSwitcherInput.Text));
                    EditWorkingInventory.UpdatePart((src.Part)editIH, editID);
                }
                else 
                { 
                    src.Outsourced editOS = new src.Outsourced(EditedPart, EditSwitcherInput.Text);
                    EditWorkingInventory.UpdatePart((src.Part)editOS, editID);
                }

                MainWindow EditPartSaved = new MainWindow();
                EditPartSaved.mainInventory = EditWorkingInventory;
                EditPartSaved.Show();
                Close();
            }
        }

        // Cancel Changes
        private void CancelEditBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow EditPartCancelled = new MainWindow();
            EditPartCancelled.mainInventory = EditRestoreInventory;
            EditPartCancelled.Show();
            Close();
        }
    }
}
