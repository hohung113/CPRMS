namespace Rms.Application.Modules.Specialities.Dtos
{
    public class SpecialityDto : CommonDto
    {
        public string Name { get; set; }
        public Guid ProfessionId { get; set; }
    }
}
