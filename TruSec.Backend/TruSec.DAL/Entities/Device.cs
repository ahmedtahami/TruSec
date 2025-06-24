namespace TruSec.DAL.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public ICollection<DeviceTelemetry> Telemetries { get; set; }
    }

}
