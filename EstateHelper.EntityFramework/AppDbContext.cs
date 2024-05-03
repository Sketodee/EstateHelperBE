using EstateHelper.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.EntityFramework
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .Property(u => u.RefererGeneration) // Assuming OrganizationIDs is a List<int> property in AppUser
                .HasConversion(
                    v => string.Join(",", v),                   // Conversion from List<int> to string
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(int.Parse)
                          .ToList());                         // Conversion from string to List<int>


            //   modelBuilder.Entity<AppUser>()
            //.Property(e => e.Link)
            //.ValueGeneratedOnAdd().HasDefaultValueSql("600");

            modelBuilder.HasSequence<int>("LinkSequence", schema: "dbo")
        .StartsAt(600)
        .IncrementsBy(1);

            modelBuilder.Entity<AppUser>()
        .Property(e => e.Link)
        .HasDefaultValueSql("NEXT VALUE FOR dbo.LinkSequence");

            //modelBuilder.Entity<AppUser>()
            //    .Property(e => e.Link)
            //    .UseHiLo("LinkSequence", schema: "dbo");

        }
    }
}
