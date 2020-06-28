using System;
using System.Collections.Generic;

namespace pamela_soulis_project0DataAccess.Model
{
    public partial class Customer : DataModel
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
