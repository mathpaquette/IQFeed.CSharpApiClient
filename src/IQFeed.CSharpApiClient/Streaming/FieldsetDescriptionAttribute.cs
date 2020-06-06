using System;

namespace IQFeed.CSharpApiClient.Streaming
{
    // *KLUDGE* - This class shoud be internal for standard API builds.  Define symbol DYNAMIC_FIELD_GENERATOR in IQFeed.CSharpApiClient and IQFeed.CSharpApiClient.Examples to enable the code generator
#if DYNAMIC_FIELD_GENERATOR
    public class FieldsetDescriptionAttribute : Attribute
#else
    internal class FieldsetDescriptionAttribute : Attribute
#endif
    {
        public string Name { get; }
        public Type Type { get; }

        public FieldsetDescriptionAttribute(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}