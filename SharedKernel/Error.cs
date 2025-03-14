using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public class Error
    {
        public string Code { get; private set; }
        public string Description { get; private set; }
        public ErrorType Type { get; private set; }


        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static readonly Error NullValue = new(
            "General.Null",
            "Null value was provided",
            ErrorType.Failure);

        public Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            this.Type = errorType;
        }

        public static Error Failure(string code, string description)
        {
            return new(code, description, ErrorType.Failure);
        }

        public static Error NotFound(string code, string description)
        {
            return new(code, description, ErrorType.NotFound);
        }

        public static Error Problem(string code, string description)
        {
            return new(code, description, ErrorType.Problem);
        }

        public static Error Conflict(string code, string description)
        {
            return new(code, description, ErrorType.Conflict);
        }
    }
}
