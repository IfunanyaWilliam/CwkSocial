using System;
namespace CwkSocial.Application.Models
{
    public class OperationResult<T>
    {
        public T PayLoad { get; set; }

        public bool IsError { get; set; }

        public List<Error>? Errors { get; set; } = new List<Error>();
    }
}

