using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
    /// <summary>
    /// Business Logic Orders, with an ID number and associated customer and location, and date/time that the order was placed on
    /// </summary>
    public class Orders : BaseBusinessModel
    {

        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }


        public List<OrderLine> OrderLine { get; set; } = new List<OrderLine>();
    }
}
 