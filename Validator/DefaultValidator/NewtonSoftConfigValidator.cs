using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Validator.ConfigClasses;
using Validator.Interfaces;
using Validator.ValidationResults;

namespace Validator.DefaultValidator
{
    public class NewtonSoftConfigValidator : IValidateJson
    {
        private readonly JSchemaGenerator _generator = new JSchemaGenerator();

        private readonly JSchema _rootSchema;
        private ConfigRoot _config;

        private IList<ValidationError> _errors = new List<ValidationError>();

        public NewtonSoftConfigValidator()
        {
            _generator.GenerationProviders.Add(new StringEnumGenerationProvider());
            _generator.GenerationProviders.Add(new ProvidersWidgetConfig.SchemaGenerator());
            _rootSchema = _generator.Generate(typeof(ConfigRoot));
        }

        public IValidationResults ValidateString(string json)
        {
            try
            {
                var configRoot = JObject.Parse(json);
                var isValid    = configRoot.IsValid(_rootSchema, out _errors);

                if (!isValid) return new InvalidResults(_errors.ToList());

                _config = JsonConvert.DeserializeObject<ConfigRoot>(json);

                return new ValidResults<ConfigRoot>(_config);
            }
            catch
            {
                return new InvalidResults(_errors.ToList());
            }
        }

        public IValidationResults ValidateFile(string jsonFilePath)
        {
            return ValidateString(File.ReadAllText(jsonFilePath));
        }
    }
}