using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C968_WGU.src
{
    public abstract class Part
    {
        private int _partID;
        private string _partName;
        private decimal _partPrice;
        private int _partsInStock;
        private int _partMin;
        private int _partMax;

        /*
        Getters / Setters
        */
        public int partID
        {
            get { return _partID; }
            set { _partID = value; }
        }

        public string partName
        {
            get { return _partName; }
            set { _partName = value; }
        }

        public decimal partPrice
        {
            get { return _partPrice; }
            set { _partPrice = value; }
        }

        public int partsInStock
        {
            get { return _partsInStock; }
            set { _partsInStock = value; }
        }

        public int partMin
        {
            get { return _partMin; }
            set { _partMin = value; }
        }

        public int partMax
        {
            get { return _partMax; }
            set { _partMax = value; }
        }

        // Base Clase Template
        public struct BasePart
        {
            public int basePartID;
            public string basePartName;
            public decimal basePartPrice;
            public int basePartQty;
            public int basePartMin;
            public int basePartMax;
        }
    }
}
