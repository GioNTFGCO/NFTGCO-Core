using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFTGCO.Core.Promise
{
    /// <summary>
    /// Base class for promise exceptions.
    /// </summary>
    public class PromiseException : Exception
    {
        public PromiseException() { }

        public PromiseException(string message) : base(message) { }

        public PromiseException(string message, Exception inner) : base(message, inner) { }
    }
}
