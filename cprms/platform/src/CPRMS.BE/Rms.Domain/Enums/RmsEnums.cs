using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Domain.Enums
{
    public class RmsEnums
    {
    }
    #region UserCampus
    public enum Campus
    {
        [Description("FPT University Da Nang")]
        FUDA = 1000,
        [Description("FPT University Ha Noi")]
        HOLA = 1001,
        [Description("FPT University Ho Chi Minh")]
        HCM = 1002
    }
    #endregion
}
