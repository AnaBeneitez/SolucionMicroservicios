using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicio.Api.CarritoCompra.Aplicacion;

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
}
