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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace C968_WGU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public src.Inventory mainInventory = new src.Inventory();
        public src.Validator mainValidator = new src.Validator();
        public DataTable partsData = new DataTable();
        public DataTable productsData = new DataTable();

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        // On Window Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainInventory.BuildPartsList();
            PrtDataBuilder();
            ProdDataBuilder();
        }

        /*
        Table Builder Methods 
        */

        // Builds Parts Data Table
        public void PrtDataBuilder()
        {
            // Creates Columns
            DataColumn prtId = new DataColumn("Part ID", typeof(int));
            DataColumn prtName = new DataColumn("Part Name", typeof(string));
            DataColumn prtPrice = new DataColumn("Part Price Per Unit", typeof(decimal));
            DataColumn prtQty = new DataColumn("Quantity In Stock", typeof(int));
            DataColumn prtMin = new DataColumn("Min", typeof(int));
            DataColumn prtMax = new DataColumn("Max", typeof(int));
            DataColumn prtMachineID = new DataColumn("Machine ID", typeof(int));
            DataColumn prtCompanyName = new DataColumn("Company Name", typeof(string));

            // Adds Columns to Data Table
            partsData.Columns.Add(new DataColumn("Selected", typeof(bool)));
            partsData.Columns.Add(prtId);
            partsData.Columns.Add(prtName);
            partsData.Columns.Add(prtPrice);
            partsData.Columns.Add(prtQty);
            partsData.Columns.Add(prtMin);
            partsData.Columns.Add(prtMax);
            partsData.Columns.Add(prtMachineID);
            partsData.Columns.Add(prtCompanyName);

            for (int i = 0; i < mainInventory.AllParts.Count; i++)
            {
                DataRow addedRow_init = partsData.NewRow();
                src.Part initPart = mainInventory.LookupPart(i);

                addedRow_init[0] = false;
                addedRow_init[1] = initPart.partID;
                addedRow_init[2] = initPart.partName;
                addedRow_init[3] = initPart.partPrice;
                addedRow_init[4] = initPart.partsInStock;
                addedRow_init[5] = initPart.partMin;
                addedRow_init[6] = initPart.partMax;

                try
                {
                    src.Outsourced conversionOS = (src.Outsourced)initPart;
                    if (conversionOS.companyName != "")
                    { addedRow_init[8] = conversionOS.companyName; }
                }
                catch
                {
                    src.InHouse conversionIH = (src.InHouse)initPart;
                    addedRow_init[7] = conversionIH.machineID;
                    addedRow_init[8] = "";
                }

                partsData.Rows.Add(addedRow_init);
            }            

            partsData.DefaultView.Sort = "Part ID asc";
            PrtDataGrid.ItemsSource = partsData.DefaultView;
        }

        // Builds Product Data Table
        public void ProdDataBuilder()
        {
            // Creates Columns
            DataColumn prodId = new DataColumn("Product ID", typeof(int));
            DataColumn prodName = new DataColumn("Product Name");
            DataColumn prodPrice = new DataColumn("Product Price Per Unit", typeof(decimal));
            DataColumn prodQty = new DataColumn("Quantity In Stock", typeof(int));
            DataColumn prodMin = new DataColumn("Min", typeof(int));
            DataColumn prodMax = new DataColumn("Max", typeof(int));
            

            // Adds Columns to Data Table
            productsData.Columns.Add(new DataColumn("Selected", typeof(bool)));
            productsData.Columns.Add(prodId);
            productsData.Columns.Add(prodName);
            productsData.Columns.Add(prodPrice);
            productsData.Columns.Add(prodQty);
            productsData.Columns.Add(prodMin);
            productsData.Columns.Add(prodMax);

            // Binds All Products
            for (int i = 0; i < mainInventory.AllProducts.Count; i++)
            {
                //DataRow addedProdRow = productsData.NewRow();
                src.Product initProduct = mainInventory.LookupProduct(mainInventory.AllProducts[i].productID);

                DataRow addProdInit = productsData.NewRow();

                addProdInit[0] = false;
                addProdInit[1] = initProduct.productID;
                addProdInit[2] = initProduct.productName;
                addProdInit[3] = initProduct.productPrice;
                addProdInit[4] = initProduct.productsInStock;
                addProdInit[5] = initProduct.productMin;
                addProdInit[6] = initProduct.productMax;

                productsData.Rows.Add(addProdInit);
            }
            
            productsData.DefaultView.Sort = "Product ID asc";
            ProdDataGrid.ItemsSource = productsData.DefaultView;
        }

        // Resets Filters on Main Tables
        public void TableReset(bool isProd)
        {
            src.Filter resetterFilter = new src.Filter();
            resetterFilter.isProd = isProd;
            resetterFilter.FilteredInventory = mainInventory;

            if (isProd == true)
            {
                resetterFilter.ResetTable(productsData);
                ProdDataGrid.ItemsSource = productsData.DefaultView;
            }
            else
            {
                resetterFilter.ResetTable(partsData);
                PrtDataGrid.ItemsSource = partsData.DefaultView;
            }
        }

        /*
        Part Window Methods 
        */

        // Opens Add New Part Window
        private void AddPrtBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPartForm NewPartsForm = new AddPartForm();
            NewPartsForm.NewWorkingInventory = mainInventory;
            NewPartsForm.NewRestoreInventory = mainInventory;
            NewPartsForm.Show();
            Close();
        }

        // Opens Edit Part Window
        private void EditPartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainValidator.SelectionValidation(partsData) == true)
            {
                int selectionID = mainValidator.selectorID;
                EditPartForm EditPartsForm = new EditPartForm(mainInventory.AllParts[selectionID].partID);
                EditPartsForm.EditWorkingInventory = mainInventory;
                EditPartsForm.EditRestoreInventory = mainInventory;
                EditPartsForm.localEdit = mainValidator.IsLocalized(partsData.Rows[selectionID]);
                EditPartsForm.Show();
                Close();
            }
            else 
            { MessageBox.Show("Invalid Quantity of Selected Items"); }
        }

        // Lookup Part By Name
        private void MainPartSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter mainPartFilter = new src.Filter();
            mainPartFilter.tableCounter = mainInventory.partCount;
            DataTable partsFilterTable = mainPartFilter.FilterMainTables(partsData, MainPartSearchInput.Text);
            PrtDataGrid.ItemsSource = partsFilterTable.DefaultView;
        }

        // Reset Button for Parts Table
        private void MainPartRestoreBtn_Click(object sender, RoutedEventArgs e)
        { TableReset(false); }

        // Deletes All Selected Parts
        private void DelPrtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainValidator.SelectionValidation(partsData) == true)
            {
                MessageBoxResult confirmDeletePart = MessageBox.Show("Are You Sure", "Part Deleted", MessageBoxButton.YesNo);
                if (confirmDeletePart == System.Windows.MessageBoxResult.Yes)
                {
                    for (int i = 0; i < partsData.Rows.Count; i++)
                    {
                        if ((bool)partsData.Rows[i].ItemArray[0] == true)
                        {
                            try
                            {
                                src.Part candidatePart;
                                candidatePart = mainInventory.AllParts[i];
                                bool successDelete = mainInventory.DeletePart(candidatePart);
                                partsData.Rows.Remove(partsData.Rows[i]);
                                
                                if (successDelete == true) 
                                { MessageBox.Show("Deleted Part Successfully"); }
                            }
                            catch
                            { Console.WriteLine("No Data"); }
                        }
                    }
                }
            }
            else
            { MessageBox.Show("Select Only 1 Item"); }
        }

        /*
        Product Window Methods     
        */

        // Opens Add Product Window
        private void AddProdBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProductForm NewProdForm = new AddProductForm();
            NewProdForm.NewProdWorkingInventory = mainInventory;
            NewProdForm.NewProdRestoreInventory = mainInventory;
            NewProdForm.Show();
            Close();
        }

        // Opens Edit Product Window
        private void EditProdBtn_Click(object sender, RoutedEventArgs e)
        {
            if(mainValidator.SelectionValidation(productsData) == true)
            {
                EditProductForm EditProdForm = new EditProductForm();
                
                for (int i = 0; i < productsData.Rows.Count; i++)
                {
                    if ((bool)productsData.Rows[i].ItemArray[0])
                    {
                        EditProdForm.editProdID = (int)productsData.Rows[i].ItemArray[1];
                        EditProdForm.EditWorkingProduct = mainInventory.AllProducts[i];
                    }
                }

                EditProdForm.EditProdWorkingInventory = mainInventory;
                EditProdForm.EditProdRestoreInventory = mainInventory;
                EditProdForm.Show();
                Close();
            }
            else
            { MessageBox.Show("Invalid Quantity of Selected Items"); }            
        }

        // Lookup Product By Name
        private void MainProdSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            src.Filter mainProdFilter = new src.Filter();
            mainProdFilter.tableCounter = mainInventory.productCount;
            DataTable prodFilterTable = mainProdFilter.FilterMainTables(productsData, MainProdSearchInput.Text);
            ProdDataGrid.ItemsSource = prodFilterTable.DefaultView;
        }

        // Reset Button for Products Table
        private void MainProdRestoreBtn_Click(object sender, RoutedEventArgs e)
        { TableReset(true); }

        // Deletes All Selected Products
        private void DelProdBtn_Click(object sender, RoutedEventArgs e)
        {
            int prodDeleteIndex;
            int prodDeleteID;
            bool formValid = false;

            if (mainValidator.SelectionValidation(productsData) == true)
            {
                for (int i = 0; i < productsData.Rows.Count; i++)
                {
                    if (mainValidator.AssociationValidator(mainInventory, (int)productsData.Rows[i].ItemArray[1]) == true)
                    { formValid = true; }
                }

                MessageBoxResult confirmDeleteProd = MessageBox.Show("Are You Sure", "Product Deleted", MessageBoxButton.YesNo);
                if (confirmDeleteProd == System.Windows.MessageBoxResult.Yes && formValid == true)
                {
                    for (int i = 0; i < productsData.Rows.Count; i++)
                    {
                        if ((bool)productsData.Rows[i].ItemArray[0] == true)
                        {
                            prodDeleteID = (int)productsData.Rows[i].ItemArray[1];
                            prodDeleteIndex = i;
                            bool successfulDelete = mainInventory.DeleteProduct(prodDeleteIndex);
                            productsData.Rows[prodDeleteIndex].Delete();
                            if (successfulDelete == true) 
                            { MessageBox.Show("Deleted Product Successfully"); }
                        }
                    }
                }
                else
                { MessageBox.Show("Cannot Delete Product With Associated Parts"); }
            }
            else
            { MessageBox.Show("Select Only 1 Item"); }
        }

        // Exit Application Button
        private void CloserBtn_Click(object sender, RoutedEventArgs e)
        { Close(); }
    }
}
