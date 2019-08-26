using CopaAirlines.AgenciesService.DA.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace CopaAirlines.AgenciesService.DA
{
    public class DbContextAgenciesServices : DbContext
    {
        public virtual DbSet<Agencies> Agencies { get; set; }
        public virtual DbSet<AgencieUsers> AgencyUsers { get; set; }

        public DbContextAgenciesServices()
        { }
        public DbContextAgenciesServices(DbContextOptions<DbContextAgenciesServices> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            IConfigurationSection configurationSection = configuration.GetSection("ConnectionStrings")
                                                                        .GetSection("DefaultConnection");

            optionsBuilder.UseSqlServer(configurationSection.Value);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
