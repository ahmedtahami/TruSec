using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruSec.DAL.Entities;

namespace TruSec.BLL.DTOs
{
    public class CompanyDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public List<TruckDto>? Trucks { get; set; }
    }
}
