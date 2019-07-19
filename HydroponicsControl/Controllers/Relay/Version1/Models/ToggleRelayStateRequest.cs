
using System.ComponentModel.DataAnnotations;

namespace HydroponicsControl.Controllers.Relay.Version1.Models
{
    public class ToggleRelayStateRequest
    {
        [Range(1,int.MaxValue)]
        [Required]
        public int Relay { get; set; }

        [Required]
        public RelayState MyProperty { get; set; }
    }
}