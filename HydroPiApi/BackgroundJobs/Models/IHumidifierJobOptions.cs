namespace HydroPiApi.BackgroundJobs.Models
{
    public interface IHumidifierJobOptions : IJobOptions
    {
        int CheckInterval { get; set; }
        int HumiditySensorGpio { get; set; }
        int RelayGpio { get; set; }
        int TargetHumidity { get; set; }
    }
}