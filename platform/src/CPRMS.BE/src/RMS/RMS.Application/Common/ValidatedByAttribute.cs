using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Application.Common
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidatedByAttribute<TValidator> : Attribute where TValidator : IValidator, new()
    {
        public Type ValidatorType { get; } = typeof(TValidator);
    }
}
