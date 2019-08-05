using HydroPiApi.Controllers.Common.Processor;

namespace HydroPiApi.Controllers.Sensors.Version1.Processors.Request
{
    public class GetSensorReadingProcessorRequestVersionOne : IProcessorRequest
    {
        public int GpioPin { get; set; }
    }
}