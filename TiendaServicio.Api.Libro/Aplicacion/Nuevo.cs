using MediatR;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string AutorLibro { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
        private readonly ContextoLibreria _contextoLibreria;
        public Manejador(ContextoLibreria contextoLibreria)
        {
            this._contextoLibreria = contextoLibreria;
        }
        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            LibreriaMaterial libro = new ()
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                FechaPublicacion = request.FechaPublicacion,
                AutorLibro = new Guid(request.AutorLibro)
            };

            _contextoLibreria.Libros.AddAsync(libro);

            int valor = await _contextoLibreria.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el libro");
        }
    }
}
