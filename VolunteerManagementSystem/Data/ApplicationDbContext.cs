using VolunteerManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace VolunteerManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        } 

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }

    }
}
