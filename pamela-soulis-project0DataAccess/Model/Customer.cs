using System;
using System.Collections.Generic;

namespace pamela_soulis_project0DataAccess.Model
{

    /// <summary>
    /// Data Access Entity Customer, with a name and ID number
    /// </summary>
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
