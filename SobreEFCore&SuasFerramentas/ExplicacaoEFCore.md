# Por que preciso instalar três pacotes no EF Core?

Quando usamos o **Entity Framework Core (EF Core)**, muitas vezes vemos a instalação de três pacotes:

* `Microsoft.EntityFrameworkCore.SqlServer` (ou outro provedor)
* `Microsoft.EntityFrameworkCore.Tools`
* `Microsoft.EntityFrameworkCore.Design`

À primeira vista pode parecer redundante, mas cada pacote possui uma responsabilidade específica. O EF Core foi dividido dessa forma para evitar que aplicações carreguem ferramentas de desenvolvimento desnecessárias em produção.

---

## 1. Provedor do banco de dados

Exemplo:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Esse pacote é responsável pela comunicação entre o EF Core e o banco de dados escolhido.

Funções:

* Conectar ao banco;
* Traduzir consultas LINQ para SQL específico;
* Executar operações de leitura e escrita.

Sem esse pacote, o EF Core não sabe "falar a língua" do banco.

Exemplos de provedores:

* SQL Server → `Microsoft.EntityFrameworkCore.SqlServer`
* PostgreSQL → `Npgsql.EntityFrameworkCore.PostgreSQL`
* MySQL → `Pomelo.EntityFrameworkCore.MySql`
* SQLite → `Microsoft.EntityFrameworkCore.Sqlite`

---

## 2. EF Tools

Exemplo:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

Esse pacote adiciona ferramentas usadas durante o desenvolvimento.

Funções:

* Criar migrations;
* Atualizar banco de dados;
* Gerenciar comandos do EF.

Exemplos:

### Package Manager Console

```powershell
Add-Migration Inicial
Update-Database
```

### CLI (.NET)

```bash
dotnet ef migrations add Inicial
dotnet ef database update
```

Sem esse pacote, esses comandos não ficam disponíveis.

---

## 3. EF Design

Exemplo:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Esse pacote contém serviços utilizados apenas durante o processo de desenvolvimento e geração automática de código.

Funções:

* Descobrir o `DbContext`;
* Gerar migrations;
* Criar snapshots do modelo;
* Fazer scaffold do banco:

```bash
Scaffold-DbContext
```

Normalmente ele aparece com:

```xml
<PrivateAssets>all</PrivateAssets>
```

Isso significa que o pacote será usado apenas durante o desenvolvimento e não será incluído em produção.

---

## Instalação comum para SQL Server

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## Nem sempre os três são necessários

Dependendo do projeto:

### Apenas testes em memória

Pode precisar apenas:

```bash
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

### Não utiliza migrations

Talvez não seja necessário instalar:

* `Tools`

### Projeto apenas consome migrations já criadas

Pode não precisar instalar todos os pacotes.

---

## Analogia simples

Imagine o EF Core como um carro:

* **SqlServer** → o motorista que sabe dirigir aquele tipo específico de carro;
* **Tools** → a caixa de ferramentas;
* **Design** → a oficina que cria peças automaticamente.

A aplicação precisa do motorista para funcionar.

As ferramentas e a oficina são usadas principalmente durante o desenvolvimento.
