using AutoMapper;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Modelo.DTOs;

namespace TiendaServicio.Api.Libro.Aplicacion;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LibreriaMaterial, LibroMaterialDto>();
    }
}
