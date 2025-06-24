using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruSec.DAL.Entities
{
    public class DeviceTelemetry
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }
        public DateTime Timestamp { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public bool FuseStatus { get; set; }
    }

}
