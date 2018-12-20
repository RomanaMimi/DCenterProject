using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCenterProject.Gateway;
using DCenterProject.Model;

namespace DCenterProject.BLL
{
    public class TestReqManager
    {
        TestReqGateway testReqGateway = new TestReqGateway();
        public List<Test> GelAllTests()
        {
            return testReqGateway.GetAllTests();
        }

        public decimal IsTestExist(int testId)
        {
            return testReqGateway.IsTestExist(testId);
        }

        public string Check(Patient p1)
        {
            //name length check
            if (p1.PatientName.Length < 3)
                return "Patient name must be 3 charachters long";

            //cell No's length check
            if (p1.CellNo.Length < 7)
                return "Cell number must be greater than 7 charachters long";

            return "Success";
        }

        public string SavePatient(Patient patient)
        {
        
            // bill number check
            if (patient.BillNo.Length < 17)
                return "Bill number must be 17 charachters long";

            //data insert
            int rowAffect = testReqGateway.SavePatient(patient);
            if (rowAffect > 0)
            {
                return "Success";
            }
            return "Not saved.";

        }

        public string SaveReqTest(TestRequest tReq)
        {
            int count = 0;
            foreach (var test in tReq.PTestList)
            {
                tReq.TestReqId = test.TestId;
                int rowAfftect = testReqGateway.SaveReqTest(tReq);
                if (rowAfftect > 0)
                    count++;
            }

            if (tReq.PTestList.Count == count)
            {
                return "Success";
            }
            return "Tests not saved.";
        }
    }
}