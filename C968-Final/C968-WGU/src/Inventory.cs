using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace C968_WGU.src
{
    public class Inventory
    {
        public BindingList<Product> AllProducts = new BindingList<Product>();
        public BindingList<Part> AllParts = new BindingList<Part>();
        public BindingList<InHouse> AllIHParts = new BindingList<InHouse>();
        public BindingList<Outsourced> AllOSParts = new BindingList<Outsourced>();

        private static int _partCount = 2;
        private static int _productCount = 2;

        /*
        Getters 
        */

        public int partCount
        { get { return _partCount; } }

        public int productCount
        { get { return _productCount; } }

        // Constructor
        public Inventory()
        {
            Part.BasePart templateIH = new Part.BasePart();
            templateIH.basePartID = 1;
            templateIH.basePartName = "Test Part 1";
            templateIH.basePartPrice = 1.45M;
            templateIH.basePartQty = 15;
            templateIH.basePartMin = 12;
            templateIH.basePartMax = 21;

            InHouse testIH = new InHouse(templateIH, 4);
            AllIHParts.Add(testIH);

            Part.BasePart templateOS = new Part.BasePart();
            templateOS.basePartID = 2;
            templateOS.basePartName = "Test Part 2";
            templateOS.basePartPrice = 4.15M;
            templateOS.basePartQty = 77;
            templateOS.basePartMin = 55;
            templateOS.basePartMax = 88;

            Outsourced testOS = new Outsourced(templateOS, "Test Company Name");
            AllOSParts.Add(testOS);

            Product testProduct = new Product();
            testProduct.productID = 1;
            testProduct.productName = "Test Product 1";
            testProduct.productPrice = 12.55M;
            testProduct.productsInStock = 5;
            testProduct.productMin = 1;
            testProduct.productMax = 25;
            testProduct.associatedParts.Add(testIH);

            AllProducts.Add(testProduct);

            Product testProduct2 = new Product();
            testProduct2.productID = 2;
            testProduct2.productName = "Test Product 2";
            testProduct2.productPrice = 2.55M;
            testProduct2.productsInStock = 115;
            testProduct2.productMin = 105;
            testProduct2.productMax = 252;
            testProduct2.associatedParts.Add(testOS);

            AllProducts.Add(testProduct2);
        }

        /*
        Part Methods
        */

        // Add New Part to Inventory
        public void AddPart(Part addedPart)
        {
            try
            {
                Outsourced convertOS = (Outsourced)addedPart;
                AllOSParts.Add(convertOS);
                Console.WriteLine("New Outsourced Part Added");
            }
            catch 
            {
                InHouse convertIH = (InHouse)addedPart;
                AllIHParts.Add(convertIH);
                Console.WriteLine("New In House Part Added");
            }

            _partCount++;
        }

        // Edit Existing Part
        public void UpdatePart(Part editedPart, int partID)
        {
            bool editComplete = false;
            int partIndex = 0;

            for (int i = 0; i < AllIHParts.Count; i++)
            {
                if (partID == AllIHParts[i].partID)
                { partIndex = AllIHParts.IndexOf(AllIHParts[i]); editComplete = true; }
            }

            if (editComplete == false)
            {
                for (int i = 0; i < AllOSParts.Count; i++)
                {
                    if (partID == AllOSParts[i].partID)
                    { partIndex = AllOSParts.IndexOf(AllOSParts[i]); }
                }
                AllOSParts.Remove(AllOSParts[partIndex]);
                editComplete = true;
            }
            else
            {
                AllIHParts.Remove(AllIHParts[partIndex]);
                editComplete = true;
            }

            try
            {
                InHouse updatedIH = (InHouse)editedPart;
                AllIHParts.Add(updatedIH);
            }
            catch
            {
                Outsourced updatedOS = (Outsourced)editedPart;
                AllOSParts.Add(updatedOS);
            }

            Console.WriteLine($"Edited Part: {editedPart.partID}");
        }

        // Lookup Part By Index
        public Part LookupPart(int lookupIndex_part)
        { return AllParts[lookupIndex_part]; }

        // Delete Selected Parts
        public bool DeletePart(Part removeCandidate)
        {
            bool notDeleted = false;
            Console.WriteLine($"Remove ID: {removeCandidate.partID}");

            for (int i = 0; i < AllParts.Count; i++)
            {
                if (AllParts[i].partID == removeCandidate.partID)
                {
                    notDeleted = true;
                    AllParts.Remove(AllParts[i]);
                }
            }

            for (int i = 0; i < AllIHParts.Count; i++)
            {
                if (AllIHParts[i].partID == removeCandidate.partID)
                {
                    notDeleted = true;
                    AllIHParts.Remove(AllIHParts[i]);
                }
            }

            for (int i = 0; i < AllOSParts.Count; i++)
            {
                if (AllOSParts[i].partID == removeCandidate.partID)
                {
                    notDeleted = true;
                    AllOSParts.Remove(AllOSParts[i]);
                }
            }

            return notDeleted;
        }

        // Builds List of All Parts
        public void BuildPartsList()
        {
            AllParts.Clear();

            foreach (InHouse ihParts in AllIHParts)
            { AllParts.Add(ihParts); }
            foreach (Outsourced osParts in AllOSParts)
            { AllParts.Add(osParts); }

            Console.WriteLine($"Parts Overall: {AllParts.Count}");
        }

        /*
        Product Methods
        */

        // Adds Product to All Products List
        public void AddProduct(Product AddNewProd)
        {
            AllProducts.Add(AddNewProd);
            _productCount++;
            Console.WriteLine("New Product Added");
        }

        // Remove Product and Replace With Modded Product
        public void EditProduct(Product EditedProd ,int prodIndex)
        {
            AllProducts.Remove(AllProducts[prodIndex]);
            AllProducts.Add(EditedProd);
            Console.WriteLine($"Added Product: {EditedProd.productID}");
        }

        // Lookup Part By ID
        public Product LookupProduct(int lookupID_prod)
        {
            Product foundProd = new Product();

            for (int i = 0; i < AllProducts.Count; i++)
            {
                if (lookupID_prod == AllProducts[i].productID)
                { foundProd = AllProducts[i]; }
            }

            return foundProd;
        }

        // Delete Selected Products
        public bool DeleteProduct(int deleteIndex)
        {
            bool notDeleted = false;

            try
            {
                AllProducts.Remove(AllProducts[deleteIndex]);
                notDeleted = true;
            }
            catch
            { Console.WriteLine("No Product Found"); }

            return notDeleted;
        }
    }
}
