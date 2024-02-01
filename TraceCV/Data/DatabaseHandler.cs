using Microsoft.EntityFrameworkCore;
using TraceCV.Models;

namespace TraceCV.Data
{
    public class DatabaseHandler : DbContext
    {

        public DatabaseHandler(DbContextOptions<DatabaseHandler> options) : base(options)
        { }

        public DbSet<Expert> Experts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Language> Languages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add any additional configurations if needed
        }

    }
}
