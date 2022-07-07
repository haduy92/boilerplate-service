namespace BoilerplateService.Models.Entities
{
    public class ContactDetail
    {
        public string Name { get; set; }
        public ContactType Type { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public enum ContactType
    {
        Billing,
        Administrative,
        Hr,
        Payor,
        Patient,
        Nurse
    }
}