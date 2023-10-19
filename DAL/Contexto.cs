using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DAL
{
    // Se define el contexto de conexión a base de datos, que después
    // será instanciado como servicio para cada sesión https de usuario.
    public class Contexto : DbContext
    {
        // Se define un constructor que nos permita generar el contexto
        // como servicio desde el contenedor de servicios de la sesión https
        // de usuario.
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        // Aseguramos el uso de Ids autoincrementales.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        // Los DbSet definen las operaciones básicas contra cada
        // entidad de base de datos (CRUD).
        // En los DbSet se definen colecciones de las clases definidas
        // en Acceso.cs, lo que nos daría las entidades de la base de datos.
        public DbSet<GeneroLibro> Generos { get; set; }
    }
}