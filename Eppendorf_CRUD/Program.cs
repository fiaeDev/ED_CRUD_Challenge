using Data.Context;
using Data.Helper;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Eppendorf_CRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            ConfigureServices(ref builder);

            var app = builder.Build();

            using (var client = new EDBContext())
            {
                client.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }


        private static void ConfigureServices(ref WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddEntityFrameworkSqlite().AddDbContext<EDBContext>();
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
            builder.Services.AddScoped<IDeviceHealthRepository, DeviceHealthRepository>();
            builder.Services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
            builder.Services.AddScoped<DataHelper>();

        }

        
    }
}