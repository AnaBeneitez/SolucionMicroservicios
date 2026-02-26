using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicio.Api.CarritoCompra.Aplicacion;
using TiendaServicio.Api.CarritoCompra.Modelo.DTOs;

namespace TiendaServicio.Api.CarritoCompra.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarritoComprasController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarritoComprasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
    {
        return await _mediator.Send(data);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<CarritoDto>> GetCarrito(int Id)
    {
        return await _mediator.Send(new Consulta.CarritoUnico { CarritoSesionId = Id });
    }
}
