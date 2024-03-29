﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class EDBContext : DbContext
    {
        public EDBContext()
        {
        }

        public EDBContext(DbContextOptions<EDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=../Data/DB/EDB.db");
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<DeviceHealth> DeviceHealths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Device>();

            modelBuilder.Entity<DeviceType>();

            modelBuilder.Entity<DeviceHealth>();


            base.OnModelCreating(modelBuilder);
        }
    }
}
