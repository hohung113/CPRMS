using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Exceptions
{
    [Serializable]
    public class NotFoundException : BaseException
    {
        private const string message = "404 Not Found.";
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public NotFoundException() : base(message, (int)Enums.ErrorCode.NotExist)
        {
        }

        public NotFoundException(string message) : base(message, (int)Enums.ErrorCode.NotExist)
        {

        }
    }
}
