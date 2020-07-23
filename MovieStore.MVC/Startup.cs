using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MovieStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Services;
using MovieStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MovieStore.MVC
{
    public class Startup// the first thing to look up when any error occures.
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<MovieStoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MovieStoreDbConnection")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
                            (options =>
                                {
                                    options.Cookie.Name = "MovieStoreAuthCookie";
                                    options.ExpireTimeSpan = TimeSpan.FromHours(2);
                                    options.LoginPath = "/Account/Login";
                                }
                            );

            // DI in ASP.NET Core has 3 types of Lifetimes, Scoped, Singleton, Transient
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICryptoService, CryptoService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //http request pipeline; when we make request to url; it go to server.And processed by asp.net, at last server respond back with something
        //in ASP.NET Core MiddleWare....a piece of software logic that will be executed...
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //in ASP.NET Core MiddleWare is a piece of software logic that will be executed...below are auto middleware, orders are importmant because they are the logic.

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //routing: pattern matching technique
                // "?" means doesnt necessarily have a value
                //int? x; vs int y;
                // value type x is null and y equals to zero by default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");//means the default is /home/index
            });
        }
    }
}
