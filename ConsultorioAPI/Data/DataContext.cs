

using ConsultorioAPI.Models;

namespace ConsultorioAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consulta>()
            .HasOne(c => c.Medico)
            .WithMany()
            .HasForeignKey(c => c.IdMedico);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.IdPaciente);

            modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Medicos)
            .WithMany(m => m.Pacientes)
            .UsingEntity<Dictionary<string, object>>(
        "PacienteMedico",
        j => j
            .HasOne<Medico>()
            .WithMany()
            .HasForeignKey("IdMedico"),
        j => j
            .HasOne<Paciente>()
            .WithMany()
            .HasForeignKey("IdPaciente"),
        j =>
        {

            j.HasKey("IdPaciente", "IdMedico");

            j.ToTable("PacienteMedico");
        });

        }
    }
}
