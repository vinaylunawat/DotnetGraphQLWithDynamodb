﻿namespace Geography.Entity.Entities
{
    using Amazon.DynamoDBv2.DataModel;
    using Framework.Entity.Entities;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Defines the <see cref="Country" />.
    /// </summary>
    [DynamoDBTable("Country")]
    public class Country : EntityWithId<Guid>
    {
        [DynamoDBProperty("Name")]
        public string Name { get; set; }

        [DynamoDBProperty("IsoCode")]
        public string IsoCode { get; set; }

        [DynamoDBProperty("Continent")]
        public string Continent { get; set; }

        //public Continent Continent
        //{
        //    get => (Continent)ContinentInt;
        //    set => ContinentInt = (int)value;
        //}

    }

}
