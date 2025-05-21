using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models.Enums
{
    public enum RMSEnum
    {
    }

    #region Project
    public enum StatusProject
    {
        Propose = 1000,
        NeedUpdate = 1001,
        Passed = 1002,
        Rejected = 1003
    }

    public enum KindOfPersonMakeRegister
    {
        Lecture = 1000,
        Student = 1001,
    }
 

    public enum ProjectType
    {
        Website = 1000,
        Mobile = 1001,
        Desktop = 1002,
        API = 1003,
        Game = 1004,
        IoT = 1005,
        AI = 1006,
        Embedded = 1007,
        Cloud = 1008,
        Blockchain = 1009,
        ERP = 1010,
        CRM = 1011,
        Automation = 1012
    }
    #endregion

    #region UserCampus
    public enum Campus
    {
        [Description("FPT University Da Nang")]
        FUDA = 1000,
        [Description("FPT University Ha Noi")]
        HOLA =1001,
        [Description("FPT University Ho Chi Minh")]
        HCM =1002
    }
    #endregion

}
