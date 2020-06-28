using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
    public class Inventory : BaseBusinessModel
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }


        
    }
}
