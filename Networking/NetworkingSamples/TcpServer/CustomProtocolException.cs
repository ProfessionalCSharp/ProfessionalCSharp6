using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TcpServer
{


    public class CustomProtocolException : Exception
    {
        public CustomProtocolException() { }
        public CustomProtocolException(string message) : base(message) { }
        public CustomProtocolException(string message, Exception inner) : base(message, inner) { }

    }
}
