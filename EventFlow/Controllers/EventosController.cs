using Microsoft.AspNetCore.Mvc;

public class EventosController : Controller
{
    private readonly EventoService _service;
    public EventosController(EventoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var eventos = await _service.ListarEventosAsync();
        return View(eventos);
    }
    public async Task<IActionResult> Details(int id)
    {
        var evento = await _service.BuscarPorIdAsync(id);
        if (evento is null)
        {
            return NotFound();
        }
        return View(evento);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(EventoCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var evento = new Evento
        {
            Titulo = model.Titulo,
            Descricao = model.Descricao,
            DataHora = model.DataHora,
            Local = model.Local,
            CapacidadeMaxima = model.CapacidadeMaxima

        };
        try
        {
            await _service.AdicionarEventoAsync(evento);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(
                nameof(EventoCreateViewModel.DataHora),
                ex.Message
            );
        return View(model);
        }
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        var evento = await _service.BuscarPorIdAsync(Id);
        if (evento is null)
        {
            return NotFound();
        }
        var model = new EventoEditViewModel
        {
            Id = evento.Id,
            Titulo = evento.Titulo,
            Descricao = evento.Descricao,
            DataHora = evento.DataHora,
            Local = evento.Local,
            CapacidadeMaxima =  evento.CapacidadeMaxima,
        };
        
        return View (model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int Id,EventoEditViewModel model)
    {
    if (!ModelState.IsValid)
        {
            return View(model);
        }
        if (Id != model.Id)
        {
            return BadRequest();
        }
        try
        {
            await _service.AlterarEventoAsync(
                model.Id,
                model.Titulo,
                model.Descricao,
                model.DataHora,
                model.Local,
                model.CapacidadeMaxima
                );
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(
                nameof(EventoEditViewModel.DataHora),
                ex.Message
            );
        return View(model);

    }
    }
    [HttpGet]
    public async Task<IActionResult> Cancel(int Id)
    {
        var evento = await _service.BuscarPorIdAsync(Id);
        if (evento is null)
        {
            return NotFound();
        }
        var model = new EventoCancelViewModel
        {
            Id = evento.Id,
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Cancel(int Id,EventoCancelViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        if (Id != model.Id)
        {
            return BadRequest();
        }
        try{
            await _service.CancelarEventoAsync(model.Id);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        return RedirectToAction(nameof(Index));
    }
}