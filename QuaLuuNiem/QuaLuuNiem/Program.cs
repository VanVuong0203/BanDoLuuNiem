using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuaLuuNiem.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Cấu hình dịch vụ session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Thêm hỗ trợ cho session
builder.Services.AddDistributedMemoryCache();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<QuaLuuNiemContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("QuaLuuNiem")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<QuaLuuNiemContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
