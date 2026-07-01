public interface IEmprestimoRepository
{   
    List<Emprestimo> ListarEmprestimosAbertos();
    void RealizarEmprestimo(Emprestimo emprestimo);
    void DevolverLivro(int livroId, string nomeUsuario);
}