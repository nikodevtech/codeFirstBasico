using Microsoft.EntityFrameworkCore;

using DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// El ciclo de vida de los servicios aqu� definido se gestionan
// de forma autom�tica junto a la sesi�n https.
builder.Services.AddControllersWithViews();
// Se define un nuevo servicio en el contenedor de servicios
// que generar� el contexto de conexi�n a base de datos
// para la sesi�n https de usuario.
builder.Services.AddDbContext<Contexto>(
     o => o.UseNpgsql(builder.Configuration.GetConnectionString("CadenaConexionPostgreSQL")));

var app = builder.Build();

// Cuando se quiera desplegar una aplicaci�n con la opci�n de construcci�n
// de base de datos migrate (con esto no se tendr�a que lanzar por consola)
// se deber� generar un contexto 
// que solo este vivo (using) durante la ejecuci�n de la migraci�n.
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

