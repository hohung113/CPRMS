﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ITenantConnectionProvider
    {
        string GetConnectionString(string dbName);
    }
}
