using Microsoft.EntityFrameworkCore;
using TraceCV.Models;

namespace TraceCV.Data
{
    public class DatabaseHandler : DbContext
    {
        public DatabaseHandler(DbContextOptions<DatabaseHandler> options) : base(options) { }

        public DbSet<Expert> Experts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<OtherKeyExpertise> OtherKeyExpertises { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Affiliation> Affiliations { get; set; }
        public DbSet<WorkedCountry> WorkedCountries { get; set; } // New

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expert>()
                .HasMany(e => e.Educations)
                .WithOne()
                .HasForeignKey(ed => ed.ExpertId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Expert>()
                .HasMany(e => e.AfricanCountriesWorked)
                .WithOne()
                .HasForeignKey(wc => wc.ExpertId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
