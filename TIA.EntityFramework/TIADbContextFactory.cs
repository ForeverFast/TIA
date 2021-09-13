using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
