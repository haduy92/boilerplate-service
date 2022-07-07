using BoilerplateService.Models.Entities;

namespace BoilerplateService.Models.Dtos
{
    public class UpdateOrganizationDto
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string TaxInfo { get; set; }
        public OrganizationType Type { get; set; }
        public StatusType Status { get; set; }
        public DateTime OnboardingDate { get; set; }
        public DateTime? OffboardingDate { get; set; }

        public AddressDetail Address { get; set; }
        public ContactDetail ContactPerson { get; set; }
        public PaymentDetail BankDetail { get; set; }
    }
}