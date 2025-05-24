using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Enums
{
    public enum ErrorCode
    {
        Unknow = 0,
        IsRequired = 1,
        InvalidFormat = 2,
        ExceedMaxLength = 3,
        NotExist = 4,
        IsDuplicate = 5,
        InvalidOperation = 6,
        MyInfoErrorCode = 10,
        CookieIsExpired = 11,
        LogoutCookie = 12,
        InvalidLogin = 13,
        ValidationError = 100,
        Unauthorized = 200,   
        Forbidden = 201,   
        NotFound = 300,
        Timeout = 400,              
        Conflict = 401,  
        InternalError = 500,       
        ServiceUnavailable = 501,
        CustomBusinessRule = 900    
    }
    public enum ResultStatus
    {
        Success = 0,
        Failed = 1
    }
}
