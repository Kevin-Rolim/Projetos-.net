using Microsoft.EntityFrameworkCore;

public class EventoService
{
    private readonly EventFlowDbContext _context;

    public EventoService(EventFlowDbContext context)
    {
        _context = context;
    }

    public async Task<List<Evento>> ListarEventosAsync()
    {
        return await _context.Eventos.ToListAsync();
    }
    public async Task<Evento?> BuscarPorIdAsync(int id)
    {
        return await _context.Eventos.FindAsync(id);
    }
    private void ValidarEvento (Evento evento)
    {
        if (evento.DataHora < DateTime.Now)
        {
            throw new InvalidOperationException(
            "A data marcada deve ser depois do momento atual.");
        }
        if (evento.CapacidadeMaxima <= 0)
        {
            throw new InvalidOperationException(
                "A capacidade deve ser maior que zero."
            );
        }
    }

    public async Task AdicionarEventoAsync(Evento evento)
    {
        ValidarEvento(evento);
        evento.Situacao = SituacaoEvento.Planejado;
        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
    }
    public async Task AlterarEventoAsync(int id, string titulo,string? descricao,DateTime dataHora, string local, int capacidadeMaxima )
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento is null)
        {
                throw new InvalidOperationException(
                "Evento não encontrado."
                );
        }
        evento.Titulo = titulo;
        evento.Descricao = descricao;
        evento.DataHora = dataHora;
        evento.Local = local;
        evento.CapacidadeMaxima = capacidadeMaxima;
        ValidarEvento(evento);
    
        await _context.SaveChangesAsync();
    }
    public async Task CancelarEventoAsync(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento is null)
        {
                throw new InvalidOperationException(
            "Evento não encontrado."
                );
        }
        if (evento.Situacao == SituacaoEvento.Encerrado)
        {
            throw new InvalidOperationException(
            "Não é possível cancelar um evento encerrado."
            );
        }
        if (evento.Situacao == SituacaoEvento.Cancelado)
        {
            throw new InvalidOperationException(
            "Não é possível cancelar um evento cancelado."
            );
        }
        evento.Situacao = SituacaoEvento.Cancelado;
            await _context.SaveChangesAsync();
    }
}