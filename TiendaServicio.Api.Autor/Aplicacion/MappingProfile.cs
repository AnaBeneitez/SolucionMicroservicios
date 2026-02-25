using AutoMapper;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Modelo.DTOs;

namespace TiendaServicio.Api.Autor.Aplicacion;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AutorLibro, AutorDto>();
    }
}
