using BoilerplateService.Infrastructures.Contexts.Interfaces;
using BoilerplateService.Models.Entities;
using BoilerplateService.Repositories.Base;
using BoilerplateService.Repositories.Interfaces;

namespace BoilerplateService.Repositories
{
    public class OrganizationRepository : DynamoRepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(IAmazonDynamoDB client, IDynamoDatabaseContext context)
            : base(client, context)
        {
        }

        public override async Task CreateTableAsync()
        {
            var keySchemas = new List<KeySchemaElement>
            {
                new KeySchemaElement(nameof(Organization.UUID), KeyType.HASH),
                // new KeySchemaElement(nameof(Organization.CreatedAtUtc), KeyType.RANGE)
            };
            var request = new CreateTableRequest
            {
                TableName = "Organizations",
                KeySchema = keySchemas,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition(nameof(Organization.UUID), ScalarAttributeType.S),
                    // new AttributeDefinition(nameof(Organization.CreatedAtUtc), ScalarAttributeType.S)
                },
                // LocalSecondaryIndexes = new List<LocalSecondaryIndex>
                // {
                //     new LocalSecondaryIndex()
                //     {
                //         IndexName = "OrganizationNameIndex",
                //         KeySchema = keySchemas,
                //         Projection = new Projection()
                //         {
                //             ProjectionType = ProjectionType.ALL
                //         }
                //     }
                // }
            };

            await _context.CreateTableAsync(request);
        }
    }
}