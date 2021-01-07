using Microsoft.EntityFrameworkCore;
using MyPlaces.Configuration;
using System;
using System.Threading.Tasks;

namespace MyPlaces.Model
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Place> Places { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlaceConfiguration());
        }

        internal Task ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}