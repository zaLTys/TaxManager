using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaxManager.Api.Contexts;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Domain;

namespace TaxManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ??
                             throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = _configuration["connectionStrings:cityInfoDBConnectionString"];
            services.AddDbContext<TaxRepositoryContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });

            services.AddTransient<ITaxManager, Domain.TaxManager>();

#if DEBUG
            services.AddTransient<ITaxRepository, TaxRepository>();
#else
            services.AddTransient<ITaxRepository, InMemoryTaxRepository>();
#endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStatusCodePages();

        }
    }
}
