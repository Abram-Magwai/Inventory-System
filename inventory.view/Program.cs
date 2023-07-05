using inventory.infrastructure.Repositories;
using inventory.view.Interfaces;
using inventory.view.Services;
using Mongo.Interfaces;
using Mongo.Models;
using jsreport.AspNetCore;
using jsreport.Binary;
using jsreport.Local;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.Configure<DbSettings>(builder.Configuration.GetSection(nameof(DbSettings)));

services.AddScoped<IInventoryService, InventoryService>();
services.AddScoped<IShippingService, ShippingService>();
services.AddScoped<ISupplierService, SupplierService>();
services.AddScoped<IRestockService, RestockService>();
services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
services.AddJsReport(new LocalReporting().UseBinary(JsReportBinary.GetBinary()).KillRunningJsReportProcesses().AsUtility().Create());

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
