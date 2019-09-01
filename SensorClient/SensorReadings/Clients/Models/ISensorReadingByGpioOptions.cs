namespace SensorClient.SensorReadings.Clients.Models
{
    public interface ISensorReadingByGpioOptions : IClientOptions
    {
        int GpioPin { get; set; }
    }
}