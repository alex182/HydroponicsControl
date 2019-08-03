
using RelayClient;
using System.ComponentModel.DataAnnotations;

namespace HydroPiApi.Controllers.Relay.Version1.Models
{
    public class ToggleRelayStateRequest
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int GpioPin { get; set; }

        [Required]
        public RelayState RelayState { get; set; }
    }
}