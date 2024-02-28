using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreFunc
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var conStr = Environment.GetEnvironmentVariable("SqlConnectionString"); // Connection string'i alıyoruz

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(conStr); // Connection string'i ekliyoruz
            }); // AppDbContext'i ekliyoruz
        }
    }
}
