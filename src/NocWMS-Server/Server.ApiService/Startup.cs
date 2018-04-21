using System.IO;
using AutoMapper;
using KiraNet.Camellia.Shared;
using Service.ApiService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Server.ApiService.Common;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Repositories.Abstracts;
using Server.ApiService.Repositories;
using Server.ApiService.Services;

namespace Service.ApiService
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
            var migrationsAssembly = typeof(Startup).Assembly.GetName().Name;
            var serviceConfig = ServiceConfiguration.Configs;

            services.AddDbContext<NocDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());

                // set authorize on all controllers or routes
                //var policy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .Build();
                //options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());

            services.AddSwaggerGen(c =>
            {
                // 最好在launchSettings.json中设置"launchUrl": "swagger", 可以直接用swagger文档所在的url打开项目, 而不需要手动跳转
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "KiraNet.CamelliaAPI",
                        Version = "v1",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Kira Yoshikage", Email = "997525106@qq.com" }
                    });

                //Set the comments path for the swagger json and ui.
                // 需要在项目属性中，设置XML文档文件，并把1591添加到禁止显示警告(Suppress warnings)中
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "KiraNet.Camellia.ApiService.xml");
                c.IncludeXmlComments(xmlPath);

                //  c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数

                // 添加IdentityServer4授权
                //c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "implicit",
                //    AuthorizationUrl = serviceConfig.AuthBase,
                //    Scopes = new Dictionary<string, string>
                //    {
                //        { serviceConfig.ApiName, serviceConfig.ApiName },
                //        { serviceConfig.ClientName, serviceConfig.ClientName }
                //    }
                //});
            });

            services.AddSession(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = serviceConfig.AuthBase;
                    options.RequireHttpsMetadata = false;

                    options.ApiName = serviceConfig.ClientName;
                });

            // Action 使用对应的EnableCors，不启用使用[DisableCors] 或 直接使用app.UseCors("") 对所有的Action生效
            services.AddCors(options =>
            {
                //options.AddPolicy(serviceConfig.AuthName, policy =>
                //{
                //    policy.WithOrigins(serviceConfig.AuthBase)
                //        .AllowAnyHeader()
                //        .AllowAnyMethod();
                //});

                options.AddPolicy(serviceConfig.ClientName, policy =>
                {
                    policy.WithOrigins(serviceConfig.ClientBase)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<IUserInfoRepository, UserInfoRepository>()
                    .AddScoped<IStaffRepository, StaffRepository>()
                    .AddScoped<IVendorRepository, VendorRepository>()
                    .AddScoped<ICustomerRepository, CustomerRepository>()
                    .AddScoped<IInventoryRepository, InventoryRepository>()
                    .AddScoped<IInventoryCellRepository, InventoryCellRepository>()
                    .AddScoped<IInboundReceiptRepository, InboundReceiptRepository>()
                    .AddScoped<IInboundReceiptItemRepository, InboundReceiptItemRepository>()
                    .AddScoped<IOutboundReceiptRepository, OutboundReceiptRepository>()
                    .AddScoped<IOutboundReceiptItemRepository, OutboundReceiptItemRepository>()
                    .AddScoped<ICategoryRepository, CategoryRepository>()
                    .AddScoped<IProductRepository, ProductRepository>()
                    .AddScoped<IWarehouseRepository, WarehouseRepository>()
                    .AddScoped<IWarehouseCellRepository, WarehouseCellRepository>()
                    .AddScoped<IInventoryCellRepository, InventoryCellRepository>()
                    .AddScoped(typeof(IInjectService<,>), typeof(InjectService<,>));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandlingMiddleware();
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            app.UseCors(ServiceConfiguration.Configs.ClientName);
            app.UseStaticFiles();
            app.UseSession();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ServiceConfiguration.Configs.ApiName + " API v1");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
