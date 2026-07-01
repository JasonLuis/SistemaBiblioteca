using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(
        "Server=localhost,1433;" +
        "Database=SistemaBiblioteca;" +
        "User Id=sa;" +
        "Password=Jason@0606;" +
        "TrustServerCertificate=True;"
    )
);

services.AddScoped<ILivroRepository, LivroRepository>();
services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
services.AddScoped<BibliotecaService>();
services.AddScoped<Menu>();

var provider = services.BuildServiceProvider();
var menu = provider.GetRequiredService<Menu>();
menu.ExibirMenu();