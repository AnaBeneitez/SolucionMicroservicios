using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Modelo.DTOs;
using TiendaServicio.Api.Autor.Persistencia;

namespace TiendaServicio.Api.Autor.Aplicacion;

public class Consulta
{
    public class ListaAutor : IRequest<List<AutorDto>> { }
    
    public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
    {
        private readonly ContextoAutor _contextoAutor;
        private readonly IMapper _mapper;

        public Manejador(ContextoAutor contextoAutor, IMapper mapper)
        {
            _contextoAutor = contextoAutor;
            _mapper = mapper;
        }
    
        public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
        {
            List<AutorLibro> autores = await _contextoAutor.AutorLibros.ToListAsync();
            List<AutorDto> autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
            return autoresDto;
        }
    }
}
