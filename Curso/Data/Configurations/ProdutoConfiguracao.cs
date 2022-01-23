using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class ProdutoConfiguracao : IEntityTypeConfiguration­<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(product => product.Id);
            builder.Property(product => product.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(product => product.Descricao).HasColumnType("VARCHAR(14)");
            builder.Property(product => product.Valor).IsRequired();
            builder.Property(product => product.TipoProduto).HasConversion<string>();
        }
    }
}