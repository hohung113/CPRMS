using System;
using System.Collections.Generic;
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
    public enum Profession
    {
        InformationTechnology = 1000,
       // Business = 1001,
    }
    public enum Speciality
    {
        SoftwareEngineer = 1000,
        ArtificialIntelligence = 1001,
        GraphicDesign = 1002,
        //Marketing = 2000,
        //InternationalBusiness = 2001,
        //BusinessAdministration = 2002
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

}
