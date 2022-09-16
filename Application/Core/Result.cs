using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public int StatusCode { get; set; }

        public static Result<T> Success(T value, int status) => new()
        {
            IsSuccess = true,
            StatusCode = status,
            Value = value
        };
        public static Result<T> Failure(string error, int status) => new()
        {
            IsSuccess = false,
            StatusCode = status,
            Error = error
        };
    }
}