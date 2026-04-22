using LojaProdutos.Data;
using LojaProdutos.Models;
using LojaProdutos.Services.Produtos;

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
    }
}
