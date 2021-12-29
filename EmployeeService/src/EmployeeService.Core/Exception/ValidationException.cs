using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Exception
{
    public class ValidationException : System.Exception
    {
        public ValidationException()
        {

        }

        public ValidationException(string message)
            : base(message)
        {

        }

        public ValidationException(string message, System.Exception inner)
            : base(message, inner)
        {

        }

        public ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
