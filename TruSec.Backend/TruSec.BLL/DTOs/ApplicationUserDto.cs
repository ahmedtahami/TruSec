using System.ComponentModel.DataAnnotations;

namespace TruSec.BLL.DTOs
{
    public class ApplicationUserDto
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string RoleName { get; set; }
    }
    public class ApplicationUserRegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
