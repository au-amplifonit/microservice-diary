using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fox.Microservices.CommonUtils;
using Fox.Microservices.CommonUtils.Models.Entities;
using Fox.Microservices.Diary.Models.Configuration;
using Fox.Microservices.Diary.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebAPITools;

namespace Fox.Microservices.Diary
{
	public class Startup : BaseStartup
	{
		public Startup(IConfiguration configuration, ILogger<Startup> logger, ILoggerFactory loggerFactory) : base(configuration, logger, loggerFactory)
		{
		}

		public override void ConfigureServices(IServiceCollection services)
		{
			base.ConfigureServices(services);

			// Add our Config object so it can be injected overriding default implementation
			services.Configure<CustomAppSettings>(Configuration.GetSection("AppSettings"));

			string ConnectionString = Configuration.GetConnectionString("ConnectionString");
			DbConnectionStringBuilder ConnBuilder = new DbConnectionStringBuilder();
			ConnBuilder.ConnectionString = ConnectionString;
			_logger.LogInformation("Using database: {0}\\{1}", ConnBuilder["Data Source"], ConnBuilder["Initial Catalog"]);

			services.AddDbContext<DiaryContext>(options =>
					options.UseLazyLoadingProxies()
					.EnableDetailedErrors()
					.UseSqlServer(ConnectionString));

			//Ensure common utils capabilities			
			services.AddDbContext<CommonUtilsContext>(options =>
					options.UseLazyLoadingProxies()
					.EnableDetailedErrors()
					.UseSqlServer(ConnectionString));

			services.AddScoped(typeof(IFoxDataService), typeof(FoxDataService));

			services.AddSwaggerGen(c =>
						{
							c.SwaggerDoc("v1", new Info { Title = "FOX Microservices - Diary API", Version = "v1" });
							var filePath = Path.Combine(System.AppContext.BaseDirectory, "Fox.Microservices.Diary.xml");
							c.IncludeXmlComments(filePath);
						});
		}
	}
}
