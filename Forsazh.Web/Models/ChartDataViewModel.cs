using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleOfDetails.Web.Models
{
    public class ChartDataViewModel
    {
        public string name { get; set; }
        public IEnumerable<int> data { get; set; } 
    }
}