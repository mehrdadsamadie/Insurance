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
    public class ContractServiceUnitTest
    {
        private readonly ContractService _sutcontract;
        private readonly CarrierService _sutcarrier;
        private readonly AdvisorService _sutadvisor;
        private readonly MGAService _sutMga;

        private readonly Mock<IContractRepository> _contractRepositoryMok = new Mock<IContractRepository>();
        private readonly Mock<ICarrierRepository> _carrierRepositoryMok = new Mock<ICarrierRepository>();
        private readonly Mock<IMGARepository> _mGARepositoryMok = new Mock<IMGARepository>();
        private readonly Mock<IAdvisorRepository> _advisorRepositoryMok = new Mock<IAdvisorRepository>();


        public ContractServiceUnitTest()
        {
            _sutcontract = new ContractService(_contractRepositoryMok.Object);
            _sutcarrier = new CarrierService(_carrierRepositoryMok.Object);
            _sutadvisor = new AdvisorService(_advisorRepositoryMok.Object);
            _sutMga = new MGAService(_mGARepositoryMok.Object);
        }


        [Fact]
        public void CreateContract_WhendoublicateContract_ShouldReturnNull()
        {
            var advisor = new Advisor() { Id = 1, FirstName = "Mehrdad", LastName = "Samadie" };
            var carrier = new Carrier() { Id = 1, BusinessName = "English" };
            var mga = new MGA() { Id = 1, BusinessName = "English" };
            var contract1 = new Contract() { Id = 1, AdvisorId = 1, CarrierId = 1, Advisor = advisor, Carrier = carrier };
            var contract2 = new Contract() { Id = 2, AdvisorId = 1, MGAId = 1, Advisor = advisor, MGA = mga };
            var contract3 = new Contract() { Id = 3, AdvisorId = 1, MGAId = 1, Carrier = carrier, MGA = mga };
            var result = new List<Contract>();
            _carrierRepositoryMok.Setup(x => x.Create(carrier)).Returns(carrier);
            _advisorRepositoryMok.Setup(x => x.Create(advisor)).Returns(advisor);
            _mGARepositoryMok.Setup(x => x.Create(mga)).Returns(mga);
            _contractRepositoryMok.Setup(x => x.Create(contract1)).Returns(contract1);
            _contractRepositoryMok.Setup(x => x.Create(contract2)).Returns(contract2);
            _contractRepositoryMok.Setup(x => x.GetIndirect(contract3)).Returns(result);

            advisor = _sutadvisor.CreateWithSaveChange(advisor);
            carrier = _sutcarrier.CreateWithSaveChange(carrier);
            mga = _sutMga.CreateWithSaveChange(mga);
            contract2 = _sutcontract.CreateWithSaveChange(contract2);

            contract3 = _sutcontract.CreateWithSaveChange(contract3);


            Assert.Null(contract3);


        }
    }
}
