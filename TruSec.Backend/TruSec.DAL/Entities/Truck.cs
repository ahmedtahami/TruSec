using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruSec.DAL.Entities
{
    public class Truck
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public string ImageSrc { get; set; }
        public ICollection<TruckSecrets> Secrets { get; set; }
    }
}
