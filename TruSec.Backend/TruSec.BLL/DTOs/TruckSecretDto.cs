using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruSec.BLL.DTOs
{
    public class TruckSecretDto
    {
        public int? Id { get; set; }
        public string? DeviceName { get; set; }
        public string? MacAddress { get; set; }
        public int? TruckId { get; set; }
        public TruckDto? Truck { get; set; }
    }
}
