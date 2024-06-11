using System.ComponentModel.DataAnnotations;

namespace TruSec.BLL.DTOs
{
    public class TruckDataLogDto
    {
        public int? Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? DriverExpression { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? TruckId { get; set; }
        public double? SpeedKPH { get; set; }
    }
    public class TruckDataLogRequestDto
    {
        public DateTime TimeStamp { get; set; }
        public string DriverExpression { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Required]
        public int TruckId { get; set; }
        public double SpeedKPH { get; set; }
    }
}
