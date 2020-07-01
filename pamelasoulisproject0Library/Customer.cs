using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
    /// <summary>
    /// Business Logic Customer, with a name and Id number 
    /// </summary>
    public class Customer : BaseBusinessModel
    {
        private string _firstname;
        private string _lastname;


        /// <summary>
        /// Customer has a firstname
        /// </summary>
        public string FirstName
        {
            get => _firstname;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Customer firstname cannot be empty or null.", nameof(value));
                }
                _firstname = value;
            }
        }

        /// <summary>
        /// And an ID number
        /// </summary>
        public int CustomerId { get; set; }

        
        /// <summary>
        /// And a lastname
        /// </summary>
        public string LastName
        {
            get => _lastname;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Customer firstname cannot be empty or null.", nameof(value));
                }
                _lastname = value;
            }
        }

        public List<Orders> Orders { get; set; } = new List<Orders>(); 
    }
}
