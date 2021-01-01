using Insurance.BusinessLogicLayer;
using Insurance.Entity;
using Insurance.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Insurance.UnitTest
{
    public class AdvisorServiceUnitTest
    {
        private readonly AdvisorService _sut;
        private readonly Mock<IAdvisorRepository> _advisorRepositoryMok = new Mock<IAdvisorRepository>();
        public AdvisorServiceUnitTest()
        {
            _sut = new AdvisorService(_advisorRepositoryMok.Object);
        }
        [Fact]
        public void CreateAdvisor_ShouldReturNewCarrier_AutoGenrateHealthStatus()
        {
            //Arange
            var _newAdvisor = new Advisor()
            {
                FirstName = "John",
                LastName = "Doari"
            };
            var _returnAdvisor = new Advisor();
            _advisorRepositoryMok.Setup(x => x.Create(_newAdvisor)).Returns(_newAdvisor);

            //Act
         _returnAdvisor = _sut.CreateWithSaveChange(_newAdvisor);
            //Assert
            Assert.NotNull(_returnAdvisor.HealthStatus);
            Assert.Equal(_returnAdvisor.FirstName, _newAdvisor.FirstName);
            Assert.Equal(_returnAdvisor.LastName, _newAdvisor.LastName);
        }
    }
}
