using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
    /// <summary>
    /// Junction table Inventory, with an associated location, product, and product quantity available
    /// </summary>
    public class Inventory : BaseBusinessModel
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }


        
    }
}
