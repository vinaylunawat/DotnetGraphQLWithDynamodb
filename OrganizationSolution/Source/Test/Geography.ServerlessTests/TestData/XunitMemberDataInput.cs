using Amazon.Runtime.Internal.Transform;
using Geography.Serverless.Model;
using Geography.ServerlessTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography.ServerlessTests.TestData
{
    public class XunitMemberDataInput
    {
        public static IEnumerable<object[]> GraphQLModelData() =>
        new List<GraphQLModel[]>
          {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query countries { countries { id name isoCode } }",
                                          Variables=null
                        }
                    }

          };
        public static IEnumerable<object[]> GraphQLModelDataForCreate() =>
        new List<GraphQLModel[]>
         {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query countries { countries { id name isoCode continent} }",
                                          Variables=null
                        },
                         new GraphQLModel{ Query="mutation($country: CountryCreateInput!) { createCountry(country: $country) { id name isoCode continent} }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              {   "country", new Dictionary<string ,string>{ 
                                                  {"name", "Test" } ,
                                                  { "isoCode","TT" } ,
                                                  {"continent","ASIA" }
                                              } }
                                          }

                        }
                    }

         };

        public static IEnumerable<object[]> GraphQLModelDataForDelete() =>
       new List<GraphQLModel[]>
        {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query countries { countries { id name isoCode continent} }",
                                          Variables=null
                        },
                         new GraphQLModel{ Query="mutation($country: CountryCreateInput!) { createCountry(country: $country) { id name isoCode continent} }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              {   "country", new Dictionary<string ,string>{
                                                  {"name", "Test" } ,
                                                  { "isoCode","TT" } ,
                                                  {"continent","ASIA" }
                                              } }
                                          }

                        },
                        new GraphQLModel{ Query="mutation($countryId: ID!) { deleteCountry(countryId: $countryId) }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "countryId", "-1" }
                                          }
                        }
                    }

        };

        public static IEnumerable<object[]> GraphQLModelDataForUpdate() =>
       new List<GraphQLModel[]>
        {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query countries { countries { id name isoCode continent} }",
                                          Variables=null
                        },
                         new GraphQLModel{ Query="mutation($country: CountryCreateInput!) { createCountry(country: $country) { id name isoCode continent} }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              {   "country", new Dictionary<string ,string>{
                                                  {"name", "Test" } ,
                                                  { "isoCode","TT" } ,
                                                  {"continent","ASIA" }
                                              } }
                                          }

                        },
                            new GraphQLModel{ Query="mutation($country: CountryUpdateInput!) { updateCountry(country: $country) { id name isoCode } }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "country", new Dictionary<string ,string>{

                                                  {"id", "-1" } ,
                                                  {"name", "TestUpdate" } ,
                                                  {"isoCode","UT" } ,
                                                  {"continent","AFRICA" }
                                              } }
                                          }
                        },
                        new GraphQLModel{ Query="mutation($countryId: ID!) { deleteCountry(countryId: $countryId) }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "countryId", "-1" }
                                          }
                        }
                    }

        };
        public static IEnumerable<object[]> GraphQLModelDataForInvalidUpdate() =>
       new List<GraphQLModel[]>
           {
                    new GraphQLModel[]
                    {
                         new GraphQLModel{ Query="mutation($country: CountryUpdateInput!) { updateCountry(country: $country) { id name isoCode } }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "country", new Dictionary<string ,string>{

                                                  {"id", Guid.NewGuid().ToString() } ,
                                                  {"name", "Test" } ,
                                                  {"isoCode","TT" } ,
                                                  {"continent","ASIA" }
                                              } }
                                          }
                        }
                    }

           };
        public static IEnumerable<object[]> GraphQLModelDeleteInvalidData() =>
        new List<GraphQLModel[]>
           {
                    new GraphQLModel[]
                    {
                         new GraphQLModel{ Query="mutation($countryId: ID!) { deleteCountry(countryId: $countryId) }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "countryId", "-1" }
                                          }
                        }
                    }

           };
       

        public static IEnumerable<object[]> GraphQLModelDataWithIsoAlreadyExist() =>
        new List<GraphQLModel[]>
         {
                    new GraphQLModel[]
                    {
                         new GraphQLModel{ Query="mutation($country: CountryCreateInput!) { createCountry(country: $country) { id name isoCode } }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "country", new Dictionary<string ,string>{

                                                  {"name", "Test" } ,
                                                  { "isoCode","TT" } ,
                                                  {"continent","ASIA" }
                                              } }
                                          }
                        }
                    }

         };
    }
}
