namespace Sandbox.MSSQL
{
    using Microsoft.EntityFrameworkCore;

    public sealed class AppDbContext : DbContext
    {
        private const string ConnectionString = @"Server=DESKTOP-KL3VEUU;Database=Testing;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
