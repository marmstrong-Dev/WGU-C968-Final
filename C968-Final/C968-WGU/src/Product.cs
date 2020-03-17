 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace C968_WGU.src
{
    public class Product
    {
        private int _productID;
        private string _productName;
        private decimal _productPrice;
        private int _productsInStock;
        private int _productMin;
        private int _productMax;

        private BindingList<Part> _associatedParts = new BindingList<Part>();

        /*
        Getters / Setters
        */

        public int productID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        public string productName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        public decimal productPrice
        {
            get { return _productPrice; }
            set { _productPrice = value; }
        }

        public int productsInStock
        {
            get { return _productsInStock; }
            set { _productsInStock = value; }
        }

        public int productMin
        {
            get { return _productMin; }
            set { _productMin = value; }
        }

        public int productMax
        {
            get { return _productMax; }
            set { _productMax = value; }
        }

        public BindingList<Part> associatedParts
        {
            get { return _associatedParts; }
        }

        /*
        Methods for Associated Parts 
        */

        // Add Associated Part to New or Edited Product
        public void AddAssociatedPart(Part addAssociation)
        { associatedParts.Add(addAssociation); }

        // Remove Associated Product From Associated Products List
        public bool DeleteAssociatedPart(int deleteAssociationIndex)
        {
            try
            { _associatedParts.Remove(_associatedParts[deleteAssociationIndex]); return true; }
            catch 
            { return false; }
        }

        // Lookup Associated Products By Index
        public Part LookupAssociatedPart(int associationIndex)
        { return associatedParts[associationIndex]; }
    }
}
