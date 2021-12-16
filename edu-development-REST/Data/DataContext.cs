using edu_development_REST.Entities;
using Microsoft.EntityFrameworkCore;

namespace edu_development_REST.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseMembership> CourseMemberships { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {

        }
    }
}
