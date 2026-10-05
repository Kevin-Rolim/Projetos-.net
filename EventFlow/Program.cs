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
builder.Services.AddScoped<ParticipanteService>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAntiforgery();
app.UseAuthorization();

app.MapControllerRoute("default","{controller=Eventos}/{action=Index}/{id?}");

app.Run();