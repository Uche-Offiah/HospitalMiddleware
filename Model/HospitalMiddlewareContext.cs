using Microsoft.EntityFrameworkCore;

namespace HospitalMiddleware.Model
{
    public partial class HospitalMiddlewareContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public HospitalMiddlewareContext(IConfiguration config)
        {
            _configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("HospitalMiddleWareInstance"));
        }
        public virtual DbSet<Patient> Patients { get; set; }
    }
}
