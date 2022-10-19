using Juguetes.Domain.Models;
using Juguetes.Persistence.DB_Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Juguetes.Persistence.DB_Context
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext() { }

        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
				/*Se debe  agregar la cadena de conexiòn de app.settings en DefaultConnection apuntando a localhost*/
				optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ProductosDB; Integrated Security=true");
            }
        }

        #region DBSets
        public virtual DbSet<Productos> Productos { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductosConfiguration());
        }
    }
}
