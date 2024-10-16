using Microsoft.EntityFrameworkCore;
using CashFlowManagerEQS.Models;

namespace CashFlowManagerEQS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lancamento> Lancamentos { get; set; }
    }
}
