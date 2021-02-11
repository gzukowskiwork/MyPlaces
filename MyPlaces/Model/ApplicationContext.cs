﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPlaces.Configuration;
using System;
using System.Threading.Tasks;

namespace MyPlaces.Model
{
    public class ApplicationContext: IdentityDbContext
    {
        public DbSet<Place> Places { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PlaceConfiguration());
        }
    }
}