using LojaProdutos.Data;
using LojaProdutos.Models;
using LojaProdutos.Services.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

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
                    Produtos = produto
                };

                _context.Add(registro);

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
            if (produtos.QuantidadeEstoque <= 0)
                throw new Exception("Estoque insuficiente para realizar a operação.");

            produtos.QuantidadeEstoque--;
        }

        public List<RegistroProdutosModel> ListagemRegistro()
        {
            try
            {
                var resultado = from c in _context.Estoques
                                .Include(x => x.Produtos)
                                .Include(y => y.Produtos.Categoria)
                                .ToList()
                                group c by new { c.Produtos.CategoriaModelId, c.DataBaixa } into total
                                select new
                                {
                                    ProdutoId = total.First().Produtos.Categoria.Id,
                                    CategoriaNome = total.First().Produtos.Categoria.Nome,
                                    DataCompra = total.First().DataBaixa,
                                    Total = total.Sum(c => c.Produtos.Valor)
                                };
                var totalGeral = _context.Estoques.Include(x => x.Produtos)
                                                  .Include(y => y.Produtos.Categoria)
                                                  .Sum(c => c.Produtos.Valor);

                List<RegistroProdutosModel> Lista = new List<RegistroProdutosModel>();

                foreach (var result in resultado)
                {
                    var registro = new RegistroProdutosModel()
                    {
                        ProdutoId = result.ProdutoId,
                        CategoriaNome = result.CategoriaNome,
                        DataCompra = result.DataCompra,
                        Total = result.Total,
                        TotalGeral = totalGeral
                    };

                    Lista.Add(registro);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar registros de estoque: {ex.Message}");
            }
        }
    }
}
