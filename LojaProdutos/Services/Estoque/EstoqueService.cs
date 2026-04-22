using LojaProdutos.Data;
using LojaProdutos.Models;
using LojaProdutos.Services.Produtos;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Services.Estoque
{
    public class EstoqueService : IEstoqueInterface
    {
        private readonly AppDbContext _context;
        private readonly IProdutosInterface _produtosInterface;
        public EstoqueService(AppDbContext context, IProdutosInterface produtosInterface)
        {
            _context = context;
            _produtosInterface = produtosInterface;
        }

        public async Task<EstoqueModel> CriarRegistro(int idProduto)
        {
            try
            {
                var produto = await _produtosInterface.GetProdutosId(idProduto);
                var registro = new EstoqueModel
                {
                    ProdutoModelId = produto.Id,
                    ProdutosModel = produto
                };

                _context.Add(registro);
                await _context.SaveChangesAsync();

                BaixarEstoque(produto);
                _context.Update(produto);
                await _context.SaveChangesAsync();

                return registro;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar registro de estoque: {ex.Message}");
            }
        }

        public void BaixarEstoque(ProdutosModel produtos)
        {
            produtos.QuantidadeEstoque--;
        }

        public List<RegistroProdutosModel> ListagemRegistro()
        {
            try
            {
                var resultado = from c in _context.Estoques.Include(x => x.ProdutosModel)
                                                            .Include(y => y.ProdutosModel.Categoria)
                                                            .ToList()
                                group c by new { c.ProdutosModel.CategoriaModelId, c.DataBaixa } into total
                                select new
                                {
                                    ProdutoId = total.First().ProdutosModel.Categoria.Id,
                                    CategoriaNome = total.First().ProdutosModel.Categoria.Nome,
                                    DataCompra = total.First().DataBaixa,
                                    Total = total.Sum(c => c.ProdutosModel.Valor)
                                };
                var totalGeral = _context.ProdutosBaixados.Include(x => x.ProdutosModel)
                                                        .Include(y => y.ProdutosModel.Categoria)
                                                        .Sum(c => c.ProdutosModel.Valor);

                List<RegistroProdutosModel> registros = resultado.Select(r => new RegistroProdutosModel
                {
                    ProdutoId = r.ProdutoId,
                    CategoriaNome = r.CategoriaNome,
                    DataCompra = r.DataCompra,
                    Total = r.Total,
                    TotalGeral = totalGeral
                }).ToList();

                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar registros de estoque: {ex.Message}");
            }
    }
}
