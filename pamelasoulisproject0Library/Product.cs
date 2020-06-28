using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
    public class Product : BaseBusinessModel
    {

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<OrderLine> TheOrders { get; set; } = new List<OrderLine>();
        public List<Inventory> TheInventory { get; set; } = new List<Inventory>();  
    }
}
 