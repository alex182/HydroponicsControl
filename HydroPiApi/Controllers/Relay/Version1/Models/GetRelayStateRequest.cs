using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Relay.Version1.Models
{
    public class GetRelayStateRequest
    {
        /// <summary>
        /// The gpio pin assigned to the relay
        /// </summary>
        [Required]
        [Range(0,int.MaxValue)]
        public int GpioPin { get; set; }
    }
}
