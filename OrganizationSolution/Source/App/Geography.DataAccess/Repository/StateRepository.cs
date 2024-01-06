namespace Geography.DataAccess.Repository
{
    using Amazon.DynamoDBv2.DataModel;
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;
    using LinqKit;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="StateRepository" />.
    /// </summary>
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(IDynamoDBContext dbContext) : base(dbContext)
        {
        }                 
    }
}
