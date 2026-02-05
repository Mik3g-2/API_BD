using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ApiBase_Datos.Models.Db
{
    [Table("Producto")]
    public class ProductoDb : BaseModel
    {
        [PrimaryKey("Pro_Id", false)]
        public long? Pro_Id { get; set; }

        [Column("Pro_Nombre")]
        public string Pro_Nombre { get; set; } = string.Empty;

        [Column("Pro_Stock")]
        public long Pro_Stock { get; set; }

        [Column("Pro_Precio")]
        public decimal Pro_Precio { get; set; }

        [Column("Pro_Tipo")]
        public string Pro_Tipo { get; set; } = string.Empty;
    }
}
