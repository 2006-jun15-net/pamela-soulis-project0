using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
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
 