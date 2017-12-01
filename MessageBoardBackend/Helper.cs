using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoardBackend
{
    public static class Helper
    {
        public static void SeedData(ApiContext context)
        {
            context.Messages.Add(new Models.Message
            {
                Owner = "John",
                Text = "hello"
            });
            context.Messages.Add(new Models.Message
            {
                Owner = "Bob",
                Text = "hi"
            });

            context.SaveChanges();
        }

        public static IWebHost SeedDBData(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApiContext>();
                Helper.SeedData(dbContext);
            }

            return webHost;
        }
    }
}
