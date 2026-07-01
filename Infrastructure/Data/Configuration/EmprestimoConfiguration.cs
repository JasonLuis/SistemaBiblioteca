using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmprestimoConfiguration : IEntityTypeConfiguration<Emprestimo>
{
    public void Configure(EntityTypeBuilder<Emprestimo> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.NomeUsuario).IsRequired().HasMaxLength(100);
        builder.Property(e => e.DataEmprestimo).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(e => e.DataDevolucao).IsRequired(false);
        builder.HasOne(e => e.Livro)
               .WithMany()
               .HasForeignKey(e => e.LivroId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}