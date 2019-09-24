namespace Validator.Interfaces
{
    public interface IValidateJson
    {
        IValidationResults ValidateString(string json);
        IValidationResults ValidateFile(string jsonFilePath);
    }
}