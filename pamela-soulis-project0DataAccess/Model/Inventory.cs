using System;
using System.Collections.Generic;

namespace pamela_soulis_project0DataAccess.Model
{

    /// <summary>
    /// Junction table Inventory, with an associated location, product, and product quantity available
    /// </summary>
    public partial class Inventory : DataModel
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
