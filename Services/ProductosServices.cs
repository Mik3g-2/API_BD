using ApiBase_Datos.Models;
using ApiBase_Datos.Models.Db;
using Microsoft.AspNetCore.Razor.Hosting;
using MongoDB.Bson.IO;
using Supabase;
using System.Net.Http;
using Newtonsoft.Json;

namespace ApiBase_Datos.Services
{
    public class ProductosService
    {
        private readonly Client _supabase;
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly string _apikey;


        public ProductosService(Client supabase,HttpClient httpClient, IConfiguration configuration)
        {
            _supabase = supabase;
            _httpClient = httpClient;
            _url = configuration["SB:ApiURL"];
            _apikey = configuration["SB:ApiKey"];
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
        public async Task<bool> Actualizar(Productos p)
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

            return true;
        }
        public async Task<Productos> ObtenerPorId(long id)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", _apikey);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apikey);

            var response = await _httpClient.GetAsync($"{_url}/rest/v1/Productos?Pro_Id=eq.{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Productos>>(json);
                return lista.FirstOrDefault();
            }
            return null;
        }
    }
}