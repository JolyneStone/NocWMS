using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using KiraNet.Camellia.AuthorizationServer.Configuration;
using KiraNet.Camellia.AuthorizationServer.Data;
using KiraNet.Camellia.AuthorizationServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.Camellia.AuthorizationServer.Extensions
{
    public static class DatabaseExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var authDbContext = serviceScope.ServiceProvider.GetRequiredService<AuthDbContext>();
                authDbContext.Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManagr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                Task.Run(async () =>
                {
                    var admin = await userManager.FindByNameAsync("admin");
                    if (admin == null)
                    {
                        await userManager.CreateAsync(new User
                        {
                            UserName = "admin",
                            Email = "997525106@qq.com",
                            EmailConfirmed = true,
                            AccessFailedCount = 0,
                            LockoutEnabled = false
                        }, "zi123123");
                    }

                    var adminRole = await roleManagr.FindByNameAsync("admin");
                    if (adminRole == null)
                    {
                        adminRole = new IdentityRole()
                        {
                            Name = "admin"
                        };

                        await roleManagr.CreateAsync(adminRole);
                    }

                    if (!await userManager.IsInRoleAsync(admin, "admin"))
                    {
                        await userManager.AddToRoleAsync(admin, "admin");
                    }

                }).GetAwaiter().GetResult();

                var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                configurationDbContext.Database.Migrate();
                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in IdentityServerConfig.GetClients().Where(client => !configurationDbContext.Clients.Any(c => c.ClientId == client.ClientId)))
                    {
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }
                }

                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (
                        var identity in
                            IdentityServerConfig.GetIdentityResources()
                                .Where(identity => !configurationDbContext.IdentityResources.Any(i => i.Name == identity.Name)))
                    {
                        configurationDbContext.IdentityResources.Add(identity.ToEntity());
                    }
                }

                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var api in IdentityServerConfig.GetApiResources().Where(api => !configurationDbContext.ApiResources.Any(a => a.Name == api.Name)))
                    {
                        configurationDbContext.ApiResources.Add(api.ToEntity());
                    }
                }

                configurationDbContext.SaveChanges();
            }
        }
    }
}
