namespace BoilerplateService.Models.Entities.Base
{
    public abstract class EntityBase
    {
        [DynamoDBHashKey]
        public string UUID { get; set; } = Guid.NewGuid().ToString();
        public string CreatedBy { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}