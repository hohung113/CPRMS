using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Context
{
    public class RunTimeContext
    {
        private static readonly AsyncLocal<RunTimeContext> instance;

        static RunTimeContext()
        {
            instance = new AsyncLocal<RunTimeContext>();
        }

        public static RunTimeContext Current
        {
            get => instance.Value ?? (instance.Value = new RunTimeContext());
            set => instance.Value = value;
        }

        public string TenantId { get; set; }
        public Guid UserId { get; set; }
        public Guid UserObjectId { get; set; }
        public string UserPrincipalName { get; set; }
        public string DisplayName { get; set; }
        public string CorrelationId { get; set; } = String.Empty;
        public string UserEmail { get; set; }

        public static void Empty()
        {
            if (instance != null)
                instance.Value = new RunTimeContext();
        }
    }
}
