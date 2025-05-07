using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ServiceModel
{
    public class AwsS3Options
    {
        public string BucketName { get; set; }
        public string Region { get; set; }
    }
}
