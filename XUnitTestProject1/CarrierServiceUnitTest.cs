using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Insurance.Service;
using Insurance.BusinessLogicLayer;
using Insurance.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Insurance.UnitTest
{
     public class CarrierServiceUnitTest
    {
        private readonly CarrierService _sut;
        private readonly Mock<ICarrierRepository> _carrierRepositoryMok = new Mock<ICarrierRepository>();
        public CarrierServiceUnitTest() 
        {
            _sut = new CarrierService(_carrierRepositoryMok.Object);
        }
        [Fact]
        public void GetAll_ShouldReturnsAllCarrier_WhenListIsNotEmpty()
        {
            //Arrange
           var carriers = new List<Carrier>
            {
                new Carrier
                {
                    Id=5,
                    BusinessName="Johne",

                }
            }.AsQueryable();

            _carrierRepositoryMok.Setup(t => t.FindAll()).Returns(carriers);

            //Act
            var result = _sut.FindAll();
            //Assert
            Assert.Single(result);
        }
        [Fact]
        public void GetCarrierById_ShouldReturnCarrier_WhenListIsNotEmpty()
        {
            //Arange
            int carrierId = 5;
            var carriers = new List<Carrier>
            {
                new Carrier
                {
                    Id=carrierId,
                    BusinessName="Johne",

                }
            }.AsQueryable();

            _carrierRepositoryMok.Setup(x => x.FindByCondition(x=>x.Id==carrierId)).Returns(carriers);

            //Act
            var newcarrier = _sut.FindByCondition(x => x.Id == 5);
            //Assert
            Assert.Single(newcarrier);
        }
    }
}
