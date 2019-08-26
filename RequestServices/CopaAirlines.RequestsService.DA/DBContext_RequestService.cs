using CopaAirlines.RequestsService.DA.DbModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopaAirlines.RequestsService.DA
{
    public class DBContext_RequestService : DbContext
    {
        public virtual DbSet<NameCorrectionRequest> NameCorrectionRequests { get; set; }


        public DBContext_RequestService()
        { }
        public DBContext_RequestService(DbContextOptions<DBContext_RequestService> options) : base(options)
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
