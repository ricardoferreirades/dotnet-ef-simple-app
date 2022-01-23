using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class PedidoItemConfiguracao : IEntityTypeConfiguration­<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItems");
            builder.HasKey(orderItem => orderItem.Id);
            builder.Property(orderItem => orderItem.Quantidate).HasDefaultValue(1).IsRequired();
            builder.Property(orderItem => orderItem.Valor).IsRequired();
            builder.Property(orderItem => orderItem.Desconto).IsRequired();
        }
    }
}