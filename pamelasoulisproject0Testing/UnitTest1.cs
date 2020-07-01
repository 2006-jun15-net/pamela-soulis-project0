using Microsoft.VisualStudio.TestTools.UnitTesting;
//using pamelasoulisproject0Library;
using pamela_soulis_project0DataAccess.Model;
using System;
using Xunit;


namespace pamelasoulisproject0Testing
{
    [TestClass]
    public class ProductTest
    {
        private readonly Product _product = new Product();

        [Fact]
        public void ProductIdIsNegativeTest() 
        {
            //string randomNameValue = "Ashley"; 
            //_customer.FirstName = randomNameValue;
            //Assert.Equals(randomNameValue, _customer.FirstName);

            //Assert.ThrowsAny<ArgumentException>(() => _product.ProductId = -10);

        }
    } 
}
 