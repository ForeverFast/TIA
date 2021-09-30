using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TIA.DataAccessLayer.Models
{
    public class TIA_DBContextFactory : IDesignTimeDbContextFactory<TIA_DBContext>
    {
        public TIA_DBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TIA_DBContext> options = new DbContextOptionsBuilder<TIA_DBContext>();

            options.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TIA_DB;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new TIA_DBContext(options.Options);
        }
    }
}
