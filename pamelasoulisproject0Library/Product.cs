using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
    /// <summary>
    /// Business Logic Product, with a name, price, and ID number
    /// </summary>
    public class Product : BaseBusinessModel
    {

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<OrderLine> OrderLine { get; set; } = new List<OrderLine>();
        public List<Inventory> Inventory { get; set; } = new List<Inventory>();  
    }
}
  