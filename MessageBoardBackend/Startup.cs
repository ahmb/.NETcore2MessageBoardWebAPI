using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;


namespace MessageBoardBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //------------------------------------------------------------
            ////NOTE ON ADDING DBCONTEXT HERE:
            //You can add dependencies to the Configure method and they will automatically get resolved. Try changing signature to public void Configure(IApplicationBuilder app, IHostingEnvironment env, MyDbContext dbContext) – Paul Hiles Aug 25 at 15:50
            //Paul Hiles comment is correct but that method works better in .NET Core 1.0.

            //In ASP.NET Core 2.0 it's generally a bad idea to run any database setup in Startup.cs. This is because if you run any migrations from the CLI or Visual Studio it will run all of Startup.cs and try to run your configuration which will fail. Of course if you don't use Entity - Framework then this isn't a problem however its still not the recommended way of doing it in 2.0. It's now recommended to do it in Program.cs.

            //For example you can create a extension method of IWebHost that will run any setup you need.

            //public static IWebHost MigrateDatabase(this IWebHost webHost)
            //{
            //    using (var scope = webHost.Services.CreateScope())
            //    {
            //        var services = scope.ServiceProvider;
            //        var dbContext = services.GetRequiredService<YourDbContext>();

            //        dbContext.Database.Migrate();
            //    }

            //    return webHost;
            //}
            //And then in Program.cs you can then call that method before running.

            //public static void Main(string[] args)
            //{
            //    BuildWebHost(args)
            //        .MigrateDatabase()
            //        .Run();
            //}
            //------------------------------------------------------------

            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
            services.AddCors(options => options.AddPolicy("Cors",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("Cors");
            app.UseMvc();

           
        }

    }
}
