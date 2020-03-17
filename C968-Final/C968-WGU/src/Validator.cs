using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace C968_WGU.src
{
    public class Validator
    {
        private int _selectorID;
        private bool _isLocalPart;

        public int selectorID
        { get { return _selectorID; } }

        public bool isLocalPart 
        { set { _isLocalPart = value; } }

        // Validates Number of Selected Items
        public bool SelectionValidation(DataTable workingTable)
        {
            bool validSelection = false;
            int numChecks = 0;
            
            for (int i = 0; i < workingTable.Rows.Count; i++)
            {
                if ((bool)workingTable.Rows[i].ItemArray[0] == true)
                { 
                    numChecks++;
                    _selectorID = i;
                }
            }

            if (numChecks == 1)
            { validSelection = true; }
            
            return validSelection;
        }  

        // Determines If Part is InHouse or Outsourced
        public bool IsLocalized(DataRow workingRow)
        {
            bool isLocal;

            if ((string)workingRow.ItemArray[8] == "")
            { isLocal = true; }
            else
            { isLocal = false; }

            Console.WriteLine($"Is Local: {isLocal}");
            return isLocal;
        }

        // Form Validation Method
        public bool IsValidForm(bool isProd, string[] formInputTxt)
        {
            bool isValidForm = true;

            foreach (string inputTxt in formInputTxt)
            {
                if (inputTxt == "")
                {
                    isValidForm = false;
                    Console.WriteLine("Empty Fields Present");
                }
            }

            if (isProd == true)
            {
                try
                {
                    decimal validateProdPrice = Decimal.Parse(formInputTxt[1]);
                    int validateProdQty = Int32.Parse(formInputTxt[2]);
                    int validateProdMin = Int32.Parse(formInputTxt[3]);
                    int validateProdMax = Int32.Parse(formInputTxt[4]);
                }
                catch
                {
                    isValidForm = false;
                    Console.WriteLine("Invalid Data Types");
                }
            }
            else
            {
                try
                {
                    decimal validatePartPrice = Decimal.Parse(formInputTxt[1]);
                    int validatePartQty = Int32.Parse(formInputTxt[2]);
                    int validatePartMin = Int32.Parse(formInputTxt[3]);
                    int validatePartMax = Int32.Parse(formInputTxt[4]);

                    if (_isLocalPart == true)
                    {
                        int validateMachineID = Int32.Parse(formInputTxt[5]);
                    }
                }
                catch
                {
                    isValidForm = false;
                    Console.WriteLine("Invalid Data Types");
                }

            }

            if (isValidForm == true)
            { isValidForm = ValidFormRange(Int32.Parse(formInputTxt[2]), Int32.Parse(formInputTxt[3]), Int32.Parse(formInputTxt[4])); }
            
            return isValidForm;
        }

        // Determines if Associated Part Exists
        public bool AssociationValidator(Inventory validatorInventory, int validationID)
        {
            bool noAssociations = false;

            for (int i = 0; i < validatorInventory.AllProducts.Count; i++)
            {
                if (validationID == validatorInventory.AllProducts[i].productID)
                {
                    if (validatorInventory.AllProducts[i].associatedParts.Count == 0)
                    { noAssociations = true; }
                }
            }

            return noAssociations;
        }

        // Determines if Min and Max Range is Valid
        private bool ValidFormRange(int qtyValidate,int minValidate, int maxValidate)
        {
            bool isValidRange = false;

            if (minValidate < maxValidate && maxValidate >= qtyValidate && minValidate <= qtyValidate)
            { 
                Console.WriteLine("Valid Range");
                isValidRange = true;
            }
            else
            { Console.WriteLine("Invalid Range"); }

            return isValidRange;
        }
    }
}
