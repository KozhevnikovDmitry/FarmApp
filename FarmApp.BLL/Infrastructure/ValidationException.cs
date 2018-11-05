using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.BLL.Infrastructure
{
    /*
     * CR-1 - get rid of this class, implement validation logic without throwing exceptions
     */
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
