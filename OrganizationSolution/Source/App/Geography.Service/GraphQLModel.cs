using Newtonsoft.Json.Linq;

namespace Geography.Service
{
    public class GraphQLModel
    {
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}