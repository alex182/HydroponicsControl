using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Tasks.Version1.Models
{
    public class UpdateHumidifierTaskRequest
    {
        [Range(0,int.MaxValue)]
        public int? HumiditySensorGpio { get; set; }

        [Range(0, int.MaxValue)]
        public int? CheckInterval { get; set; }

        [Range(0, int.MaxValue)]
        public int? TargetHumidity { get; set; }

        [Range(0, int.MaxValue)]
        public int? RelayGpio { get; set; }

        [Required]
        public string JobName { get; set; }
    }
}
