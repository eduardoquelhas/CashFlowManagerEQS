using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashFlowManagerEQS.Data;
using CashFlowManagerEQS.Models;
using CashFlowManagerEQS.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CashFlowManagerEQS.Tests.CashFlowManagerEQS.Tests.Service
{
    public class LancamentosTests
    {
          private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nome único para cada teste
                .Options;

            return new ApplicationDbContext(options);
        }
        [Fact]
        public async Task AdicionarNovoLancamento_DeveAdicionarLancamentoNoBanco()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            dbContext.Lancamentos.RemoveRange(dbContext.Lancamentos); // Limpa os lançamentos existentes
            await dbContext.SaveChangesAsync();

            var service = new LancamentoService(dbContext);

            var novoLancamento = new Lancamento
            {
                Descricao = "Venda de produto",
                Tipo = "Crédito",
                Valor = 150.00m,
                Data = DateTime.Today
            };

            // Act
            await service.AdicionarLancamentoAsync(novoLancamento);

            // Assert
            var lancamentos = await dbContext.Lancamentos.ToListAsync();
            Assert.Single(lancamentos); // Verifica se há apenas um lançamento
            Assert.Equal("Venda de produto", lancamentos[0].Descricao);
            Assert.Equal(150.00m, lancamentos[0].Valor);
        }

        [Fact]
        public async Task EditarLancamento_DeveAtualizarLancamentoNoBanco()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var service = new LancamentoService(dbContext);

            var lancamento = new Lancamento
            {
                Descricao = "Compra de materiais",
                Tipo = "Débito",
                Valor = 100.00m,
                Data = DateTime.Today
            };

            dbContext.Lancamentos.Add(lancamento);
            await dbContext.SaveChangesAsync();

            // Act
            lancamento.Descricao = "Compra de materiais atualizada";
            lancamento.Valor = 120.00m;
            await service.EditarLancamentoAsync(lancamento);

            // Assert
            var lancamentoEditado = await dbContext.Lancamentos.FindAsync(lancamento.Id);
            Assert.Equal("Compra de materiais atualizada", lancamentoEditado.Descricao);
            Assert.Equal(120.00m, lancamentoEditado.Valor);
        }
        [Fact]
        public async Task DeletarLancamento_DeveRemoverLancamentoDoBanco()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var service = new LancamentoService(dbContext);

            var lancamento = new Lancamento
            {
                Descricao = "Despesa de viagem",
                Tipo = "Débito",
                Valor = 500.00m,
                Data = DateTime.Today
            };

            dbContext.Lancamentos.Add(lancamento);
            await dbContext.SaveChangesAsync();

            // Act
            await service.DeletarLancamentoAsync(lancamento.Id);

            // Assert
            var lancamentoRemovido = await dbContext.Lancamentos.FindAsync(lancamento.Id);
            Assert.Null(lancamentoRemovido);
        }

        [Fact]
        public async Task GerarRelatorioDiario_DeveRetornarLancamentosDoDia()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var service = new LancamentoService(dbContext);

            var lancamento1 = new Lancamento { Descricao = "Venda 1", Tipo = "Crédito", Valor = 100.00m, Data = DateTime.Today };
            var lancamento2 = new Lancamento { Descricao = "Venda 2", Tipo = "Crédito", Valor = 200.00m, Data = DateTime.Today };
            var lancamento3 = new Lancamento { Descricao = "Despesa", Tipo = "Débito", Valor = 50.00m, Data = DateTime.Today.AddDays(-1) };

            dbContext.Lancamentos.AddRange(lancamento1, lancamento2, lancamento3);
            await dbContext.SaveChangesAsync();

            // Act
            var relatorio = await service.GerarRelatorioDiarioAsync(DateTime.Today);

            // Assert
            Assert.Equal(2, relatorio.Count());
            Assert.Contains(relatorio, r => r.Descricao == "Venda 1");
            Assert.Contains(relatorio, r => r.Descricao == "Venda 2");
        }

    }
}