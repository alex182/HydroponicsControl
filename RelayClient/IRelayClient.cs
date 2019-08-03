using RelayClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelayClient
{
    public interface IRelayClient
    {
        List<Relay> GetRelays();
        RelayState? GetRelayState(int relay);
        ToggleRelayStateResponse ToggleRelayState(ToggleRelayStateRequest request);
    }
}
