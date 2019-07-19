namespace RelayClient
{
    public class ToggleRelayStateRequest
    {
        public int Relay { get; set; }
        public RelayState State { get; set; }
    }
}