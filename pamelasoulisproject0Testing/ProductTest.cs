using Microsoft.VisualStudio.TestTools.UnitTesting;
using pamelasoulisproject0Library;
using System;
using Xunit;


namespace pamelasoulisproject0Testing
{
    [TestClass]
    public class ProductTest
    {
        private readonly Product _product = new Product();

        [Fact]
        public void ProductNameNotEmptyOrNull()
        {

            Assert.ThrowsException<ArgumentException>(() => _product.Name = string.Empty);

        }
        [Fact]
        public void ProductPriceNotZero()
        {
            Assert.ThrowsException<ArgumentException>(() => _product.Price = 0);
        }
    }
}