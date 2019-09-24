using Validator.Interfaces;

namespace Validator.ValidationResults
{
    public class ValidResults<TResult> : IValidationResults
    {
        public ValidResults(TResult result)
        {
            Result = result;
        }

        public TResult Result { get; }

        public bool IsValid { get; } = true;
    }
}