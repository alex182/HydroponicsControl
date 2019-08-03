using RelayClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Relay.Version1.Models
{
    public class ScheduledToggleRequest
    {
        /// <summary>
        /// The gpio pin that this job is for. 
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int GpioPin { get; set; }

        /// <summary>
        /// When this job starts.
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// When this job stops running.  If left null it will run indefinitely. 
        /// </summary>
        public DateTime? Stop { get; set; }

        /// <summary>
        /// How long pin will be toggled on.
        /// </summary>
        [Required]
        public TimeSpan IntervalOn { get; set; }


        /// <summary>
        /// How long pin will be toggled off.
        /// </summary>
        [Required]
        public TimeSpan IntervalOff { get; set; }
    }
}
