using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace C968_WGU.src
{
    public class Filter
    {
        public Inventory FilteredInventory;
        public DataTable resetTable;
        public Product workingFilteredProd;
        private bool _isProd;
        private int _tableCounter;

        /*
        Setters
        */

        public bool isProd
        { set { _isProd = value; } }

        public int tableCounter 
        { set { _tableCounter = value; } }

        // Reset Table Router
        public DataTable ResetTable(DataTable resettingTable)
        {
            resetTable = TableRestore(resettingTable);
            Console.WriteLine($"Reset Table");
            return resetTable;
        }

        // Filters Tables on Main Menu
        public DataTable FilterMainTables(DataTable mainFilter, string searchedText)
        {
            // Sets All Selections to False
            for (int i = 0; i < mainFilter.Rows.Count; i++)
            {
                mainFilter.Rows[i].BeginEdit();
                mainFilter.Rows[i][0] = false;
                mainFilter.Rows[i].EndEdit();
            }

            // Searches for Matches to Searched Text
            for (int i = 0; i < mainFilter.Rows.Count; i++) 
            {                
                string filteredName = mainFilter.Rows[i].ItemArray[2].ToString();

                if (filteredName.ToUpper().Contains(searchedText.ToUpper()))
                {
                    mainFilter.Rows[i].BeginEdit();
                    mainFilter.Rows[i][0] = true;
                    mainFilter.DefaultView.RowFilter = "Selected = true";
                    mainFilter.Rows[i].EndEdit();

                    _tableCounter--;
                    
                    Console.WriteLine($"Result Found: {filteredName}");
                }
            }
            return mainFilter;
        }

        // Filters Candidate Parts Table
        public DataTable FilterCandidates(DataTable candidatePartsFilter, string searchedText)
        {
            for (int i = 0; i < candidatePartsFilter.Rows.Count; i++)
            {
                candidatePartsFilter.Rows[i].BeginEdit();
                candidatePartsFilter.Rows[i][0] = false;
                candidatePartsFilter.Rows[i].EndEdit();

                string filteredString = candidatePartsFilter.Rows[i].ItemArray[2].ToString();
                if (filteredString.ToUpper().Contains(searchedText.ToUpper()))
                {
                    candidatePartsFilter.Rows[i].BeginEdit();
                    candidatePartsFilter.Rows[i][0] = true;
                    candidatePartsFilter.DefaultView.RowFilter = "Selected = true";
                    candidatePartsFilter.Rows[i].EndEdit();

                    Console.WriteLine($"Result Found: {filteredString}");
                }
                else
                { Console.WriteLine("No Results"); }
            }

            return candidatePartsFilter;
        }

        // Filters Associated Data Tables
        public DataTable FilterAssociations(DataTable associatedPartsFilter, string searchedText)
        {
            for (int i = 0; i < associatedPartsFilter.Rows.Count; i++)
            {
                string associatedName = (string)associatedPartsFilter.Rows[i].ItemArray[2];
                int associatedID = (int)associatedPartsFilter.Rows[i].ItemArray[1];
                
                if (associatedName.ToUpper().Contains(searchedText.ToUpper()))
                {
                    associatedPartsFilter.Rows[i].BeginEdit();
                    associatedPartsFilter.Rows[i][0] = true;
                    associatedPartsFilter.DefaultView.RowFilter = "Selected = true";
                    associatedPartsFilter.Rows[i].EndEdit();
                    Console.WriteLine($"{associatedName} Filtered");
                }
            }

            return associatedPartsFilter;
        }

        // Resets Filters on Tables
        public DataTable TableRestore(DataTable mainRestore)
        {
            // Sets All Selections to False
            for (int i = 0; i < mainRestore.Rows.Count; i++)
            {
                mainRestore.Rows[i].BeginEdit();
                mainRestore.Rows[i][0] = false;
                mainRestore.Rows[i].EndEdit();
            }

            mainRestore.DefaultView.RowFilter = String.Empty;

            return mainRestore;
        }
    }
}
