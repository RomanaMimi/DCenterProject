using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCenterProject.Model
{
    [Serializable]
    public class Test
    {
       
        public int TestId { get; set;}
        public string TestName { get; set; }
        public decimal Fee { get; set; }
        public int TypeId { get; set; }
    }
}