using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Exception
{
    public class NotFoundException: System.Exception
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string message)
            :base(message)
        {
            
        }

        public NotFoundException(string message, System.Exception inner)
            :base(message, inner)
        {

        }

        public NotFoundException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {

        }
    }
}
