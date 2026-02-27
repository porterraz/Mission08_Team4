using Microsoft.EntityFrameworkCore;
using Mission08_Team4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register the SQLite-backed EF Core context for task storage.
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TaskConnection")));
// Resolve the repository through DI anywhere the interface is requested.
builder.Services.AddScoped<ITaskRepository, EFTaskRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Create the database and seed lookup data on first run.
    var context = scope.ServiceProvider.GetRequiredService<TaskContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
