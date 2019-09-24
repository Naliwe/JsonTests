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
            var files = Directory.GetFiles("Config", "*.json");

            foreach (var file in files)
            {
                var validator = new NewtonSoftConfigValidator();
                var results   = validator.ValidateFile(file);

                switch (results)
                {
                    case ValidResults<ConfigRoot> validResults:
                        Console.WriteLine($"Yay! {validResults.Result.Claim.ClaimType}");
                        break;
                    case InvalidResults invalidResults:
                        Console.WriteLine($"Too bad! File {file} has errors");
                        invalidResults.Errors.ForEach(
                            err => Console.WriteLine(
                                $"\t> {err.Path} {err.LineNumber}-{err.LinePosition}: {err.Message}"
                            )
                        );
                        break;
                }
            }
        }
    }
}