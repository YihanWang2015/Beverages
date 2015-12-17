using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cis237Assignment6.Models
{
    public partial class Beverage
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pack { get; set; }
        public decimal price { get; set; }
        public bool active { get; set; }

    }
}