namespace RelayClient
{
    public class ToggleRelayStateRequest
    {
        public int GpioPin { get; set; }
        public RelayState State { get; set; }
    }
}