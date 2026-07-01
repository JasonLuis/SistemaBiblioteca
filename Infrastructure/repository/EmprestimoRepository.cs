using Microsoft.EntityFrameworkCore;

public class EmprestimoRepository : IEmprestimoRepository
{

    private readonly AppDbContext _context;

    public EmprestimoRepository(AppDbContext context)
    {
        _context = context;
    }

    public void DevolverLivro(int livroId, string nomeUsuario)
    {
        var emprestimo = _context.Emprestimos.FirstOrDefault(e => e.LivroId == livroId && e.NomeUsuario == nomeUsuario && e.DataDevolucao == null);

        if (emprestimo != null)
        {
            emprestimo.DataDevolucao = DateTime.Now;
            _context.SaveChanges();
        }
    }

    public List<Emprestimo> ListarEmprestimosAbertos()
    {
        return _context.Emprestimos
                       .Include(e => e.Livro)
                       .Where(e => e.DataDevolucao == null)
                       .ToList();
    }

    public void RealizarEmprestimo(Emprestimo emprestimo)
    {
        _context.Emprestimos.Add(emprestimo);
        _context.SaveChanges();
    }
}