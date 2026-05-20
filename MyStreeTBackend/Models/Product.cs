using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStreeTBackend.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; } 
        public int StockQty { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}