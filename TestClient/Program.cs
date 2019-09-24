using System;
using System.IO;
using Validator.ConfigClasses;
using Validator.DefaultValidator;
using Validator.ValidationResults;

namespace TestClient
{
    internal static class Program
    {
        private static void Main()
        {
            var jsonFile  = Path.Combine("Config", "DefaultConfig.json");
            var validator = new NewtonSoftConfigValidator();
            var results   = validator.ValidateFile(jsonFile);

            switch (results)
            {
                case ValidResults<ConfigRoot> validResults:
                    Console.WriteLine($"Yay! {validResults.Result.Claim.ClaimType}");
                    break;
                case InvalidResults invalidResults:
                    Console.WriteLine("Too bad!");
                    invalidResults.Errors.ForEach(
                        err => Console.WriteLine(
                            $"{err.Path} {err.LineNumber}-{err.LinePosition}: {err.Message}"
                        )
                    );
                    break;
            }
        }
    }
}