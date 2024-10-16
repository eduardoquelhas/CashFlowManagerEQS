using System;
using System.ComponentModel.DataAnnotations;

namespace CashFlowManagerEQS.Models
{
    public class Lancamento
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; } = "";

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public string Tipo { get; set; } = "Crédito"; // Crédito ou Débito

        [Required]
        public DateTime Data { get; set; }
    }
}
