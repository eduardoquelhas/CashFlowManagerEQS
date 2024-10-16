using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CashFlowManagerEQS.Data;
using CashFlowManagerEQS.Models;

namespace CashFlowManagerEQS.Services
{
    public class LancamentoService
    {
        private readonly ApplicationDbContext _context;

        public LancamentoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para adicionar um novo lançamento
        public async Task AdicionarLancamentoAsync(Lancamento lancamento)
        {
            _context.Lancamentos.Add(lancamento);
            await _context.SaveChangesAsync();
        }

        // Método para gerar o relatório diário de lançamentos
        public async Task<IEnumerable<Lancamento>> GerarRelatorioDiarioAsync(DateTime data)
        {
            return await _context.Lancamentos
                .Where(l => l.Data.Date == data.Date)
                .ToListAsync();
        }

        // Método para editar um lançamento existente
        public async Task EditarLancamentoAsync(Lancamento lancamento)
        {
            var lancamentoExistente = await _context.Lancamentos.FindAsync(lancamento.Id);
            if (lancamentoExistente != null)
            {
                lancamentoExistente.Descricao = lancamento.Descricao;
                lancamentoExistente.Tipo = lancamento.Tipo;
                lancamentoExistente.Valor = lancamento.Valor;
                lancamentoExistente.Data = lancamento.Data;

                _context.Lancamentos.Update(lancamentoExistente);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Lançamento não encontrado.");
            }
        }

        // Criar um novo lançamento
        public async Task<bool> CriarLancamentoAsync(Lancamento lancamento)
        {
            if (lancamento.Valor <= 0 || string.IsNullOrEmpty(lancamento.Tipo) || lancamento.Data == default)
            {
                return false;
            }

            _context.Lancamentos.Add(lancamento);
            await _context.SaveChangesAsync();
            return true;
        }

        // Listar todos os lançamentos
        public async Task<List<Lancamento>> ListarLancamentosAsync()
        {
            return await _context.Lancamentos.ToListAsync();
        }

        // Obter um lançamento por ID
        public async Task<Lancamento> ObterLancamentoPorIdAsync(int id)
        {           
            return await _context.Lancamentos.FindAsync(id);
        }

        // Atualizar um lançamento existente
        public async Task<bool> AtualizarLancamentoAsync(Lancamento lancamento)
        {
            var lancamentoExistente = await _context.Lancamentos.FindAsync(lancamento.Id);
            if (lancamentoExistente == null)
            {
                return false;
            }

            lancamentoExistente.Descricao = lancamento.Descricao;
            lancamentoExistente.Valor = lancamento.Valor;
            lancamentoExistente.Tipo = lancamento.Tipo;
            lancamentoExistente.Data = lancamento.Data;

            _context.Lancamentos.Update(lancamentoExistente);
            await _context.SaveChangesAsync();
            return true;
        }

        // Deletar um lançamento
        public async Task<bool> DeletarLancamentoAsync(int id)
        {
            var lancamento = await _context.Lancamentos.FindAsync(id);
            if (lancamento == null)
            {
                return false;
            }

            _context.Lancamentos.Remove(lancamento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}