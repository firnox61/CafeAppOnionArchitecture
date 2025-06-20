﻿using FluentValidation.Results;
using Newtonsoft.Json;

namespace Cafe.WebAPI.Middlewares
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        // public IEnumerable<ValidationFailure> Errors { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
