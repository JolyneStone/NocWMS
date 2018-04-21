using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Server.ApiService.Common;

namespace Server.ApiService.DesignTimeDbContexts
{
    public class NocDesignTimeDbContextFactory : IDesignTimeDbContextFactory<NocDbContext>
    {
        public NocDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NocDbContext>();
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Initial Catalog=NocWMS;Data Source=.\\sqlexpress");

            return new NocDbContext(optionsBuilder.Options);
        }
    }
}
