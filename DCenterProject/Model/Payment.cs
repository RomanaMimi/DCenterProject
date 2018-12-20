using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCenterProject.Model
{
    [Serializable]
    /*This class also work as a Payment*/
    public class Payment
    {
        public string BillNo { get; set; }
        public string Patient_name { get; set; }
        public List<Test> PTestList { get; set; }
        public string BillDate { get; set; }
        public decimal Total { get; set; }
        public decimal Paid { get; set; }
        public decimal Due { get; set; }

        public decimal CalculateDue(decimal total, decimal paid)
        {
            total -= paid;
            Due = total;
            return Due;
        }
    }
}