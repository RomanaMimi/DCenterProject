using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCenterProject.Model
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string BirthDate { get; set; }
        public string CellNo { get; set; }
        public string BillNo { get; set; }
        public decimal TotalFee { get; set; }
    }
}