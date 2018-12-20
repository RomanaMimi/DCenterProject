using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCenterProject.Model
{
    [Serializable]
    public class TestRequest
    {
        public int TestReqId { get; set; }
        public List<Test> PTestList { get; set; }
        public int PTestID { get; set; }
        public string BillNo { get; set; }
        public string TestReqDate { get; set; }
    }
}