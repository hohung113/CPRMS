using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Enums
{
    public enum ErrorCode
    {
        // Dinh nghia cac error code cho fe bat de hien message
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
    }
    public enum ResultStatus
    {
        Success = 0,
        Failed = 1
    }
}
