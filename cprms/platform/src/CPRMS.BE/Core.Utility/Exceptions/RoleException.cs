using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Exceptions
{
    [Serializable]
    public class RoleException : BaseException
    {
        protected RoleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public RoleException(string message) : base(message, (int)Enums.ErrorCode.Unknow)
        {
        }

        public RoleException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
