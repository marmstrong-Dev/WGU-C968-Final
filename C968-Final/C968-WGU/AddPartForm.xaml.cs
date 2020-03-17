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
    /// Interaction logic for AddPartForm.xaml
    /// </summary>
    public partial class AddPartForm : Window
    {
        public src.Inventory NewRestoreInventory;
        public src.Inventory NewWorkingInventory;
        public src.Validator NewPartValidator = new src.Validator();
        public bool localPart;

        // Constructor
        public AddPartForm()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        // On Window Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            AddPrtIDLabel.Content = $"Part ID: {NewWorkingInventory.partCount + 1}";
            AddIHSelect.IsChecked = true;
        }

        // Switches Label Based on Part Type
        private void AddOSSelect_Checked(object sender, RoutedEventArgs e)
        { AddSwitcherLabel.Content = "Company Name: "; }

        private void AddIHSelect_Checked(object sender, RoutedEventArgs e)
        { AddSwitcherLabel.Content = "Machine ID: "; }

        // Builds New Part and Passes to Inventory
        private bool NewPartBuilder()
        {
            if (AddIHSelect.IsChecked == true)
            { localPart = true; }
            else
            { localPart = false; }

            string[] inputFormTxt = { AddPartNameInput.Text, AddPartPriceInput.Text, AddPartQtyInput.Text, AddMinInput.Text, AddMaxInput.Text, AddSwitcherInput.Text };
            NewPartValidator.isLocalPart = localPart;
            bool isValidPartInput = NewPartValidator.IsValidForm(false, inputFormTxt);

            if (isValidPartInput == false)
            { MessageBox.Show("Error Invalid Form Data"); }
            else
            {
                src.Part.BasePart AddedNewPart = new src.Part.BasePart();
                AddedNewPart.basePartID = NewWorkingInventory.partCount + 1;
                AddedNewPart.basePartName = AddPartNameInput.Text;
                AddedNewPart.basePartPrice = Decimal.Parse(AddPartPriceInput.Text) + 0.00M;
                AddedNewPart.basePartQty = Int32.Parse(AddPartQtyInput.Text);
                AddedNewPart.basePartMin = Int32.Parse(AddMinInput.Text);
                AddedNewPart.basePartMax = Int32.Parse(AddMaxInput.Text);

                if (localPart == true)
                {
                    src.InHouse addedIH = new src.InHouse(AddedNewPart, Int32.Parse(AddSwitcherInput.Text));
                    NewWorkingInventory.AddPart((src.Part)addedIH);
                }
                else
                {
                    src.Outsourced addedOS = new src.Outsourced(AddedNewPart, AddSwitcherInput.Text);
                    NewWorkingInventory.AddPart((src.Part)addedOS);
                }

                Console.WriteLine("Build Finished");
            }

            return isValidPartInput;
        }

        /*
        Button Methods 
        */

        // Save New Product
        private void SaveNewPrtBtn_Click(object sender, RoutedEventArgs e)
        {
            bool passedValidation = NewPartBuilder();

            if (passedValidation == false)
            { Console.WriteLine("Form Contains Invalid Fields"); }
            else
            {
                MainWindow AddPartSaved = new MainWindow();
                AddPartSaved.mainInventory = NewWorkingInventory;
                AddPartSaved.Show();
                Close();
            }
        }

        // Cancel And Return to Main Window
        private void CancelNewPrtBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow AddPartCancelled = new MainWindow();
            AddPartCancelled.mainInventory = NewRestoreInventory;
            AddPartCancelled.Show();
            Close();
        }

        
    }
}
