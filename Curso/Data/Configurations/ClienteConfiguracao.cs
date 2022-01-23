using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class ClienteConfiguracao : IEntityTypeConfiguration­<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> cliente)
        {
            cliente.ToTable("Clients");
            cliente.HasKey(client => client.Id);
            cliente.Property(client => client.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            cliente.Property(client => client.Telefone).HasColumnType("CHAR(11)").IsRequired();
            cliente.Property(client => client.CEP).HasColumnType("CHAR(8)").IsRequired();
            cliente.Property(client => client.Estado).HasColumnType("CHAR(2)").IsRequired();
            cliente.Property(client => client.Cidade).HasMaxLength(60).IsRequired();

            cliente.HasIndex(client => client.Telefone).HasName("idx_client_telefone");
        }
    }
}