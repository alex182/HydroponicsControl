using HydroPiApi.Controllers.Common.Processor;

namespace HydroPiApi.Controllers.Tasks.Version1.Processors.Request
{
    public class UpdateHumidifierTaskProcessorRequestVersionOne : IProcessorRequest
    {
        public string JobName { get; set; }
        public int? HumiditySensorGpio { get; set; }
        public int? CheckInterval { get; set; }
        public int? TargetHumidity { get; set; }
        public int? RelayGpio { get; set; }
    }
}
