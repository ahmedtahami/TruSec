using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruSec.DAL.DbContexts;

namespace TruSec.DAL.Utils
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DataSeeder(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        
        public void SeedRolesToDb_ROLLBACK()
        {
            var roles = _context.Roles.ToList();
            _context.RemoveRange(roles);
            _context.SaveChanges();
        }
    }
}
