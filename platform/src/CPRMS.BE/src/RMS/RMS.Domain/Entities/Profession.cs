namespace Rms.Domain.Entities
{
    public class Profession : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Speciality> Specialities { get; set; }
    }
}
