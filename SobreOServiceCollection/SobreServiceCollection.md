# Por que usar o ServiceCollection?

O meu código esta assim atualmente:

```csharp
var services = new ServiceCollection();

services.AddScoped<ILivroRepository, LivroRepository>();
services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
services.AddScoped<BibliotecaService>();
services.AddScoped<Menu>();

var provider = services.BuildServiceProvider();

var menu = provider.GetRequiredService<Menu>();

menu.ExibirMenu();
```

Está dizendo: 


```bash
.NET, gerencie a criação dos objetos para mim.
```

Sem **ServiceCollection**, precisaria criar tudo manualmente.
Ficando algo parecido com isso:

```csharp
var optionsBuilder =
    new DbContextOptionsBuilder<AppDbContext>();

optionsBuilder.UseSqlServer(
    "Server=localhost,1433;" +
    "Database=SistemaBiblioteca;" +
    "User Id=sa;" +
    "Password=Jason@0606;" +
    "TrustServerCertificate=True;"
);

var context =
    new AppDbContext(optionsBuilder.Options);

var livroRepository =
    new LivroRepository(context);

var emprestimoRepository =
    new EmprestimoRepository(context);

var bibliotecaService =
    new BibliotecaService(
        livroRepository,
        emprestimoRepository);

var menu =
    new Menu(bibliotecaService);

menu.ExibirMenu();
```

Funciona perfeitamente.

Mas acontece o seguinte:

```bash
Menu
    ↓
BibliotecaService
    ↓
LivroRepository
    ↓
AppDbContext
```

Precisaria conhecer toda a árvore de dependências.
Imagine que amanhã o BibliotecaService passe a depender de:

```csharp
IEmailService
ILogService
INotificacaoService
```

Agora vira:
```csharp 
var emailService =
    new EmailService();

var logService =
    new LogService();

var notificacaoService =
    new NotificacaoService();

var bibliotecaService =
    new BibliotecaService(
        livroRepository,
        emprestimoRepository,
        emailService,
        logService,
        notificacaoService);
```

Começa a crescer rápido.
Com DI (Injeção de Dependência):

```csharp 
services.AddScoped<IEmailService, EmailService>();

services.AddScoped<ILogService, LogService>();

services.AddScoped<INotificacaoService,
                   NotificacaoService>();
```

e continua:

```csharp 
var menu =
    provider.GetRequiredService<Menu>();
```

O container resolve tudo sozinho.

---

Outro ponto importante: sem DI cria-se acoplamento forte.

Exemplo ruim:
```csharp 
public class BibliotecaService
{
    private LivroRepository _repository =
        new LivroRepository();
}
```
Problemas:

- difícil testar;
- difícil trocar implementação;
- dependência fixa.

Com DI:
```csharp 
public class BibliotecaService
{
    private readonly ILivroRepository _repository;

    public BibliotecaService(
        ILivroRepository repository)
    {
        _repository = repository;
    }
}
```
Agora você pode trocar:
```csharp 
LivroRepository
```

por: 
```csharp 
LivroFakeRepository
```

ou:
```csharp 
LivroMemoryRepository
```

sem alterar **BibliotecaService**

---

Para um projeto pequeno:
``` 
new AppDbContext()
new Repository()
new Service()
new Menu()
```
é totalmente aceitável, mas para APIs e projetos maiores **ServiceCollection** é ideal.

O **ServiceCollection** existe para evitar que o **Program.cs** fique cheio de *news*