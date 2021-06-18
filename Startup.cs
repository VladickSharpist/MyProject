using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.EfStuff;
using WebApplication.EfStuff.Models;
using WebApplication.EfStuff.Repository;
using WebApplication.EfStuff.Repository.IRepository;
using WebApplication.Models;
using WebApplication.Service;
using WebApplication.Service.IService;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace WebApplication
{
    public class Startup
    {
        public const string AuthMethod = "Cookie";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("connectionString");
            services.AddDbContext<ProjectDbContext>(x => x.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository>(x => new UserRepository(
                x.GetService<ProjectDbContext>()));

            services.AddScoped<IUserService>(x => new UserService(
                x.GetService<IHttpContextAccessor>(),
                x.GetService<IUserRepository>()));

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "MaBoi";
                    config.LoginPath = "/User/Login";
                });

            RegisterMapper(services);
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
        }

        private void RegisterMapper(IServiceCollection services)
        {
            var configExpression = new MapperConfigurationExpression();

            MapBoth<User, RegistrationViewModel>(configExpression);

            var mapperConfiguration = new MapperConfiguration(configExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(c => mapper);
        }

        public void MapBoth<T, Y>(MapperConfigurationExpression configExpression)
        {
            configExpression.CreateMap<T, Y>();
            configExpression.CreateMap<Y, T>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Home}/{id?}");
            });
        }
    }
}