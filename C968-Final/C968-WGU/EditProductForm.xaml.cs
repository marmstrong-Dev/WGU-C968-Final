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
    /// Interaction logic for EditProductForm.xaml
    /// </summary>
    public partial class EditProductForm : Window
    {
        public src.Inventory EditProdRestoreInventory;
        public src.Inventory EditProdWorkingInventory;
        public src.Product EditWorkingProduct = new src.Product();
        public src.Validator EditProdValidator = new src.Validator();
        public DataTable EditCandidateData;
        public DataTable EditAssociatedData;
        public int editProdID;
        public int editProdIndex;

        // Constructor
        public EditProductForm()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        // On Window Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BuildCandidateParts_e();
            BuildAssociatedParts_e();
            EditIDFinder_e();
            ProductFormBuilder_e();
        }

        // Determines ID to Modify
        private void EditIDFinder_e()
        {
            for (int i = 0; i < EditProdWorkingInventory.AllProducts.Count; i++)
            {
                if (editProdID == EditProdWorkingInventory.AllProducts[i].productID)
                {
                    editProdIndex = EditProdWorkingInventory.AllProducts.IndexOf(EditProdWorkingInventory.AllProducts[i]);
                    Console.WriteLine($"Index Selected: {editProdIndex}");
                }
            }
        }

        // Fills Form With Product Information
        private void ProductFormBuilder_e()
        {
            EditProdIDLabel.Content = "Product ID: " + editProdID;
            EditedProdNameInput.Text = EditProdWorkingInventory.AllProducts[editProdIndex].productName;
            EditedProdPriceInput.Text = EditProdWorkingInventory.AllProducts[editProdIndex].productPrice.ToString();
            EditedProdQtyInput.Text = EditProdWorkingInventory.AllProducts[editProdIndex].productsInStock.ToString();
            EditedProdMinInput.Text = EditProdWorkingInventory.AllProducts[editProdIndex].productMin.ToString();
            EditedProdMaxInput.Text = EditProdWorkingInventory.AllProducts[editProdIndex].productMax.ToString();
        }

        // Builds Table for Candidate Parts
        private void BuildCandidateParts_e()
        {
            EditCandidateData = new DataTable();

            // Creates Columns
            DataColumn availableId_e = new DataColumn("Part ID", typeof(int));
            DataColumn availableName_e = new DataColumn("Part Name", typeof(string));
            DataColumn availablePrice_e = new DataColumn("Part Price Per Unit", typeof(decimal));
            DataColumn availableQty_e = new DataColumn("Quantity In Stock", typeof(int));
            DataColumn availableMin_e = new DataColumn("Min", typeof(int));
            DataColumn availableMax_e = new DataColumn("Max", typeof(int));

            // Adds Columns to Table
            EditCandidateData.Columns.Add(new DataColumn("Selected", typeof(bool)));
            EditCandidateData.Columns.Add(availableId_e);
            EditCandidateData.Columns.Add(availableName_e);
            EditCandidateData.Columns.Add(availablePrice_e);
            EditCandidateData.Columns.Add(availableQty_e);
            EditCandidateData.Columns.Add(availableMin_e);
            EditCandidateData.Columns.Add(availableMax_e);

            CandidatePartRestore_e();
        }

        // Builds Table for Associated Parts
        private void BuildAssociatedParts_e()
        {
            EditAssociatedData = new DataTable();

            // Creates Columns
            DataColumn associatedId_e = new DataColumn("Part ID", typeof(int));
            DataColumn associatedName_e = new DataColumn("Part Name", typeof(string));
            DataColumn associatedPrice_e = new DataColumn("Part Price Per Unit", typeof(decimal));
            DataColumn associatedQty_e = new DataColumn("Quantity In Stock", typeof(int));
            DataColumn associatedMin_e = new DataColumn("Min", typeof(int));
            DataColumn associatedMax_e = new DataColumn("Max", typeof(int));

            // Adds Columns to Table
            EditAssociatedData.Columns.Add(new DataColumn("Selected", typeof(bool)));
            EditAssociatedData.Columns.Add(associatedId_e);
            EditAssociatedData.Columns.Add(associatedName_e);
            EditAssociatedData.Columns.Add(associatedPrice_e);
            EditAssociatedData.Columns.Add(associatedQty_e);
            EditAssociatedData.Columns.Add(associatedMin_e);
            EditAssociatedData.Columns.Add(associatedMax_e);

            AssociatedPartRestore_e();
        }

        // Adds Candidate Parts to Table
        private void CandidatePartRestore_e()
        {
            for (int i = 0; i < EditProdWorkingInventory.AllParts.Count; i++)
            {
                DataRow addedRow = EditCandidateData.NewRow();
                
                addedRow[0] = false;
                addedRow[1] = EditProdWorkingInventory.AllParts[i].partID;
                addedRow[2] = EditProdWorkingInventory.AllParts[i].partName;
                addedRow[3] = EditProdWorkingInventory.AllParts[i].partPrice;
                addedRow[4] = EditProdWorkingInventory.AllParts[i].partsInStock;
                addedRow[5] = EditProdWorkingInventory.AllParts[i].partMin;
                addedRow[6] = EditProdWorkingInventory.AllParts[i].partMax;

                EditCandidateData.Rows.Add(addedRow);
            }

            EditCandidateData.DefaultView.Sort = "Part ID asc";
            EditedAvailableProdDataGrid.ItemsSource = EditCandidateData.DefaultView;
        }

        // Adds Associated Parts to Table
        private void AssociatedPartRestore_e()
        {
            for (int i = 0; i < EditWorkingProduct.associatedParts.Count; i++)
            {
                DataRow addedRow = EditAssociatedData.NewRow();

                addedRow[0] = false;
                addedRow[1] = EditWorkingProduct.associatedParts[i].partID;
                addedRow[2] = EditWorkingProduct.associatedParts[i].partName;
                addedRow[3] = EditWorkingProduct.associatedParts[i].partPrice;
                addedRow[4] = EditWorkingProduct.associatedParts[i].partsInStock;
                addedRow[5] = EditWorkingProduct.associatedParts[i].partMin;
                addedRow[6] = EditWorkingProduct.associatedParts[i].partMax;

                EditAssociatedData.Rows.Add(addedRow);
            }

            EditAssociatedData.DefaultView.Sort = "Part ID asc";
            EditedAssociatedProdDataGrid.ItemsSource = EditAssociatedData.DefaultView;
        }

        /*
        Button Methods 
        */

        // Reset Candidate Parts Table
        private void EditedCandidateResetBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter candidateRestore_e = new src.Filter();
            candidateRestore_e.FilteredInventory = EditProdWorkingInventory;
            candidateRestore_e.ResetTable(EditCandidateData);
        }
        // Filter Candidate Parts By Name
        private void EditedCandidateSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter candidateFilter_e = new src.Filter();
            DataTable filteredCandidatesTable_e = candidateFilter_e.FilterCandidates(EditCandidateData, EditedCandidateSearchInput.Text);
            EditedAvailableProdDataGrid.ItemsSource = filteredCandidatesTable_e.DefaultView;
        }

        // Reset Associated Parts Table
        private void EditedAssociatedResetBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter associationRestore = new src.Filter();
            associationRestore.FilteredInventory = EditProdWorkingInventory;
            associationRestore.workingFilteredProd = EditWorkingProduct;
            associationRestore.ResetTable(EditAssociatedData);
        }

        // Filters Associated Parts By Name
        private void EditedAssociatedSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter associationFilter = new src.Filter();
            DataTable filteredAssociatesTable = associationFilter.FilterAssociations(EditAssociatedData, EditedAssociatedSearchInput.Text);
            EditedAssociatedProdDataGrid.ItemsSource = filteredAssociatesTable.DefaultView;
        }

        // Adds Candidate Part to Associated Parts List
        private void EditedAssociationAdder_Click(object sender, RoutedEventArgs e)
        {// Fix Me
            for (int i = 0; i < EditCandidateData.Rows.Count; i++)
            {
                if ((bool)EditCandidateData.Rows[i].ItemArray[0] == true)
                {
                    src.Part.BasePart TemplateAssociation_e = new src.Part.BasePart();
                    TemplateAssociation_e.basePartID = (int)EditCandidateData.Rows[i].ItemArray[1];
                    TemplateAssociation_e.basePartName = (string)EditCandidateData.Rows[i].ItemArray[2];
                    TemplateAssociation_e.basePartPrice = (decimal)EditCandidateData.Rows[i].ItemArray[3];
                    TemplateAssociation_e.basePartQty = (int)EditCandidateData.Rows[i].ItemArray[4];
                    TemplateAssociation_e.basePartMin = (int)EditCandidateData.Rows[i].ItemArray[5];
                    TemplateAssociation_e.basePartMax = (int)EditCandidateData.Rows[i].ItemArray[6];

                    DataRow addedRow = EditAssociatedData.NewRow();

                    addedRow[0] = false;
                    addedRow[1] = (int)EditCandidateData.Rows[i].ItemArray[1];
                    addedRow[2] = (string)EditCandidateData.Rows[i].ItemArray[2];
                    addedRow[3] = (decimal)EditCandidateData.Rows[i].ItemArray[3];
                    addedRow[4] = (int)EditCandidateData.Rows[i].ItemArray[4];
                    addedRow[5] = (int)EditCandidateData.Rows[i].ItemArray[5];
                    addedRow[6] = (int)EditCandidateData.Rows[i].ItemArray[6];

                    EditAssociatedData.Rows.Add(addedRow);
                    src.InHouse AddAssoc_e = new src.InHouse(TemplateAssociation_e, 99);
                    EditWorkingProduct.AddAssociatedPart((src.Part)AddAssoc_e);
                    Console.WriteLine("Added Part");
                }
            }
        }

        // Removes Associated Part
        private void EditedAssociationRemover_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < EditAssociatedData.Rows.Count; i++)
            {
                if ((bool)EditAssociatedData.Rows[i].ItemArray[0] == true)
                { // Fix Me
                    if (EditWorkingProduct.DeleteAssociatedPart(i) == true)
                    { EditAssociatedData.Rows[i].Delete(); }

                    Console.WriteLine($"Removed Part {i}");
                }
            }
        }

        // Save Edited Product
        private void SaveEditedProdBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] inputFormTxt_e = { EditedProdNameInput.Text, EditedProdPriceInput.Text, EditedProdQtyInput.Text, EditedProdMinInput.Text, EditedProdMaxInput.Text };
            bool isValidProdInput = EditProdValidator.IsValidForm(true, inputFormTxt_e);

            if (isValidProdInput == false)
            { MessageBox.Show("Error Invalid Form Data"); }
            else
            {
                EditWorkingProduct.productID = editProdID;
                EditWorkingProduct.productName = EditedProdNameInput.Text;
                EditWorkingProduct.productPrice = Decimal.Parse(EditedProdPriceInput.Text) + 0.00M;
                EditWorkingProduct.productsInStock = Int32.Parse(EditedProdQtyInput.Text);
                EditWorkingProduct.productMin = Int32.Parse(EditedProdMinInput.Text);
                EditWorkingProduct.productMax = Int32.Parse(EditedProdMaxInput.Text);

                EditProdWorkingInventory.EditProduct(EditWorkingProduct, editProdIndex);

                MainWindow EditProductSaved = new MainWindow();
                EditProductSaved.mainInventory = EditProdWorkingInventory;
                EditProductSaved.Show();
                Close();
            }
        }

        // Cancels and Returns to Main Menu
        private void CancelEditedProdBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow EditProductCancelled = new MainWindow();
            EditProductCancelled.mainInventory = EditProdRestoreInventory;
            EditProductCancelled.Show();
            Close();
        }
    }
}
