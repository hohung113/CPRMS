﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Application.Modules.UserManagement.Dto
{
    public class GoogleLoginResponseDto
    {
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public List<string> RoleNames { get; set; }
        public string AccessToken { get; set; }

    }
}
