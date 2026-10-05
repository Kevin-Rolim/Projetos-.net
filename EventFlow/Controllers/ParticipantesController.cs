using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

public class ParticipantesController : Controller
{
    private readonly ParticipanteService _service;
    public ParticipantesController(ParticipanteService service)
    {
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        var participantes = await _service.ListarParticipantesAsync();
        return View(participantes);
    }
    public async Task<IActionResult> Details(int id)
    {
        var participante = await _service.BuscarParticipantePorIdAsync(id);
        if (participante is null)
        {
            return NotFound();
        }
        return View(participante);
    }
    public IActionResult Create()
    {
        
        return View();
    }
    [HttpPost]
    [RequireAntiforgeryToken]
    public async Task<IActionResult> Create(ParticipanteCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var participante = new Participante
        {
            Nome = model.Nome,
            Email = model.Email,
            Telefone = model.Telefone,
        };
        try
        {
            await _service.AdicionarParticipanteAsync(participante);
            return RedirectToAction(nameof(Index));
        }
        catch(InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(ParticipanteCreateViewModel.Email), ex.Message);
            return View(model);
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(nameof(ParticipanteCreateViewModel.Nome), ex.Message);
            return View(model);
        }
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var participante = await _service.BuscarParticipantePorIdAsync(id);
        if (participante is null)
        {
            return NotFound();
        }
        var model = new ParticipanteEditViewModel
        {
            Id = participante.Id,
            Nome = participante.Nome,
            Email = participante.Email,
            Telefone = participante.Telefone,
        };
        return View(model);
        
    }
    [HttpPost]
    [RequireAntiforgeryToken]
    public async Task<IActionResult> Edit(int id, ParticipanteEditViewModel model)
    {
        if (id != model.Id)
            {
                return BadRequest();
            }
        if (!ModelState.IsValid)
        {
            return View (model);
        }
        try
        {
            await _service.AlterarParticipanteAsync(
                model.Id,
                model.Nome,
                model.Email,
                model.Telefone
            );
            return RedirectToAction(nameof(Index));
        }
        catch(InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(ParticipanteEditViewModel.Email), ex.Message);
            return View(model);
        }
        catch(KeyNotFoundException ex)
        {
            ModelState.AddModelError(nameof(ParticipanteEditViewModel.Id), ex.Message);
            return View(model);
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(nameof(ParticipanteEditViewModel.Nome), ex.Message);
            return View(model);
        }
    }

}