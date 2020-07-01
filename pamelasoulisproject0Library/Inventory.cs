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

        /// <summary>
        /// Inventory associated to a particular store
        /// </summary>
        public int LocationId { get; set; }


        /// <summary>
        /// And a  product 
        /// </summary>
        public int ProductId { get; set; }



        /// <summary>
        /// The amount of that product the store has
        /// </summary>
        public int Quantity { get; set; }


        
    }
}
