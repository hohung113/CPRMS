using Core.Domain.Models.Enums;

namespace Core.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string ClassName { get; set; }
        public Profession Profession { get; set; }
        public Speciality Speciality { get; set; }
        public ProjectType ProjectType { get; set; }
        public KindOfPersonMakeRegister KindOfPersonMakeRegister { get; set; }
        public StatusProject StatusProject { get; set; }
        public string Context { get; set; }
        public string ResearchContent { get; set; }
        public string ExpectedFeature { get; set; }
        public string CapstonePrtojectNameEN { get; set; }
        public string CapstonePrtojectNameVN { get; set; }
        public string Abbreviation { get; set; }
        public Guid GroupId { get; set; }
        public Guid SemesterId { get; set; }
        public bool IsChoosen { get; set; }
        public ICollection<SupervisorOfProject> SupervisorOfProjects { get; set; }
    }
}
