using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Tasks.Version1.Models
{
    public class UpdateFanTaskRequest
    {
        [Range(0,int.MaxValue)]
        public int? JobInterval { get; set; }

        [Range(0, int.MaxValue)]
        public int? RunDuration { get; set; }

        [Range(0, int.MaxValue)]
        public int? RelayGpio { get; set; }

        [Required]
        public string JobName { get; set; }

    }
}
