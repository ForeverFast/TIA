using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TIA.EntityFramework
{
    public class TIADbContextFactory : IDesignTimeDbContextFactory<TIADbContext>
    {
        public TIADbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TIADbContext> options = new DbContextOptionsBuilder<TIADbContext>();
            
            options.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TIA_DB;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new TIADbContext(options.Options);
        }

        public TIADbContext CreateDbContext()
        {
            throw new System.NotImplementedException();
        }
    }
}
