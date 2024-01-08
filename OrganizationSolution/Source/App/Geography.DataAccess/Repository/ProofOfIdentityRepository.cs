namespace Geography.DataAccess.Repository
{
    using Amazon.DynamoDBv2.DataModel;
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ProofOfIdentityRepository" />.
    /// </summary>
    public class ProofOfIdentityRepository : GenericRepository<ProofOfIdentity>, IProofOfIdentityRepository
    {
        public ProofOfIdentityRepository(IDynamoDBContext dbContext) : base(dbContext)
        {
        }
    }
}
