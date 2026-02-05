using Supabase;
using ApiBase_Datos.Models;
using ApiBase_Datos.Models.Db;

namespace ApiBase_Datos.Services
{
    public class ProductosService
    {
        private readonly Client _supabase;

        public ProductosService(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<List<Productos>> ObtenerTodos()
        {
            var res = await _supabase
                .From<ProductoDb>()
                .Get();

            return res.Models.Select(p => new Productos
            {
                Pro_Id = p.Pro_Id,
                Pro_Nombre = p.Pro_Nombre,
                Pro_Stock = p.Pro_Stock,
                Pro_Precio = p.Pro_Precio,
                Pro_Tipo = p.Pro_Tipo
            }).ToList();
        }

        public async Task Insertar(Productos p)
        {
            Console.WriteLine("INSERTANDO PRODUCTO");

            var db = new ProductoDb
            {
                Pro_Nombre = p.Pro_Nombre,
                Pro_Stock = p.Pro_Stock,
                Pro_Precio = p.Pro_Precio,
                Pro_Tipo = p.Pro_Tipo
            };

            var res = await _supabase
                .From<ProductoDb>()
                .Insert(db);

            Console.WriteLine("RESPUESTA SUPABASE: " + res.ResponseMessage.StatusCode);
        }

        public async Task Eliminar(long id)
        {
            await _supabase
                .From<ProductoDb>()
                .Where(p => p.Pro_Id == id)
                .Delete();

        }
        public async Task Actualizar(Productos p)
        {
            var db = new ProductoDb
            {
                Pro_Id = p.Pro_Id,
                Pro_Nombre = p.Pro_Nombre,
                Pro_Stock = p.Pro_Stock,
                Pro_Precio = p.Pro_Precio,
                Pro_Tipo = p.Pro_Tipo
            };
            await _supabase
                .From<ProductoDb>()
                .Where(x => x.Pro_Id == p.Pro_Id)
                .Update(db);
        }
    }
}