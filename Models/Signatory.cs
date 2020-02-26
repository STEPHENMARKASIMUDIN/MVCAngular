using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAngular
{
    public class Signatory
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Division { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public int IsActive { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
    }
}