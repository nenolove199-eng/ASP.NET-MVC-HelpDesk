using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HelpDeskAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
    }
}

