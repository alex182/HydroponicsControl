using System;
using System.Collections.Generic;
using System.Text;

namespace RelayClient.Models
{
    public class ToggleRelayStateResponse
    {
        public bool IsSuccess{ get; set; }
        public int Relay { get; set; }
        public RelayState? State { get; set; }
        public string ErrorMessage { get; set; }
    }
}
