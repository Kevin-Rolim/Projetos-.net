using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
public class ParticipanteService
{
    private readonly EventFlowDbContext _context;


    public ParticipanteService(EventFlowDbContext context)
    {
        _context = context;
    }

    public async Task<List<Participante>> ListarParticipantes()
    {
        return await _context.Participantes.ToListAsync();
    }

    public async Task<Participante?> BuscarParticipantePorId(int Id)
    {
        return await _context.Participantes.FindAsync(Id);
    }

    private void ValidarParticipante(Participante participante)
    {
        var nome = new string(participante.Nome);
        ValidarModels.
    }
}