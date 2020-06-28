using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace pamelasoulisproject0Library
{
    public class Location : BaseBusinessModel
    {
        public string Name { get; set; }
        public int LocationId { get; set; }

        public List<Inventory> StoreInventory { get; set; } = new List<Inventory>();

        public List<Orders> StoreOrders { get; set; } = new List<Orders>();
    }
}
