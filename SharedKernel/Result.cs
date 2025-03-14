using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public Error Error { get; private set; }

        public Result(bool isSuccess, Error error)
        {

            if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, Error.None);
        }

        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }


    }

    public class Result<TValue> : Result
    {
        private TValue? _value { get; set; }

        public Result(bool isSuccess, Error error, TValue? value) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("No value");

        public static Result<TValue> Success(TValue value)
        {
            return new Result<TValue>(true, Error.None, value);
        }

        public new static Result<TValue> Failure(Error error)
        {
            return new Result<TValue>(false, error, default!);
        }

        public static Result<TValue> ValidationFailure(Error error)
        {
            return new(false, error, default);
        }
    }
}
