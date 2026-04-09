using LojaProdutos.Data;
using LojaProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Services.Categorias
{
    public class CategoriaServices :ICategoriaInterface
    {
        private readonly AppDbContext _context;
        public CategoriaServices(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<CategoriaModel>> BuscarCategorias()
        {
            try
            {
                var categorias =_context.Categorias.ToListAsync();
                return categorias;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
