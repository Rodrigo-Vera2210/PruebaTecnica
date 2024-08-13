using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model
{
    public class PruebaTecnicaDbContext : DbContext
    {
        public PruebaTecnicaDbContext(DbContextOptions<PruebaTecnicaDbContext> options) : base(options) { }

        public DbSet<DatoSensor> DatosSensores { get; set; }
        public DbSet<ParametroSensor> ParametrosSensores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParametroSensor>()
                .HasMany(c => c.DatosSensores)
                .WithOne(a => a.ParametroSensor)
                .HasForeignKey(a => a.ParametroSensoresId);
        }
    }
}
