using Microsoft.VisualStudio.TestTools.UnitTesting;
using pamelasoulisproject0Library;
using System;
using Xunit;


namespace pamelasoulisproject0Testing
{
    [TestClass]
    public class CustomerTest
    {
        private readonly Customer _customer = new Customer();

        [Fact]
        public void CustomerNameNotEmptyOrNull() 
        {
            
            Assert.ThrowsException<ArgumentException>(() => _customer.FirstName = string.Empty);
             
        }
        [Fact]
        public void TheCustomerNameNotEmptyOrNull()
        {

            Assert.ThrowsException<ArgumentException>(() => _customer.LastName = string.Empty);

        }

    } 
}
 