using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Modelo.DTOs;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion;

public class Consulta
{
    public class ListaLibro : IRequest<List<LibroMaterialDto>> { }

    public class Manejador : IRequestHandler<ListaLibro, List<LibroMaterialDto>>
    {
        private readonly ContextoLibreria _contextoLibreria;
        private readonly IMapper _mapper;

        public Manejador(ContextoLibreria contextoLibreria, IMapper mapper)
        {
            _contextoLibreria = contextoLibreria;
            _mapper = mapper;
        }

        public async Task<List<LibroMaterialDto>> Handle(ListaLibro request, CancellationToken cancellationToken)
        {
            List<LibreriaMaterial> libros = await _contextoLibreria.Libros.ToListAsync();
            List<LibroMaterialDto> librosDto = _mapper.Map<List<LibroMaterialDto>>(libros);

            return librosDto;
        }
    }
}
