﻿using System;
using System.Collections.Generic;
using System.Text;

namespace pamelasoulisproject0Library
{
    public class Customer : BaseBusinessModel
    {

        public string FirstName { get; set; }
        public int CustomerId { get; set; }

        public string LastName { get; set; }


        public List<Orders> Orders { get; set; } = new List<Orders>(); 
    }
}
