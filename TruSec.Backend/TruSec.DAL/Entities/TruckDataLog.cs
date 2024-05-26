using System.ComponentModel.DataAnnotations.Schema;

namespace TruSec.DAL.Entities
{
    public class TruckDataLog
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DriverExpression { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int TruckId { get; set; }
        [ForeignKey(nameof(TruckId))]
        public Truck Truck { get; set; }
        public double SpeedKPH { get; set; }
    }
}
