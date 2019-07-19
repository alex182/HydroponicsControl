using System;
using System.Collections.Generic;
using System.Text;

namespace RelayClient.Models
{
    public class RelayClientOptions : IRelayClientOptions
    {
        public List<Relay> Relays { get; set; } = new List<Relay>(); 
    }
}
