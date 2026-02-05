
namespace ApiBase_Datos.Models
{
    public class Productos
    {
        public long? Pro_Id { get; set; }
        public string Pro_Nombre { get; set; } = string.Empty;
        public long Pro_Stock { get; set; }
        public decimal Pro_Precio { get; set; }
        public string Pro_Tipo { get; set; } = string.Empty;
    }
}