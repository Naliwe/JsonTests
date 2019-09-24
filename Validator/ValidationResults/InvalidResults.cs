using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using Validator.Interfaces;

namespace Validator.ValidationResults
{
    public class InvalidResults : IValidationResults
    {
        public InvalidResults(List<ValidationError> errors)
        {
            Errors = errors;
        }

        public List<ValidationError> Errors { get; }

        public bool IsValid { get; } = false;
    }
}