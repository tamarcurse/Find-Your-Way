using DAL;
using BLL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.models;
using DAL.interfaces;
using BLL.interfaces;
using DAL.classes;
using BLL.classes;
using BLL.Algoritm;

using BLL.googleMaps;
using BLL.OR_Tools;
using BLL.algoritm;

namespace truckingWebAPI
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
            //נאפשר הרשאת גישה מכל מקור, כל פונקציה וכל כותרת
            services.AddCors(o => o.AddPolicy("newCors", p => p.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "truckingWebAPI", Version = "v1" });
            });

            services.AddScoped(typeof(IProviderDL), typeof(providersDL));
            services.AddScoped(typeof(IproviderBLL), typeof(ProviderBLL));
            services.AddScoped(typeof(ISizeContainDAL), typeof(SizeContainDAL));
            services.AddScoped(typeof(ISizeContainsBLL), typeof(SizeContainsBLL));
          
            //services.AddScoped(typeof(ITimeLimitDAL), typeof(TimeLimitDAL));
            services.AddScoped(typeof(IPlaceDAL), typeof(PlaceDAL));
            services.AddScoped(typeof(IPlaceBLL), typeof(PlaceBLL));
            services.AddScoped(typeof(IShopDAL), typeof(ShopDAL));
            services.AddScoped(typeof(IShopBLL), typeof(ShopBLL));
            services.AddScoped(typeof(ITrucksDAL), typeof(TrucksDAL));
            services.AddScoped(typeof(ITrucksBLL), typeof(TrucksBLL));
            services.AddScoped(typeof(IStationsInShopBLL), typeof(StationsInShopBLL));
            services.AddScoped(typeof(IStationsInShopDAL), typeof(StationsInShopDAL));
            services.AddScoped(typeof(ManegerInterface), typeof(Maneger));
            services.AddScoped(typeof(DataModelTruckInterface), typeof(DataModelTruck));
            services.AddScoped(typeof(DataModelVertexInterface), typeof(DataModelVertex));
            services.AddScoped(typeof(AllVertexInterface), typeof(AllVertex));
            services.AddScoped(typeof(GroupsAndTruckInterface), typeof(GroupsAndTruck));
            services.AddScoped(typeof(DistanceMatrixInterface), typeof(DistanceMatrix));
            services.AddScoped(typeof(IGoogleMapsPlaceId), typeof(GoogleMapsPlaceId));
            services.AddScoped(typeof(IDataModel), typeof(DataModel));
            services.AddScoped(typeof(IFindWay), typeof(FindWay));
            services.AddDbContext<FindYourWayContext>(o => o.UseSqlServer("Data Source=408-08;Initial Catalog=FindYourWay;Integrated Security=True"));
            services.AddControllers()
                  .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "truckingWebAPI v1"));
            }
            //ניתן ליישום להשתמש הרשאת גישה זו
            app.UseCors("newCors");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
