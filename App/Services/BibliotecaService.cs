public class BibliotecaService
{
    private readonly ILivroRepository _livroRepository;
    private readonly IEmprestimoRepository _emprestimoRepository;

    public BibliotecaService(ILivroRepository livroRepo, IEmprestimoRepository emprestimoRepo)
    {
        _livroRepository = livroRepo;
        _emprestimoRepository = emprestimoRepo;
    }

    public void AdicionarLivro(string titulo, string autor)
    {
        int codigo = new Random().Next(1000, 9999); // Gera um código aleatório para o livro
        var livro = new Livro { CodigoLivro = codigo, Titulo = titulo, Autor = autor, Disponivel = true };
        _livroRepository.AdicionarLivro(livro);
    }

    public List<Livro> ListarLivros()
    {
        return _livroRepository.ObterTodosLivros();
    }

    public Livro ObterLivroPorCodigo(int codigo)
    {
        return _livroRepository.ObterLivroPorCodigo(codigo);
    }

    public void AtualizarLivro(int id, string titulo, string autor)
    {
        var livro = _livroRepository.ObterLivroPorId(id);
        if (livro == null)
        {
            throw new Exception("Livro não encontrado.");
        }

        livro.Titulo = titulo;
        livro.Autor = autor;
        _livroRepository.AtualizarLivro(livro);
    }

    public void RealizarEmprestimo(int codigo, string nomeUsuario)
    {
        var livro = _livroRepository.ObterLivroPorCodigo(codigo);
        if (livro == null || !livro.Disponivel)
        {
            throw new Exception("Livro não disponível para empréstimo.");
        }

        var emprestimo = new Emprestimo
        {
            LivroId = livro.Id,
            NomeUsuario = nomeUsuario,
            DataEmprestimo = DateTime.Now
        };

        _emprestimoRepository.RealizarEmprestimo(emprestimo);
        livro.Disponivel = false;
        _livroRepository.AtualizarLivro(livro);
    }

    public List<Emprestimo> ListarEmprestimosAbertos()
    {
        return _emprestimoRepository.ListarEmprestimosAbertos();
    }

    public void DevolverLivro(int codigo, string nomeUsuario)
    {
        var livro = _livroRepository.ObterLivroPorCodigo(codigo);
        if (livro == null || livro.Disponivel)
        {
            throw new Exception("Livro não encontrado ou já devolvido.");
        }

        _emprestimoRepository.DevolverLivro(livro.Id, nomeUsuario);
        livro.Disponivel = true;
        _livroRepository.AtualizarLivro(livro);
    }
}