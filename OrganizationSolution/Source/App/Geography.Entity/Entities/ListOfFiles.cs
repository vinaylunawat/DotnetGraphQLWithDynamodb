using Amazon.DynamoDBv2.DataModel;
using Framework.Entity.Entities;
using System;

namespace Geography.Entity.Entities
{

    [DynamoDBTable("ListOfFiles")]
    public class ListOfFiles : EntityWithId<Guid>
    {
        [DynamoDBProperty("EntityId")]
        public string EntityId { get; set; }

        [DynamoDBProperty("PreSignedUrl")]
        public string PreSignedUrl { get; set; }
    }
    
}
