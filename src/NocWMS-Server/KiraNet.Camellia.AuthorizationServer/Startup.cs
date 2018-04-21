using System;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using KiraNet.Camellia.AuthorizationServer.Configuration;
using KiraNet.Camellia.AuthorizationServer.Data;
using KiraNet.Camellia.AuthorizationServer.Extensions;
using KiraNet.Camellia.AuthorizationServer.Models;
using KiraNet.Camellia.AuthorizationServer.Serivces;
using KiraNet.Camellia.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;
using NLog.Web;

namespace KiraNet.Camellia.AuthorizationServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceConfig = ServiceConfiguration.Configs;
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).Assembly.GetName().Name;
           
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(connectionString, sql=>
                           sql.MigrationsAssembly(migrationsAssembly)));
                           //sql.MigrationsAssembly("KiraNet.Camellia.AuthorizationServer")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Signin settings
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "Camellia-AuthorizationServerCookie";
                //options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
            services.AddIdentityServer(options =>
            {
                //缓存参数处理  缓存起来提高了效率 不用每次从数据库查询
                options.Caching = new IdentityServer4.Configuration.CachingOptions
                {
                    ClientStoreExpiration = new TimeSpan(1, 0, 0),//设置Client客户端存储加载的客户端配置的数据缓存的有效时间 
                    ResourceStoreExpiration = new TimeSpan(1, 0, 0),// 设置从资源存储加载的身份和API资源配置的缓存持续时间
                    CorsExpiration = new TimeSpan(1, 0, 0)  //设置从资源存储的跨域请求数据的缓存时间
                };
            })
                //#if DEBUG
                //              .AddDeveloperSigningCredential()
                //#else
                .AddSigningCredential(new X509Certificate2(
                    Configuration["AuthorizationServer:SigningCredentialCertificatePath"],
                    Configuration["AuthorizationServer:SigningCredentialCertificatePassword"]))
                //#endif
                //.AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                //.AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                //.AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString, sql =>
                            sql.MigrationsAssembly(migrationsAssembly));
                            //sql.MigrationsAssembly("KiraNet.Camellia.AuthorizationServer"));
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                          builder.UseSqlServer(connectionString, sql =>
                              sql.MigrationsAssembly(migrationsAssembly));
                              //sql.MigrationsAssembly("KiraNet.Camellia.AuthorizationServer"));

                    options.EnableTokenCleanup = true;  //允许对Token的清理
                    options.TokenCleanupInterval = 1800;  //清理周期时间Secends
                })
                .AddAspNetIdentity<User>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApiAuthorizationPolicy.PolicyAdminWithName, policy =>
                    policy.RequireClaim(ApiAuthorizationPolicy.ClaimName, ApiAuthorizationPolicy.ClaimValue));
                options.AddPolicy(ApiAuthorizationPolicy.PolicyAdminWithRole, policy =>
                    policy.RequireRole(ApiAuthorizationPolicy.AdminRole));
            });


            //services.AddCors(options =>
            //{
            //    options.AddPolicy(serviceConfig.ApiName, policy =>
            //    {
            //        policy.WithOrigins(serviceConfig.ApiBase)
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowAnyOrigin();
            //    });

            //    options.AddPolicy(serviceConfig.ClientName, policy =>
            //    {
            //        policy.WithOrigins(serviceConfig.ClientBase)
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowAnyOrigin();
            //    });
            //});

            //为NLog.web注入HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeDatabase();
            var serviceConfig = ServiceConfiguration.Configs;

            //添加NLog到.net core框架中
            loggerFactory.AddNLog();
            //指定NLog的配置文件
            env.ConfigureNLog("nlog.config");
            //app.UseCors(serviceConfig.ApiName)
            //   .UseCors(serviceConfig.ClientName);

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole();

                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
