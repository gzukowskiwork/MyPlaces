using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entities.Configuration;
using System;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class ApplicationContext: IdentityDbContext
    {
        public DbSet<Place> Places { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PlaceConfiguration());
            
            ConfigureRealtions(modelBuilder);
        }

        private static void ConfigureRealtions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                            .HasMany(place => place.Places)
                            .WithOne(a => a.ApplicationUser)
                            .HasForeignKey(place => place.UserId);
        }
    }
}