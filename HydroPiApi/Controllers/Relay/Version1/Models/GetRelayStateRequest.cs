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
        /// The physical pin on the board
        /// </summary>
        [Range(0,int.MaxValue)]
        public int? Pin { get; set; }

        /// <summary>
        /// The gpio pin assigned to the relay
        /// </summary>
        [Range(0,int.MaxValue)]
        public int? GpioPin { get; set; }

        /// <summary>
        /// The assigned name of the relay
        /// </summary>
        public string RelayName { get; set; }
    }
}
