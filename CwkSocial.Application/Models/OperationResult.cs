using System;
using CwkSocial.Application.Enums;

namespace CwkSocial.Application.Models
{
    public class OperationResult<T>
    {
        public T PayLoad { get; set; }

        public bool IsError { get; private set; }

        public List<Error>? Errors { get; set; } = new List<Error>();

        public void AddError(ErrorCode code, string message)
        {
            Errors.Add(new Error { Code = code, Message = message });
            IsError = true;
        }

        public void ResetIsErrorFlag()
        {
            IsError = false;
        }
    }
}

