using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Interfaces;
using Rms.Domain.Entities;
namespace Rms.Domain.Modules.Academic.Interface
{
    public interface ISemesterRepository : IBaseRepository<Semester>
    {
    }
}
