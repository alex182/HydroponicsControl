
using RelayClient;
using System.ComponentModel.DataAnnotations;

namespace HydroPiApi.Controllers.Relay.Version1.Models
{
    public class ToggleRelayStateRequest
    {
        /// <summary>
        /// The gpio pin that will its state toggled. 
        /// </summary>
        [Required]
        [Range(1,int.MaxValue)]
        public int GpioPin { get; set; }

        /// <summary>
        /// The state that the gpio will be toggled to. 
        /// </summary>
        [Required]
        public RelayState RelayState { get; set; }
    }
}