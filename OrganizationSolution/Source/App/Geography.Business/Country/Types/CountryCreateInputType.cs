using Geography.Business.State.Types;
using GraphQL.Types;

namespace Geography.Business.Country.Types
{
    public class CountryCreateInputType : InputObjectGraphType
    {
        public CountryCreateInputType()
        {
            Name = "CountryCreateInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<StringGraphType>("isoCode");
            Field<ContinentType> ("continent");
            Field<ListGraphType<StateCreateInputType>>("states");
        }
    }

    public class CountryUpdateInputType : InputObjectGraphType
    {
        public CountryUpdateInputType()
        {
            Name = "CountryUpdateInput";
            Field<NonNullGraphType<IdGraphType>>("Id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("isoCode");
            Field<ContinentType>("continent");
            Field<ListGraphType<StateUpdateInputType>>("states");
        }
    }

    public class File
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; }
    }

    public class FileGraphType : ObjectGraphType<File>
    {
        public FileGraphType()
        {
            Name = "FileInputType";
            Field<StringGraphType>("id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("mimetype");
            Field<StringGraphType>("path");
        }
    }
}
