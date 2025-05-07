using ADMIN.ITEGAMAX._4._0;
using ADMIN.ITEGAMAX._4._0.App_Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Store config settings in one object
builder.Configuration.GetSection("CUSTAPPSETTINGS").Bind(CLCustAppsettings.Instance);
builder.Configuration.GetSection("ConnectionStrings").Bind(CLConnectionStrings.Instance);
builder.Configuration.GetSection("COMPANYSETTINGS").Bind(CLCompanySettings.Instance);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ITeGAMAX4Context>(options =>
  options.UseMySQL(builder.Configuration.GetConnectionString("MariaDbConnectionString")!));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

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
