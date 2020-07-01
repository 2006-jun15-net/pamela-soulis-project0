using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
    /// <summary>
    /// Business Logic Product, with a name, price, and ID number
    /// </summary>
    public class Product : BaseBusinessModel
    {

        private string _name; 
        private decimal _price;

        public int ProductId { get; set; }
        

        /// <summary>
        /// The name of the product a customer can purcahse
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
        /// The price of that product
        /// </summary>
        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Product price cannot be zero.", nameof(value));
                }
                _price = value;
            }
        }




        public List<OrderLine> OrderLine { get; set; } = new List<OrderLine>();
        public List<Inventory> Inventory { get; set; } = new List<Inventory>();  
    }
}
  