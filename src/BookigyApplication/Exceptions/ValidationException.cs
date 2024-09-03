using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Application.Abstractions.Behaviors;

namespace Bookify.Application.Exceptions
{ 
    public class ValidationException(IEnumerable<ValidationError> errors) : Exception
    {
        public IEnumerable<ValidationError> Errors { get; set; } = errors;
    }
}
