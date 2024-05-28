namespace TruSec.BLL.DTOs
{
    public class ApplicationUserDto
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
