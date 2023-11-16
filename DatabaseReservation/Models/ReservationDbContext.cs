using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;
using DatabaseReservation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Models;

public partial class ReservationDbContext : IdentityDbContext<ApplicationUser>
{
    public ReservationDbContext()
    {
    }

    public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllTable> AllTables { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservedTable> ReservedTables { get; set; }

    public virtual DbSet<Sitting> Sittings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=THEBLUEACE\\SQLEXPRESS;Database=ReservationDB;Trusted_Connection=true;TRustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Seeding roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "manager",
                NormalizedName = "MANAGER",
            },
            new IdentityRole
            {
                Id = "2",
                Name = "staff",
                NormalizedName = "STAFF"
            },
            new IdentityRole
            {
                Id = "3",
                Name = "member",
                NormalizedName = "MEMBER",
            });

        // seeding sittings for 3months
        SeedSittings(builder);
        
        // Tables seeding
        builder.Entity<Area>().HasData(
          new Area
          {
              AreaId = 1,
              AreaType = "Main"
          },
          new Area
          {
              AreaId = 2,
              AreaType = "Outside"
          },
          new Area
          {
              AreaId = 3,
              AreaType = "Balcony"
          }
          );

        // seeding tables, 10 in each area
        SeedAllTables(builder);
        SeedingUsers(builder);

    }
    public static void SeedingUsers(ModelBuilder builder)
    {

        // seeding users, 1 manager, 1 staff, 1 memeber
        var hasher = new PasswordHasher<ApplicationUser>();

        var manager = new ApplicationUser()
        {
            Email = "manager@beanscene.com",
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = "Manager",
            NormalizedUserName = "MANAGER",
            FirstName = "John",
            LastName = "Smith",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        manager.PasswordHash = hasher.HashPassword(manager, "manager");

        var staff = new ApplicationUser()
        {
            Email = "staff@beanscene.com",
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = "Staff",
            NormalizedUserName = "STAFF",
            FirstName = "Adam",
            LastName = "Smith",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        staff.PasswordHash = hasher.HashPassword(staff, "staff");

        var member = new ApplicationUser()
        {
            Email = "member@beanscene.com",
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = "Member",
            NormalizedUserName = "MEMBER",
            FirstName = "Jason",
            LastName = "Smith",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        member.PasswordHash = hasher.HashPassword(member, "member");
        
        builder.Entity<ApplicationUser>().HasData(
     manager, staff, member
      );

        builder.Entity<IdentityUserRole<string>>().HasData(
             new IdentityUserRole<string>
             {
                 RoleId = "1",
                 UserId = manager.Id
             },
             new IdentityUserRole<string>
             {
                 RoleId = "2",
                 UserId = staff.Id
             },
              new IdentityUserRole<string>
              {
                  RoleId = "3",
                  UserId = member.Id
              }
         );

      
    }
    // Seed method for the Sitting table
    public static void SeedSittings(ModelBuilder modelBuilder)
    {
        DateTime startDate = DateTime.Now.Date;
        startDate = startDate.AddHours(7);
        DateTime endDate = startDate.AddMonths(3);
        DateTime currentDate = startDate;
        int i = 1;
        // Insert data for breakfast, lunch, and dinner with a capacity of 40
        while (currentDate <= endDate)
        {
            modelBuilder.Entity<Sitting>().HasData(
                new Sitting
                {
                    SittingId = i,
                    SittingType = "breakfast",
                    StartDateTime = currentDate,
                    EndDateTime = currentDate.AddMinutes(299),
                    Capacity = 40
                },
                new Sitting
                {
                    SittingId = i + 1,
                    SittingType = "lunch",
                    StartDateTime = currentDate.AddHours(5),
                    EndDateTime = currentDate.AddMinutes(599),
                    Capacity = 40
                },
                new Sitting
                {
                    SittingId = i + 2,
                    SittingType = "dinner",
                    StartDateTime = currentDate.AddHours(10),
                    EndDateTime = currentDate.AddMinutes(899),
                    Capacity = 40
                }
            );
            i+=3;

            // Move to the next day
            currentDate = currentDate.AddDays(1);
        }
    }
   
    // Seed method for the AllTable table
    public static void SeedAllTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllTable>().HasData(
          new AllTable { TableId = 1, TableName = "M1", AreaId = 1 },
          new AllTable { TableId = 2, TableName = "M2", AreaId = 1 },
          new AllTable { TableId = 3, TableName = "M3", AreaId = 1 },
          new AllTable { TableId = 4, TableName = "M4", AreaId = 1 },
          new AllTable { TableId = 5, TableName = "M5", AreaId = 1 },
          new AllTable { TableId = 6, TableName = "M6", AreaId = 1 },
          new AllTable { TableId = 7, TableName = "M7", AreaId = 1 },
          new AllTable { TableId = 8, TableName = "M8", AreaId = 1 },
          new AllTable { TableId = 9, TableName = "M9", AreaId = 1 },
          new AllTable { TableId = 10, TableName = "M10", AreaId = 1 },

          new AllTable { TableId = 11, TableName = "O1", AreaId = 2 },
          new AllTable { TableId = 12, TableName = "O2", AreaId = 2 },
          new AllTable { TableId = 13, TableName = "O3", AreaId = 2 },
          new AllTable { TableId = 14, TableName = "O4", AreaId = 2 },
          new AllTable { TableId = 15, TableName = "O5", AreaId = 2 },
          new AllTable { TableId = 16, TableName = "O6", AreaId = 2 },
          new AllTable { TableId = 17, TableName = "O7", AreaId = 2 },
          new AllTable { TableId = 18, TableName = "O8", AreaId = 2 },
          new AllTable { TableId = 19, TableName = "O9", AreaId = 2 },
          new AllTable { TableId = 20, TableName = "O10", AreaId = 2 },

          new AllTable { TableId = 21, TableName = "B1", AreaId = 2 },
          new AllTable { TableId = 22, TableName = "B2", AreaId = 2 },
          new AllTable { TableId = 23, TableName = "B3", AreaId = 2 },
          new AllTable { TableId = 24, TableName = "B4", AreaId = 2 },
          new AllTable { TableId = 25, TableName = "B5", AreaId = 2 },
          new AllTable { TableId = 26, TableName = "B6", AreaId = 2 },
          new AllTable { TableId = 27, TableName = "B7", AreaId = 2 },
          new AllTable { TableId = 28, TableName = "B8", AreaId = 2 },
          new AllTable { TableId = 29, TableName = "B9", AreaId = 2 },
          new AllTable { TableId = 30, TableName = "B10", AreaId = 2 }
      );
    }
}
