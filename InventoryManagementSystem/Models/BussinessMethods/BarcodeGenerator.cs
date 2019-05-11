using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models.BussinessMethods
{
    public class BarcodeGenerator
    {
        public DataTable GenerateBarcode(string LotNumber,int Quantity,string ProductID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductBarcode", typeof(string));
            dt.Columns.Add("LotNumber", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));

            for(int i=1;i<=Quantity;i++)
            {
                string serialNumber = i.ToString();
                string dateImpress = System.DateTime.Now.ToString("ddMMyy");
                //string dateImpress = "010519";

                string Barcode = ProductID.PadLeft(3,'0') + LotNumber.PadLeft(4, '0') + dateImpress + serialNumber.PadLeft(3, '0');

                dt.Rows.Add(Barcode, Convert.ToInt32(LotNumber),Convert.ToInt32(ProductID));
            }

            return dt;
        }

    }
}
