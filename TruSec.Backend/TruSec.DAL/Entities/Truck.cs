using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruSec.DAL.Entities
{
    public class Truck
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public string? ImageSrc { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }
        public ICollection<TruckSecret> Secrets { get; set; }
        public ICollection<TruckDataLog> DataLogs { get; set; }
    }
}
