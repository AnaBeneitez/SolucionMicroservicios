using TiendaServicio.Api.CarritoCompra.InterfazRemota;
using TiendaServicio.Api.CarritoCompra.ModeloRemoto;

namespace TiendaServicio.Api.CarritoCompra.ServicioRemoto;

public class LibrosService : ILibrosService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<LibrosService> _logger;

    public LibrosService(IHttpClientFactory httpClientFactory, ILogger<LibrosService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<(bool resultado, LibroRemoto libro, string errorMessage)> GetLibro(Guid libroId)
    {
        try
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("Libros");
            HttpResponseMessage response = await httpClient.GetAsync($"/api/Libros/{libroId}");
            if (response.IsSuccessStatusCode)
            {
                LibroRemoto? libroDto = await response.Content.ReadFromJsonAsync<LibroRemoto>();
                return (true, libroDto, null);
            }

            _logger.LogError($"Error al obtener el libro con ID {libroId}. Status Code: {response.StatusCode}");
            return (false, null, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, $"Excepción al obtener el libro con ID {libroId}");
            return (false, null, ex.Message);
        }
    }
}
