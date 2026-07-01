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
            Console.WriteLine("6. Sair");
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
                    
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    Pause();
                    break;
            }
        }
    }

    private void ListarEmprestimosAbertos()
    {
        Console.Clear();
    }

    private void RealizarEmprestimo()
    {
        Console.Clear();
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

    private static void Pause()
    {
        Console.WriteLine("\nPressione qualquer tecla...");
        Console.ReadLine();
    }
}