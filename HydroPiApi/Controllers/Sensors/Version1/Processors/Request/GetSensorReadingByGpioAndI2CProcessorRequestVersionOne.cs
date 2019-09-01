using HydroPiApi.Controllers.Common.Processor;

namespace HydroPiApi.Controllers.Sensors.Version1.Processors.Request
{
    public class GetSensorReadingByGpioAndI2CProcessorRequestVersionOne : IProcessorRequest
    {
        public int GpioPin { get; set; }
        public int I2CPin { get; set; }
    }
}