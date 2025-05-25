using Newtonsoft.Json;

namespace Rms.Domain.Constants
{
    public static class CprmsConstants
    {
        public const string CprmsAdminDisplayName = "CPRMS ADMIN";
        public const string CprmsAdmin = "Admin";
    }
    public static class CprmsRoles
    {
        public const string Admin = "Admin";
        public const string Lecture = "Lecture";
        public const string EvaluationCommittee = "Evaluation Committee";
        public const string HeadOfDepartment = "Head of Department";
        public const string AcademicAffairsOffice = "Academic Affairs Office";
        public const string Student = "Student";
    }

    public static class CprmsCampus
    {
        public const string FUDA = "FPTUDN";
        public const string HN = "FPTUHN";
        public const string HCM = "FPTUHCM";
        public const string QN = "FPTUQN";
        public const string CT = "FPTUCT";
    }
}