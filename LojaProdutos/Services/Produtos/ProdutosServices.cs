using Azure.Messaging;
using LojaProdutos.Data;
using LojaProdutos.DTO.Produto;
using LojaProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LojaProdutos.Services.Produtos
{
    public class ProdutosServices : IProdutosInterface
    {
        private readonly AppDbContext _context;
        private readonly string _sistema;
        public ProdutosServices(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
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

        public async Task<ProdutosModel> Cadastrar(CriarProdutoDTO criarProdutoDTO, IFormFile foto)
        {
            try
            {
                var NomeCaminhoArquivo = GeraCaminhoArquivo(foto);
                var produto = new ProdutosModel
                {
                    Nome = criarProdutoDTO.Nome,
                    Marca = criarProdutoDTO.Marca,
                    Valor = criarProdutoDTO.Valor,
                    QuantidadeEstoque = criarProdutoDTO.QuantidadeEstoque,
                    Foto = await NomeCaminhoArquivo,
                    CategoriaModelId = criarProdutoDTO.CategoriaModelId
                };

                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private async Task<string> GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            var caminhoParaSalvarImagens = _sistema + "\\imagens\\";

            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }
            using (var stream = File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
            {
                await foto.CopyToAsync(stream);
            }
            return nomeCaminhoImagem;
        }
    }
}
