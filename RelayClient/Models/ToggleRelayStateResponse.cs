using System;
using System.Collections.Generic;
using System.Text;

namespace RelayClient.Models
{
    public class ToggleRelayStateResponse
    {
        public bool IsSuccess{ get; set; }
        public int GpioPin { get; set; }
        public RelayState? State { get; set; }
        public string ErrorMessage { get; set; }
    }
}
