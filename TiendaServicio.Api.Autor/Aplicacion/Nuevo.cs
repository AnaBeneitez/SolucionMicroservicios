using MediatR;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Persistencia;

namespace TiendaServicio.Api.Autor.Aplicacion;

public class Nuevo
{
    public class Ejecuta: IRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }

    public class Manejador: IRequestHandler<Ejecuta>
    {
        private readonly ContextoAutor _context;

        public Manejador(ContextoAutor contextoAutor)
        {
            _context = contextoAutor;
        }
        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            AutorLibro autorLibro = new AutorLibro
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                AutorLibroGuid = Guid.NewGuid().ToString()
            };

            _context.AutorLibros.Add(autorLibro);

            int valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el autor del libro");
        }
    }
}
