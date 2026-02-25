using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Modelo.DTOs;
using TiendaServicio.Api.Autor.Persistencia;

namespace TiendaServicio.Api.Autor.Aplicacion;

public class ConsultaFiltro
{
    public class AutorUnico : IRequest<AutorDto>
    {
        public string AutorGuid { get; set; }
    }

    public class Manejador : IRequestHandler<AutorUnico, AutorDto>
    {
        private readonly ContextoAutor _contextoAutor;
        private readonly IMapper _mapper;
        public Manejador(ContextoAutor contextoAutor, IMapper mapper)
        {
            _contextoAutor = contextoAutor;
            _mapper = mapper;
        }
        public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
        {
            AutorLibro? autorLibro = await _contextoAutor.AutorLibros
                                            .Where(x => x.AutorLibroGuid == request.AutorGuid)
                                            .FirstOrDefaultAsync();

            if (autorLibro == null)
            {
                throw new Exception("No se encontro el autor del libro");
            }

            AutorDto autorLibroDto = _mapper.Map<AutorLibro, AutorDto>(autorLibro);

            return autorLibroDto;
        }
    }
}
