using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Exceptions
{
    [Serializable]
    public class NotExistException : BaseException
    {
        protected NotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public NotExistException(string message) : base(message, (int)Enums.ErrorCode.NotExist)
        {
        }
    }
}
