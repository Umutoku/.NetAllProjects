﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(FirstFunction.Startup))] // Startup class'ını belirtiyoruz
namespace FirstFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IService, Service>(); // IService interface'ini Service class'ına bağlıyoruz
        }
    }
}
