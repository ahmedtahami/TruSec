using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruSec.BLL.DTOs
{
    public class DeviceTelemetryDto
    {
        public string DeviceSerial { get; set; }
        public DateTime Timestamp { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public bool FuseStatus { get; set; }
    }

}
