using Insurance.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Insurance.Web.Model
{


    [ContractParticipant]
    public class ContractCreate
    {
        public Contractor FirstContractor { get; set; }
        public Contractor SecondContractor { get; set; }
    }
    public class ContractorCreated
    {
        public string AdvisorId { get; set; }

        public string MGAId { get; set; }

        public string CarrierId { get; set; }
    }
    public class ContractCreateed
    {
        public ContractorCreated FirstContractor { get; set; }
        public ContractorCreated SecondContractor { get; set; }
    }

    public class ContractResult
    {
        public int? Id { get; set; }
        public int? AdvisorId { get; set; }
        public string AdvisorLastName { get; set; }
        public string AdvisorFirstName { get; set; }
        public int? MGAId { get; set; }
        public string MGABusinessName { get; set; }
        public int? CarrierId { get; set; }
        public string CarrierBusinessName { get; set; }
    }
    public class ContractList
    {
        public ContractResult Direct { get; set; }
        public ContractResult IndirectList { get; set; }
    }
    public class ContractParticipantAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ContractCreate _contract = (ContractCreate)validationContext.ObjectInstance;

            if (_contract.FirstContractor.AdvisorId == null ^ _contract.FirstContractor.MGAId == null ^ _contract.FirstContractor.CarrierId == null)
            {
                return new ValidationResult("Contract is between two Entity");
            }
            if (_contract.SecondContractor.AdvisorId == null ^ _contract.SecondContractor.MGAId == null ^ _contract.SecondContractor.CarrierId == null)
            {
                return new ValidationResult("Contract is between two Entity");
            }
            if (_contract.FirstContractor.AdvisorId == _contract.SecondContractor.AdvisorId &&
                _contract.FirstContractor.CarrierId == _contract.SecondContractor.CarrierId &&
                _contract.FirstContractor.MGAId == _contract.SecondContractor.MGAId
                )
            {
                return new ValidationResult("Duplicate is not possible");
            }
            return ValidationResult.Success;
        }
    }
}
