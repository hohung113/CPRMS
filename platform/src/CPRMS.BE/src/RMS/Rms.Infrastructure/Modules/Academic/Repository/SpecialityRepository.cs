using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Infrastructure.Modules.Academic.Repository
{
    public class SpecialityRepository : BaseRepository<RmsDbContext, Speciality>, ISpecialityRepository
    {
        public SpecialityRepository(RmsDbContext context) : base(context)
        {
        }
    }
}
