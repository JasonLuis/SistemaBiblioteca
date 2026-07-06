public class Menu
{
    public readonly BibliotecaService _bibliotecaService;

    public Menu(BibliotecaService bibliotecaService)
    {
        _bibliotecaService = bibliotecaService;
    }

    public void ExibirMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(@"
    ▗▄▄▖ ▗▄▄▄▖▗▄▄▖ ▗▖   ▗▄▄▄▖ ▗▄▖▗▄▄▄▖▗▄▄▄▖ ▗▄▄▖ ▗▄▖     
    ▐▌ ▐▌  █  ▐▌ ▐▌▐▌     █  ▐▌ ▐▌ █  ▐▌   ▐▌   ▐▌ ▐▌    
    ▐▛▀▚▖  █  ▐▛▀▚▖▐▌     █  ▐▌ ▐▌ █  ▐▛▀▀▘▐▌   ▐▛▀▜▌    
    ▐▙▄▞▘▗▄█▄▖▐▙▄▞▘▐▙▄▄▖▗▄█▄▖▝▚▄▞▘ █  ▐▙▄▄▖▝▚▄▄▖▐▌ ▐▌    
        ");
            Console.WriteLine("=== Sistema de Biblioteca ===");
            Console.WriteLine("1. Adicionar Livro");
            Console.WriteLine("2. Listar Livros");
            Console.WriteLine("3. Atualizar Livro");
            Console.WriteLine("4. Realizar Empréstimo");
            Console.WriteLine("5. Listar Empréstimos Abertos");
            Console.WriteLine("6. Devolver Livro");
            Console.WriteLine("7. Sair");
            Console.Write("Escolha uma opção: ");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AdicionarLivro();
                    break;
                case "2":
                    ListarLivros();
                    break;
                case "3":
                    AtualizarLivro();
                    break;
                case "4":
                    RealizarEmprestimo();
                    break;
                case "5":
                    ListarEmprestimosAbertos();
                    break;
                case "6":
                    DevolverLivro();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    Pause();
                    break;
            }
        }
    }

    private void AdicionarLivro()
    {
        Console.Clear();
        Console.Write("Digite o título do livro: ");
        var titulo = Console.ReadLine();
        Console.Write("Digite o autor do livro: ");
        var autor = Console.ReadLine();

        _bibliotecaService.AdicionarLivro(titulo, autor);
        Console.WriteLine("Livro adicionado com sucesso!");
        Pause();
    }

    private void ListarLivros()
    {
        Console.Clear();
        
        var livros = _bibliotecaService.ListarLivros();
        livros.ForEach(l =>
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Código: {l.CodigoLivro} \nTítulo: {l.Titulo} \nAutor: {l.Autor} \nDisponível: {(l.Disponivel ? "Sim" : "Não")}");
            Console.WriteLine("--------------------------------------------------");                    
        });
        Pause();
    }

    private void AtualizarLivro()
    {
        Console.Clear();
        var codigo = LerCodigoLivro();

        var livro = _bibliotecaService.ObterLivroPorCodigo(codigo);
        if (livro == null)
        {
            Console.WriteLine("Livros não encontrados.");
            Pause();
            return;
        }

        Console.Write("Digite o novo título do livro: ");
        var novoTitulo = Console.ReadLine();
        Console.Write("Digite o novo autor do livro: ");
        var novoAutor = Console.ReadLine();

        _bibliotecaService.AtualizarLivro(livro.Id, novoTitulo, novoAutor);
        Console.WriteLine("Livro atualizado com sucesso!");
        Pause();
    }

    private void RealizarEmprestimo()
    {
        Console.Clear();
        var codigo = LerCodigoLivro();
        var livro = _bibliotecaService.ObterLivroPorCodigo(codigo);
        if (livro == null)
        {
            Console.WriteLine("Livros não encontrados.");
            Pause();
            return;
        }
        else if (!livro.Disponivel)
        {
            Console.WriteLine("Livro não disponível para empréstimo.");
            Pause();
            return;
        }

        Console.Write("Digite o nome do usuário: ");
        var nomeUsuario = Console.ReadLine();
        _bibliotecaService.RealizarEmprestimo(codigo, nomeUsuario);

        Console.WriteLine("Empréstimo realizado com sucesso!");
        Pause();
    }

    private void ListarEmprestimosAbertos()
    {
        Console.Clear();
        var emprestimos = _bibliotecaService.ListarEmprestimosAbertos();
        if (emprestimos.Count == 0)
        {
            Console.WriteLine("Nenhum empréstimo aberto."); 
            Pause();
            return;
        }

        emprestimos.ForEach(e =>
        {
            var livro = _bibliotecaService.ObterLivroPorCodigo(e.Livro.CodigoLivro);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Código do Livro: {livro.CodigoLivro} \nTítulo: {livro.Titulo} \nAutor: {livro.Autor} \nNome do Usuário: {e.NomeUsuario} \nData do Empréstimo: {e.DataEmprestimo}");
            Console.WriteLine("--------------------------------------------------");
        });

        Pause();
    }

    private void DevolverLivro()
    {
        Console.Clear();
        var codigo = LerCodigoLivro();
        var livro = _bibliotecaService.ObterLivroPorCodigo(codigo);
        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado.");
            Pause();
            return;
        }
        else if (livro.Disponivel)
        {
            Console.WriteLine("Este livro não está emprestado.");
            Pause();
            return;
        }

        Console.Write("Digite o nome do usuário que está devolvendo o livro: ");
        var nomeUsuario = Console.ReadLine();

        try
        {
            _bibliotecaService.DevolverLivro(codigo, nomeUsuario);
            Console.WriteLine("Livro devolvido com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao devolver o livro: {ex.Message}");
        }

        Pause();
    }
    private static void Pause()
    {
        Console.WriteLine("\nPressione qualquer tecla...");
        Console.ReadLine();
    }

    private static int LerCodigoLivro()
    {
        while (true)
        {
            Console.Write("Digite o código do livro: ");
            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                return codigo;
            }
            else
            {
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }
        }
    }
}