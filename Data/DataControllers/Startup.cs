using FutbolChallenge.Data.Repository;
using Helpers.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Ninject;
using System.Configuration;
using System.Linq;

namespace DataControllers
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			ConfigRoot = configuration;
		}

		public IConfiguration ConfigRoot { get; }


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
            services.AddAuthentication
            (options => {
                options.DefaultAuthenticateScheme = "Bearer";
            }).AddJwtBearer();

            services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataControllers", Version = "v1" });
			});

			services.AddMicrosoftIdentityWebApiAuthentication(ConfigRoot);

			var kernReg = services.FirstOrDefault(s => s.ServiceType == typeof(StandardKernel));
			var kernel = (StandardKernel)kernReg.ImplementationInstance;

			services.AddSingleton<IDataRepositoryProvider>(BootstrapHelper.TypeCreate<IDataRepositoryProvider>(kernel));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Participant v1"));
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

		//	app.UseAuthentication();
		//	app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
