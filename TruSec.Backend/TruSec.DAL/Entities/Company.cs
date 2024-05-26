namespace TruSec.DAL.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public ICollection<Truck> Trucks { get; set; }
    }
}
