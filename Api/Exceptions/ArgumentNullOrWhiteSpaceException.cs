using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingLogger.Exceptions
{
    public class ArgumentNullOrWhiteSpaceException : Exception
    {
        public ArgumentNullOrWhiteSpaceException()
        {
        }

        public ArgumentNullOrWhiteSpaceException(string message) : base(message)
        {
        }

        public ArgumentNullOrWhiteSpaceException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
