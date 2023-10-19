using Microsoft.EntityFrameworkCore;

using DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// El ciclo de vida de los servicios aquí definido se gestionan
// de forma automática junto a la sesión https.
builder.Services.AddControllersWithViews();
// Se define un nuevo servicio en el contenedor de servicios
// que generará el contexto de conexión a base de datos
// para la sesión https de usuario.
builder.Services.AddDbContext<Contexto>(
     o => o.UseNpgsql(builder.Configuration.GetConnectionString("CadenaConexionPostgreSQL")));

var app = builder.Build();

// Cuando se quiera desplegar una aplicación con la opción de construcción
// de base de datos migrate (con esto no se tendría que lanzar por consola)
// se deberá generar un contexto 
// que solo este vivo (using) durante la ejecución de la migración.
using (var scope = app.Services.CreateScope())
{
    var appDBContext = scope.ServiceProvider.GetRequiredService<Contexto>();
    appDBContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

