namespace TruSec.BLL.DTOs
{
    public class TruckDto
    {
        public int? Id { get; set; }
        public string? ModelName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ImageSrc { get; set; }
        public int? CompanyId { get; set; }
        public CompanyDto? Company { get; set; }
        public List<TruckSecretDto>? Secrets { get; set; }
        public List<TruckDataLogDto>? DataLogs { get; set; }
    }
}
