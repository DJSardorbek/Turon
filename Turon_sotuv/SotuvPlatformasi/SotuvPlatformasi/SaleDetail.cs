using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotuvPlatformasi
{
    public class SaleDetail
    {
        public int product_id { get; set; }
        public string name { get; set; }
        public string measurement { get; set; }
        public double quantity { get; set; }
        public string val_ul { get; set; }
        public double price { get; set; }
        public double total { get; set; }
    }
}
