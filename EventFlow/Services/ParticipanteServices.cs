using Microsoft.EntityFrameworkCore;
public class ParticipanteService
{
    private readonly EventFlowDbContext _context;


    public ParticipanteService(EventFlowDbContext context)
    {
        _context = context;
    }

    private async Task ValidarParticipanteAsync(int? participanteId, string nome, string email)
    {
        var emailJaCadastrado = await _context.Participantes
            .AnyAsync(u => u.Email == email &&
                (!participanteId.HasValue || u.Id != participanteId.Value));

        if (emailJaCadastrado)
        {
            throw new InvalidOperationException(
                "Já existe um participante com esse e-mail."
            );
        }

        ValidarModels.ValidarCaracteresRepetidos(nome);
        ValidarModels.ValidarCaracteresValidos(nome);
        ValidarModels.ValidarMuitasConsoantes(nome);
    }

    public async Task<List<Participante>> ListarParticipantesAsync()
    {
        return await _context.Participantes.ToListAsync();
    }

    public async Task<Participante?> BuscarParticipantePorIdAsync(int Id)
    {
        return await _context.Participantes.FindAsync(Id);
    }
    public async Task AdicionarParticipanteAsync(Participante participante)
    {
        await ValidarParticipanteAsync(null, participante.Nome, participante.Email);
        _context.Participantes.Add(participante);
        await _context.SaveChangesAsync();
    }
    public async Task AlterarParticipanteAsync(int id, string nome, string email, string telefone)
    {
        var participante = await _context.Participantes.FindAsync(id);

        if (participante is null)
        {
            throw new InvalidOperationException(
                "Participante não encontrado."
            );
        }

        await ValidarParticipanteAsync(id, nome, email);
        participante.Nome = nome;
        participante.Email = email;
        participante.Telefone = telefone;
        await _context.SaveChangesAsync();
    }
}