using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ServiceModel
{
    public class RefreshTokenRequest
    {
        [Required]
        public string ExpiredAccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
