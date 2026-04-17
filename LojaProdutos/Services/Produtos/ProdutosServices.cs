using Azure.Messaging;
using LojaProdutos.Data;
using LojaProdutos.DTO.Produto;
using LojaProdutos.DTO.Produtos;
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

        public Task<List<ProdutosModel>> BuscarProdutosFiltro(string pesquisar)
        {
            try
            {
                var produtos = _context.Produtos
                                        .Include(c => c.Categoria)
                                        .Where(p => p.Nome.Contains(pesquisar) || p.Marca.Contains(pesquisar))
                                        .ToListAsync();
                return produtos;
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
                    Foto = NomeCaminhoArquivo,
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

        public async Task<ProdutosModel> Editar(EditarProdutoDTO editarProdutoDTO, IFormFile? foto)
        {

            try
            {
                var produto = await GetProdutosId(editarProdutoDTO.Id);

                var nomeCaminhoImagem = "";
                if (foto != null)
                {
                    string CaminhoCapaExistente = _sistema + "\\imagens\\" + produto.Foto;
                    if (File.Exists(CaminhoCapaExistente))
                    {
                        File.Delete(CaminhoCapaExistente);
                    }

                    nomeCaminhoImagem = GeraCaminhoArquivo(foto);
                }

                produto.Nome = editarProdutoDTO.Nome;
                produto.Valor = editarProdutoDTO.Valor;
                produto.Marca = editarProdutoDTO.Marca;
                produto.QuantidadeEstoque = editarProdutoDTO.QuantidadeEstoque;
                produto.CategoriaModelId = editarProdutoDTO.CategoriaModelId;

                if (nomeCaminhoImagem != "")
                {
                    produto.Foto = nomeCaminhoImagem;
                }

                _context.Update(produto);
                await _context.SaveChangesAsync();
                return produto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<ProdutosModel> GetProdutosId(int id)
        {
            try
            {
                var produto = await _context.Produtos
                                            .Include(c => c.Categoria)
                                            .FirstOrDefaultAsync(p => p.Id == id);
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutosModel> Remover(int id)
        {
            try
            {
                var produto = await GetProdutosId(id);
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch ( Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private string GeraCaminhoArquivo(IFormFile foto)
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
                 foto.CopyToAsync(stream).Wait();
            }
            return nomeCaminhoImagem;
        }
    }
}
