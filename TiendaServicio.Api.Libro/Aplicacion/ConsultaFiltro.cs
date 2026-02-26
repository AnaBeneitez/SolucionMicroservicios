using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Modelo.DTOs;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion;

public class ConsultaFiltro
{
    public class LibroUnico : IRequest<LibroMaterialDto>
    {
        public string LibreriaMaterialID { get; set; }
    }

    public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto>
    {
        private readonly ContextoLibreria _contextoLibreria;
        private readonly IMapper _mapper;
        public Manejador(ContextoLibreria contextoLibreria, IMapper mapper)
        {
            _contextoLibreria = contextoLibreria;
            _mapper = mapper;
        }
        public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.LibreriaMaterialID, out Guid libreriaMaterialId))
            {
                throw new Exception("El ID proporcionado no es un GUID válido");
            }

            LibreriaMaterial? libro = await _contextoLibreria.Libros
                                .Where(x => x.LibreriaMaterialID == libreriaMaterialId)
                                .FirstOrDefaultAsync();

            if (libro == null)
            {
                throw new Exception("No se encontró el libro");
            }

            LibroMaterialDto libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);

            return libroDto;
        }
    }
}
