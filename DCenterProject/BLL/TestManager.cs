using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCenterProject.Gateway;
using DCenterProject.Model;
using DCenterProject.Model.ViewModel;

namespace DCenterProject.BLL
{
    public class TestManager
    {
        TestGateway testGateway = new TestGateway();

        public string Save(Test test)
        {
            //length check
            if(test.TestName.Length < 3)
                return "Name must be 3 charcters long";

            //type is selected or not (check)
            if (test.TypeId.Equals(0))
                return "Type is not selected.";

            //unique check
            if (testGateway.IsTestExist(test.TestName))
                return "Already exist";

            //data insert
            int rowAffect = testGateway.SaveTest(test);
            if (rowAffect > 0)
            {
                return "Success";
            }
            return "Not save.";
        }

        public List<ViewAllTestWithType> GetViewAllTestWithType()
        {
            return testGateway.GetAllTestWithTypes();
        }
    }
}