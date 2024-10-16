using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CashFlowManagerEQS.Models;
using CashFlowManagerEQS.Services;


namespace CashFlowManagerEQS.Controllers
{      
    public class LancamentosController : Controller
    {
        private readonly LancamentoService _lancamentoService;  
        private readonly ConsolidadoDiarioService _consolidadoDiarioService;

        // Injetando os serviços através do construtor
        public LancamentosController(LancamentoService lancamentoService, ConsolidadoDiarioService consolidadoDiarioService)
        {
            _lancamentoService = lancamentoService;
            _consolidadoDiarioService = consolidadoDiarioService;
        }

        // Ação que chama o serviço ConsolidadoDiarioService
        public async Task<IActionResult> RelatorioDiario(string data)
        {
            if (DateTime.TryParse(data, out var date))
            {
                var relatorio = await _consolidadoDiarioService.GerarRelatorioConsolidadoAsync(date);
                ViewData["Relatorio"] = relatorio;
            }
            else
            {
                ViewData["Relatorio"] = "Data inválida!";
            }

            return View();
        }
       

        // Listar todos os lançamentos
        public async Task<IActionResult> Index()
        {
            var lancamentos = await _lancamentoService.ListarLancamentosAsync();
            return View(lancamentos);
        }

         public async Task<IActionResult> LancamentoMovimentacao()
        {
            var lancamentos = await _lancamentoService.ListarLancamentosAsync();
            return View(lancamentos);
        }

        // Exibir a página de criação de um novo lançamento
        public IActionResult Criar()
        {
            return View();
        }

        // Criar um novo lançamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                await _lancamentoService.CriarLancamentoAsync(lancamento);
                return RedirectToAction(nameof(LancamentoMovimentacao));
            }
            return View(lancamento);
        }

        // Exibir a página de edição de um lançamento
        public async Task<IActionResult> Editar(int id)
        {
            var lancamento = await _lancamentoService.ObterLancamentoPorIdAsync(id);
            if (lancamento == null)
            {
                return NotFound();
            }
            return View(lancamento);
        }

        // Editar um lançamento existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Editar(int id, Lancamento lancamento)
        public async Task<IActionResult> Editar(int id, Lancamento lancamento)
        {
             ViewBag.Id = id;
            
            if (id != lancamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var atualizado = await _lancamentoService.AtualizarLancamentoAsync(lancamento);
                if (!atualizado)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(LancamentoMovimentacao));
            }
            return View(lancamento);
          
        }

        // Exibir a página de confirmação para deletar um lançamento
        public async Task<IActionResult> Deletar(int id)
        {
            var lancamento = await _lancamentoService.ObterLancamentoPorIdAsync(id);
            if (lancamento == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(LancamentoMovimentacao));
            }
            return View(lancamento);
            //return RedirectToAction(nameof(LancamentoMovimentacao));
            
        }

        // Deletar um lançamento existente
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarDeletar(int id)
        {
            var sucesso = await _lancamentoService.DeletarLancamentoAsync(id);
            if (!sucesso)
            {
                return NotFound();
                //return RedirectToAction(nameof(LancamentoMovimentacao));
            }
            return RedirectToAction(nameof(LancamentoMovimentacao));
        }
    }
}