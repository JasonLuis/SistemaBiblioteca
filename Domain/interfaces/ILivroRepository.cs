public interface ILivroRepository
{
    void AdicionarLivro(Livro livro);
    Livro? ObterLivroPorCodigo(int codigo);

    Livro ObterLivroPorId(int id);
    List<Livro> ObterTodosLivros();
    void AtualizarLivro(Livro livro);
    void RemoverLivro(int id);
}