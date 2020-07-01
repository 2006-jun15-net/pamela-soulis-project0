using Microsoft.VisualStudio.TestTools.UnitTesting;
using pamelasoulisproject0Library;
using System;
using Xunit;


namespace pamelasoulisproject0Testing
{
    [TestClass]
    public class LocationTest
    {
        private readonly Location _location = new Location();

        [Fact]
        public void LocationNameNotEmptyOrNull()
        {

            Assert.ThrowsException<ArgumentException>(() => _location.Name = string.Empty);

        }

    } 
}