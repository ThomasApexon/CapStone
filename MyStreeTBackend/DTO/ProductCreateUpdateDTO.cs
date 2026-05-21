using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStreeTBackend.DTO
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; } // CSV format: "7,8,9,10"
        public int StockQty { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }

    public class ProductUpdateDTO
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public int StockQty { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
