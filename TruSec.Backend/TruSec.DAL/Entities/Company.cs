namespace TruSec.DAL.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Truck> Trucks { get; set; }
    }
}
