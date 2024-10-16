using Microsoft.EntityFrameworkCore;
using CashFlowManagerEQS.Data;
using CashFlowManagerEQS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurando o DbContext para usar o SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar os serviços
builder.Services.AddScoped<LancamentoService>();
builder.Services.AddScoped<ConsolidadoDiarioService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Lancamentos}/{action=Index}/{id?}");

app.Run();
