using ApiBase_Datos.Models;

namespace ApiBase_Datos.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}


namespace ApiBase_Datos.Models
{
    public class ProductoResponse
    {
        public required List<Productos> Results { get; set; }

    }
}
