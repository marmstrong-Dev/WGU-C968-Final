using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C968_WGU.src
{
    public class InHouse : Part
    {
        private int _machineID;

        // Getter / Setter
        public int machineID
        {
            get { return _machineID; }
            set { _machineID = value; }
        }

        // Constructor
        // Constructor
        public InHouse(BasePart basePart, int mID)
        {
            partID = basePart.basePartID;
            partName = basePart.basePartName;
            partPrice = basePart.basePartPrice;
            partsInStock = basePart.basePartQty;
            partMin = basePart.basePartMin;
            partMax = basePart.basePartMax;

            machineID = mID;
        }
    }
}
