public class LivroRepository : ILivroRepository
{
    private readonly AppDbContext _context;

    public LivroRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AdicionarLivro(Livro livro)
    {
        _context.Livros.Add(livro);
        _context.SaveChanges();
    }


    public Livro? ObterLivroPorCodigo(int codigo)
    {
        // Antes estava com First, mas para evitar exceções caso não encontre o livro, utilizei FirstOrDefault
        return _context.Livros.FirstOrDefault(l => l.CodigoLivro == codigo); 
    }

    public Livro ObterLivroPorId(int id)
    {
        // Utilizando First para garantir que um livro será retornado, assumindo que o ID é único e sempre existirá
        return _context.Livros.First(l => l.Id == id);
    }

    public List<Livro> ObterTodosLivros()
    {
        return _context.Livros.ToList();
    }

    public void AtualizarLivro(Livro livro)
    {
        _context.Livros.Update(livro);
        _context.SaveChanges();
    }

    public void RemoverLivro(int id)
    {
        var livro = _context.Livros.Find(id);
        if (livro != null)
        {
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }
    }
}