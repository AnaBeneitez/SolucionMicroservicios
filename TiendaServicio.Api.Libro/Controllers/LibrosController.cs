using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicio.Api.Libro.Aplicacion;
using TiendaServicio.Api.Libro.Modelo.DTOs;

namespace TiendaServicio.Api.Libro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibrosController : ControllerBase
{
    private readonly IMediator _mediator;

    public LibrosController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
    {
        return await _mediator.Send(data);
    }

    [HttpGet]
    public async Task<ActionResult<List<LibroMaterialDto>>> GetLibro()
    {
        return await _mediator.Send(new Consulta.ListaLibro());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LibroMaterialDto>> GetLibroById(string id)
    {
        return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibreriaMaterialID = id });
    }
}
