using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C968_WGU.src
{
    public class Outsourced : Part
    {
        private string _companyName;

        // Getter / Setter
        public string companyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        // Constructor
        public Outsourced(BasePart basePart, string cNM)
        {
            partID = basePart.basePartID;
            partName = basePart.basePartName;
            partPrice = basePart.basePartPrice;
            partsInStock = basePart.basePartQty;
            partMin = basePart.basePartMin;
            partMax = basePart.basePartMax;

            companyName = cNM;
        }
    }
}
