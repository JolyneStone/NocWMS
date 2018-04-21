using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KiraNet.Camellia.AuthorizationServer.Data
{
    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Initial Catalog=KiraNet.Camellia.AuthorizationServer;Server=.\\sqlexpress;MultipleActiveResultSets=true", 
                sql=> sql.MigrationsAssembly("KiraNet.Camellia.AuthorizationServer"));

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
