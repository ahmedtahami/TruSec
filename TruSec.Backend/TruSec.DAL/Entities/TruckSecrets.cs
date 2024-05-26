using System.ComponentModel.DataAnnotations.Schema;

namespace TruSec.DAL.Entities
{
    public class TruckSecrets
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string MacAddress { get; set; }
        public int TruckId { get; set; }
        [ForeignKey(nameof(TruckId))]
        public Truck Truck { get; set; }
    }
}
