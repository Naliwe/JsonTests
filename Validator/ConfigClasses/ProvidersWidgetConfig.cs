using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Validator.ConfigClasses
{
    public class ProvidersWidgetConfig
    {
        public string Path { get; set; }

        public class SchemaGenerator : JSchemaGenerationProvider
        {
            public override JSchema GetSchema(JSchemaTypeGenerationContext context)
            {
                var generator = new JSchemaGenerator();
                var schema    = generator.Generate(context.ObjectType);

                schema.Properties["Path"].Pattern = @"(\b\.)+";

                return schema;
            }

            public override bool CanGenerateSchema(JSchemaTypeGenerationContext context)
            {
                return base.CanGenerateSchema(context) &&
                       context.ObjectType == typeof(ProvidersWidgetConfig);
            }
        }
    }
}