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

        public int? AdvisorId { get; set; }

        public int? MGAId { get; set; }

        public int? CarrierId { get; set; }

    }
    public class ContractCreated 
    {
        public string AdvisorId { get; set; }

        public string MGAId { get; set; }

        public string CarrierId { get; set; }
    }
    public class ContractResult
    {
        public int? Id { get;  set; }
        public int? AdvisorId { get;  set; }
        public string AdvisorLastName { get;  set; }
        public string AdvisorFirstName { get;  set; }
        public int? MGAId { get;  set; }
        public string MGABusinessName { get;  set; }
        public int? CarrierId { get;  set; }
        public string CarrierBusinessName { get;  set; }
    }
    public class ContractList 
    {
        public ContractResult Direct { get; set; }
        public List<ContractResult> IndirectList { get; set; }
    }
    public class ContractParticipantAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ContractCreate contract = (ContractCreate)validationContext.ObjectInstance;

            if (contract.AdvisorId==null && contract.MGAId==null &&contract.CarrierId==null)
            {
                return new ValidationResult("Contract is between two Entity");
            }

            return ValidationResult.Success;
        }
    }
}
