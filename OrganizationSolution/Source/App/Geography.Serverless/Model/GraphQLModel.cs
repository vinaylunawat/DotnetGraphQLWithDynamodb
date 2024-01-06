using Newtonsoft.Json.Linq;

namespace Geography.Serverless.Model
{
    public class GraphQLModel
    {
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
