using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingLogger.Exceptions
{
    public class MissingEntityException : Exception
    {
        public MissingEntityException()
        {
        }

        public MissingEntityException(string message) : base(message)
        {
        }

        public MissingEntityException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
