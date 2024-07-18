﻿using AuthMicroservice.DAL.Entities;
using AuthMicroservice.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthMicroservice.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(u => u.Login).IsUnique();

            builder.Entity<UserRoles>().HasKey(ur => new { ur.UserId, ur.Role });
            var enumRoleNameConverter = new EnumToStringConverter<Role>();
            builder.Entity<UserRoles>().Property(ud => ud.Role).HasConversion(enumRoleNameConverter);
        }
    }
}

