using System;
using System.Collections.Generic;

namespace pamela_soulis_project0DataAccess.Model
{
    /// <summary>
    /// Junction table Orderline
    /// </summary>
    public partial class OrderLine : DataModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
