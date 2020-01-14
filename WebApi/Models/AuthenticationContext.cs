using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class AuthenticationContext: IdentityDbContext
    {

        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        // public DbSet<Activity> Activities { get; set; }
       // public DbSet<UserActivity> UserActivities { get; set; }

        public DbSet<School> Schools { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Outreachschedule> Outreachschedules { get; set; }
        public DbSet<Dayofweek> Dayofweeks { get; set; }

        public DbSet<Attendance> Attendances { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // primary key builder
            builder
                .Entity<Outreachschedule>(x =>
                x.HasKey(ors =>
               new { ors.ApplicationUserId, ors.SchoolId, ors.DayofweekId }));

            // foreign key builder for user
            builder.Entity<Outreachschedule>()
                .HasOne(u => u.ApplicationUser)
                .WithMany(ors => ors.Outreachschedules)
                .HasForeignKey(u => u.ApplicationUserId);
            // foreign key builder for school
            builder.Entity<Outreachschedule>()
                .HasOne(s => s.School)
                .WithMany(ors => ors.Outreachschedules)
                .HasForeignKey(s => s.SchoolId);
        }
        
    }
}
