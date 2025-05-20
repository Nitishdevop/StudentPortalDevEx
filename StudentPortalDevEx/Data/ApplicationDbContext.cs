using Microsoft.EntityFrameworkCore;
using StudentPortalDevEx.Models;
using StudentPortalDevEx.Models.Entities;

namespace StudentPortalDevEx.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }

    }
}
