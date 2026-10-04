using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' não encontrada."
    );
builder.Services.AddDbContext<EventFlowDbContext>(
    options =>
        options.UseSqlServer(connectionString)
);
builder.Services.AddScoped<EventoService>();
var app = builder.Build();

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute("default","{controller=Eventos}/{action=Index}/{id?}"
);

app.Run();