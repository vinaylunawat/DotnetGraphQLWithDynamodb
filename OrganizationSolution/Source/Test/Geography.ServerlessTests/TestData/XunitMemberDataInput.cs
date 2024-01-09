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
        public static IEnumerable<object[]> GraphQLModelDatas() =>
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
                                              { "country", new Dictionary<string ,string>{

                                                  {"name", "Country One" } ,
                                                  { "isoCode","US" } ,
                                                  {"continent","ASIA" }
                                              } }
                                          }

                        }
                    }

         };
        public static IEnumerable<object[]> GraphQLModelDelete() =>
        new List<GraphQLModel[]>
           {
                    new GraphQLModel[]
                    {
                         new GraphQLModel{ Query="mutation($countryId: ID!) { deleteCountry(countryId: $countryId) }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "countryId", "20020" }
                                          }
                        }
                    }

           };
        public static IEnumerable<object[]> GraphQLModelDataForUpdate() =>
        new List<GraphQLModel[]>
            {
                    new GraphQLModel[]
                    {
                         new GraphQLModel{ Query="mutation($country: CountryUpdateInput!) { updateCountry(country: $country) { id name isoCode } }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "country", new Dictionary<string ,string>{

                                                  {"id", "20025" } ,
                                                  {"name", "Country One" } ,
                                                  { "isoCode","US" } ,
                                                  {"continent","ASIA" }
                                              } }
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

                                                  {"name", "Country One" } ,
                                                  { "isoCode","US" } ,
                                                  {"continent","ASIA" }
                                              } }
                                          }
                        }
                    }

         };
    }
}
