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
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace NeelSwaggerSampleApplication
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "My first API",
                    Description = "My First ASP.NET Core 2.0 Web API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Neel Bhatt", Email = "neel.bhatt40@gmail.com", Url = "https://neelbhatt40.wordpress.com/" }
                });

                c.SwaggerDoc("v2", new Info
                {
                    Version = "v2",
                    Title = "My second API",
                    Description = "My Second ASP.NET Core 2.0 Web API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Neel Bhatt 2", Email = "neel.bhatt40@gmail.com", Url = "https://neelbhatt40.wordpress.com/" }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "NeelSwaggerSampleApplication.xml");
                c.IncludeXmlComments(xmlPath);
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            });
        }
    }
}
