using BoilerplateService.Models.Entities.Base;

namespace BoilerplateService.Models.Entities
{
    [DynamoDBTable("Organizations")]
    public class Organization : EntityBase
    {
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public string Alias { get; set; }
        [DynamoDBProperty]
        public string Description { get; set; }
        [DynamoDBProperty]
        public string TaxInfo { get; set; }
        [DynamoDBProperty]
        public OrganizationType Type { get; set; }
        [DynamoDBProperty]
        public StatusType Status { get; set; }
        [DynamoDBProperty]
        public DateTime OnboardingDate { get; set; }
        [DynamoDBProperty]
        public DateTime OffboardingDate { get; set; }
        [DynamoDBProperty]
        public AddressDetail Address { get; set; }
        [DynamoDBProperty]
        public ContactDetail ContactPerson { get; set; }
        [DynamoDBProperty]
        public PaymentDetail BankDetail { get; set; }
    }

    public enum OrganizationType
    {
        Insurance,
        Hospital,
        HealthCareBoilerplate,
        Government,
        Commercial
    }
}