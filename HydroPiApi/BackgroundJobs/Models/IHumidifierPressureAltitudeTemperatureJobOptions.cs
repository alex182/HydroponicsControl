namespace HydroPiApi.BackgroundJobs.Models
{
    public interface IHumidifierPressureAltitudeTemperatureJobOptions : IJobOptions
    {
        int CheckInterval { get; set; }
        int HumiditySensorGpio { get; set; }
        int RelayGpio { get; set; }
        int SclkPin { get; set; }
        double TargetAltitude { get; set; }
        int TargetHumidity { get; set; }
        double TargetPressure { get; set; }
        double TargetTemperature { get; set; }
    }
}