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
using System.Data;

namespace C968_WGU
{
    /// <summary>
    /// Interaction logic for AddProductForm.xaml
    /// </summary>
    public partial class AddProductForm : Window
    {
        public src.Inventory NewProdRestoreInventory;
        public src.Inventory NewProdWorkingInventory;
        public src.Product NewWorkingProduct = new src.Product();
        public src.Validator NewProdValidator = new src.Validator();
        public DataTable NewCandidateData;
        public DataTable NewAssociatedData;

        // Constructor
        public AddProductForm()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        // On Window Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BuildCandidateParts();
            BuildAssociatedParts();
            AddProdIDLabel.Content = $"Product ID: {NewProdWorkingInventory.productCount + 1}";
        } 

        // Builds Table for Candidate Parts
        private void BuildCandidateParts()
        {
            NewCandidateData = new DataTable();

            // Creates Columns
            DataColumn availableId = new DataColumn("Part ID", typeof(int));
            DataColumn availableName = new DataColumn("Part Name", typeof(string));
            DataColumn availablePrice = new DataColumn("Part Price Per Unit", typeof(decimal));
            DataColumn availableQty = new DataColumn("Quantity In Stock", typeof(int));
            DataColumn availableMin = new DataColumn("Min", typeof(int));
            DataColumn availableMax = new DataColumn("Max", typeof(int));

            // Adds Columns to Table
            NewCandidateData.Columns.Add(new DataColumn("Selected", typeof(bool)));
            NewCandidateData.Columns.Add(availableId);
            NewCandidateData.Columns.Add(availableName);
            NewCandidateData.Columns.Add(availablePrice);
            NewCandidateData.Columns.Add(availableQty);
            NewCandidateData.Columns.Add(availableMin);
            NewCandidateData.Columns.Add(availableMax);

            AddCandidatePartRestore();
        }

        // Builds Table for Associated Parts
        private void BuildAssociatedParts()
        {
            NewAssociatedData = new DataTable();

            // Creates Columns
            DataColumn associatedId = new DataColumn("Part ID", typeof(int));
            DataColumn associatedName = new DataColumn("Part Name", typeof(string));
            DataColumn associatedPrice = new DataColumn("Part Price Per Unit", typeof(decimal));
            DataColumn associatedQty = new DataColumn("Quantity In Stock", typeof(int));
            DataColumn associatedMin = new DataColumn("Min", typeof(int));
            DataColumn associatedMax = new DataColumn("Max", typeof(int));

            // Adds Columns to Table
            NewAssociatedData.Columns.Add(new DataColumn("Selected", typeof(bool)));
            NewAssociatedData.Columns.Add(associatedId);
            NewAssociatedData.Columns.Add(associatedName);
            NewAssociatedData.Columns.Add(associatedPrice);
            NewAssociatedData.Columns.Add(associatedQty);
            NewAssociatedData.Columns.Add(associatedMin);
            NewAssociatedData.Columns.Add(associatedMax);

            AddAssociatedPartRestore();
        }

        // Adds / Restores Candidate Table
        public void AddCandidatePartRestore()
        {
            for (int i = 0; i < NewProdWorkingInventory.AllParts.Count; i++)
            {
                DataRow addedRow = NewCandidateData.NewRow();

                addedRow[0] = false;
                addedRow[1] = NewProdWorkingInventory.AllParts[i].partID;
                addedRow[2] = NewProdWorkingInventory.AllParts[i].partName;
                addedRow[3] = NewProdWorkingInventory.AllParts[i].partPrice;
                addedRow[4] = NewProdWorkingInventory.AllParts[i].partsInStock;
                addedRow[5] = NewProdWorkingInventory.AllParts[i].partMin;
                addedRow[6] = NewProdWorkingInventory.AllParts[i].partMax;

                NewCandidateData.Rows.Add(addedRow);
            }

            NewCandidateData.DefaultView.Sort = "Part ID asc";
            NewAvailableProdDataGrid.ItemsSource = NewCandidateData.DefaultView;
        }

        // Adds / Restores Associated Table
        public void AddAssociatedPartRestore()
        {
            NewAssociatedData.DefaultView.Sort = "Part ID asc";
            NewAssociatedProdDataGrid.ItemsSource = NewAssociatedData.DefaultView;
        }

        /*
        Button Methods 
        */

        // Resets Candidate Part Filters
        private void AddCandidateResetBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter candidateRestore = new src.Filter();
            candidateRestore.FilteredInventory = NewProdWorkingInventory;
            candidateRestore.ResetTable(NewCandidateData);
        }

        // Filter Candidate Parts By Name
        private void AddCandidateSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter candidateFilter = new src.Filter();
            DataTable filteredCandidatesTable = candidateFilter.FilterCandidates(NewCandidateData, AddCandidateSearchInput.Text);

            NewAvailableProdDataGrid.ItemsSource = filteredCandidatesTable.DefaultView;
        }

        // Resets Associated Part Filters
        private void AddAssociatedResetBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter associationRestore = new src.Filter();
            associationRestore.FilteredInventory = NewProdWorkingInventory;
            associationRestore.workingFilteredProd = NewWorkingProduct;
            associationRestore.ResetTable(NewAssociatedData);
        }

        // Filter Associated Parts By Name
        private void AddAssociatedSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter associationFilter = new src.Filter();
            DataTable filteredAssociatesTable = associationFilter.FilterAssociations(NewAssociatedData, AddAssociatedSearchInput.Text);

            NewAssociatedProdDataGrid.ItemsSource = filteredAssociatesTable.DefaultView;
        }

        // Adds Candidate Part to Associated Parts List
        private void NewAssociationAdder_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < NewCandidateData.Rows.Count; i++)
            {
                if ((bool)NewCandidateData.Rows[i].ItemArray[0] == true)
                {
                    src.Part.BasePart TemplateAssociation = new src.Part.BasePart();
                    TemplateAssociation.basePartID = (int)NewCandidateData.Rows[i].ItemArray[1];
                    TemplateAssociation.basePartName = (string)NewCandidateData.Rows[i].ItemArray[2];
                    TemplateAssociation.basePartPrice = (decimal)NewCandidateData.Rows[i].ItemArray[3];
                    TemplateAssociation.basePartQty = (int)NewCandidateData.Rows[i].ItemArray[4];
                    TemplateAssociation.basePartMin = (int)NewCandidateData.Rows[i].ItemArray[5];
                    TemplateAssociation.basePartMax = (int)NewCandidateData.Rows[i].ItemArray[6];

                    DataRow addedRow = NewAssociatedData.NewRow();

                    addedRow[0] = false;
                    addedRow[1] = (int)NewCandidateData.Rows[i].ItemArray[1];
                    addedRow[2] = (string)NewCandidateData.Rows[i].ItemArray[2];
                    addedRow[3] = (decimal)NewCandidateData.Rows[i].ItemArray[3];
                    addedRow[4] = (int)NewCandidateData.Rows[i].ItemArray[4];
                    addedRow[5] = (int)NewCandidateData.Rows[i].ItemArray[5];
                    addedRow[6] = (int)NewCandidateData.Rows[i].ItemArray[6];

                    NewAssociatedData.Rows.Add(addedRow);
                    src.InHouse AddAssoc = new src.InHouse(TemplateAssociation, 99);
                    NewWorkingProduct.AddAssociatedPart((src.Part)AddAssoc);
                    Console.WriteLine("Added Part");
                }
            }

            NewAssociatedProdDataGrid.ItemsSource = NewAssociatedData.DefaultView;
        }

        // Removes Associated Part
        private void NewAssociationRemover_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < NewAssociatedData.Rows.Count; i++)
            {
                if ((bool)NewAssociatedData.Rows[i].ItemArray[0] == true)
                {
                    if (NewWorkingProduct.DeleteAssociatedPart(i) == true) 
                    { NewAssociatedData.Rows[i].Delete(); }
                }
            }
        }

        // Saves New Product
        private void SaveNewProdBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] inputFormTxt = { AddProdNameInput.Text, AddProdPriceInput.Text, AddProdQtyInput.Text, AddProdMinInput.Text, AddProdMaxInput.Text };
            bool isValidProdInput = NewProdValidator.IsValidForm(true, inputFormTxt);

            if (isValidProdInput == false)
            { MessageBox.Show("Error Invalid Form Data"); }
            else
            {
                NewWorkingProduct.productID = NewProdWorkingInventory.productCount + 1;
                NewWorkingProduct.productName = AddProdNameInput.Text;
                NewWorkingProduct.productPrice = Decimal.Parse(AddProdPriceInput.Text) + 0.00M;
                NewWorkingProduct.productsInStock = Int32.Parse(AddProdQtyInput.Text);
                NewWorkingProduct.productMin = Int32.Parse(AddProdMinInput.Text);
                NewWorkingProduct.productMax = Int32.Parse(AddProdMaxInput.Text);

                NewProdWorkingInventory.AddProduct(NewWorkingProduct);
                Console.WriteLine($"Added Product: {NewWorkingProduct.productID}");
                MainWindow AddProductSaved = new MainWindow();
                AddProductSaved.mainInventory = NewProdWorkingInventory;
                AddProductSaved.Show();
                Close();
            }
        }

        // Cancels and Returns to Main Menu
        private void CancelNewProdBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow AddProductCancelled = new MainWindow();
            AddProductCancelled.mainInventory = NewProdRestoreInventory;
            AddProductCancelled.Show();
            Close();
        }
    }
}
