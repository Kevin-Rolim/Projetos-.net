using Microsoft.EntityFrameworkCore;

public class EventFlowDbContext: DbContext
{

    public EventFlowDbContext(
            DbContextOptions<EventFlowDbContext> options)
            : base(options)
    {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Inscricao>()
        .HasKey(m => new { m.Evento_Id, m.Participante_Id });
}
    public DbSet<Evento> Eventos {get; set;}
    public DbSet<Participante> Participantes {get; set;}
    public DbSet<Inscricao> Inscricoes {get; set;}
}