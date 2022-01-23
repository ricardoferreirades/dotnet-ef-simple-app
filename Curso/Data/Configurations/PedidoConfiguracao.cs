using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class PedidoConfiguracao : IEntityTypeConfiguration­<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(pedido => pedido.Id);
            builder.Property(pedido => pedido.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(pedido => pedido.Status).HasConversion<string>();
            builder.Property(pedido => pedido.TipoFrete).HasConversion<int>();
            builder.Property(pedido => pedido.Observacao).HasColumnType("VARCHAR(512)");

            builder.HasMany(pedido => pedido.Itens)
            .WithOne(pedido => pedido.Pedido)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}