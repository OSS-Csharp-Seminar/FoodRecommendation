using System;
using System.Collections.Generic;

namespace Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base("Validation error occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> errors) : base("Validation error occurred.")
        {
            Errors = errors;
        }

        public ValidationException(string message) : base(message)
        {
            LogError(message);
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            LogError(message);
        }

        public ValidationException(string message, IDictionary<string, string[]> errors) : base(message)
        {
            Errors = errors;
            LogError(message);
        }

        private void LogError(string message)
        {
            Console.WriteLine($"Validation error: {message}");
        }
    }
}
