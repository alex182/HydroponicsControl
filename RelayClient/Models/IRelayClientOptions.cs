using System;
using System.Collections.Generic;
using System.Text;

namespace RelayClient.Models
{
    public interface IRelayClientOptions
    {
        List<Relay> Relays { get; set; }
    }
}
