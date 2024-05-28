namespace TruSec.BLL.DTOs
{
    public class UserCompanyDto
    {
        public int? Id { get; set; }
        public int? CompanyId { get; set; }
        public CompanyDto? Company { get; set; }
        public string? UserId { get; set; }
        public ApplicationUserDto User { get; set; }
    }
}
