using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CashFlowManagerEQS.Data;
using CashFlowManagerEQS.Models;

namespace CashFlowManagerEQS.Services
{
    public class ConsolidadoDiarioService
    {
        private readonly ApplicationDbContext _context;

        public ConsolidadoDiarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para obter o saldo consolidado de um dia específico
        public async Task<decimal> ObterSaldoConsolidadoAsync(DateTime data)
        {
            var lancamentos = await _context.Lancamentos
                .Where(l => l.Data.Date == data.Date)
                .ToListAsync();

            var totalCreditos = lancamentos
                .Where(l => l.Tipo.Equals("Crédito", StringComparison.OrdinalIgnoreCase))
                .Sum(l => l.Valor);

            var totalDebitos = lancamentos
                .Where(l => l.Tipo.Equals("Débito", StringComparison.OrdinalIgnoreCase))
                .Sum(l => l.Valor);

            return totalCreditos - totalDebitos;
        }

        // Método para gerar o relatório diário consolidado
        public async Task<string> GerarRelatorioConsolidadoAsync(DateTime data)
        {
            var saldo = await ObterSaldoConsolidadoAsync(data);
            return $"Saldo consolidado para {data.ToShortDateString()}: R$ {saldo}";
        }
    }
}