using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace pamelasoulisproject0Library
{

    /// <summary>
    /// Business Logic Location, with a store name and ID number
    /// </summary>
    public class Location : BaseBusinessModel
    {

        private string _name; 
        


        /// <summary>
        /// Store has a name
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Customer firstname cannot be empty or null.", nameof(value));
                }
                _name = value;
            }
        }


        /// <summary>
        /// And an ID number
        /// </summary>
        public int LocationId { get; set; }

        public List<Inventory> Inventory { get; set; } = new List<Inventory>();

        public List<Orders> Orders { get; set; } = new List<Orders>();
    }
}
 