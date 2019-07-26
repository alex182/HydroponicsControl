using System;
using System.Collections.Generic;
using System.Text;

namespace RelayClient.Models
{
    public class Relay
    {
        public int Pin { get; set; }
        public int GpioPin { get; set; }
        public string RelayName { get; set; }
    }
}
