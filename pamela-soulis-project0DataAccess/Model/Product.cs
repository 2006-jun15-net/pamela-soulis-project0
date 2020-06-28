using System;
using System.Collections.Generic;

namespace pamela_soulis_project0DataAccess.Model
{
    public partial class Product : DataModel
    {
        public Product()
        {
            Inventory = new HashSet<Inventory>();
            OrderLine = new HashSet<OrderLine>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
