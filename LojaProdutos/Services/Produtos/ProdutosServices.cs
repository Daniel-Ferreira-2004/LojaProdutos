using Azure.Messaging;
using LojaProdutos.Data;
using LojaProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Services.Produtos
{
    public class ProdutosServices : IProdutosInterface
    {
        private readonly AppDbContext  _context;
        public ProdutosServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProdutosModel>> BuscarProdutos()
        {
            try 
            {
                return await _context.Produtos.Include(c => c.Categoria).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
