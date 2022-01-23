using System;
using System.Collections.Generic;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // AddMultipleData();
            // addProducts(); 
            // ConsultarDatos();
            // AdicionarPedito();   
            // ConsultaPedidos();
            // AtualizarCliente();

            // AdicionarCliente();
            RemoverCliente();
        }

        private static void RemoverCliente()
        {
            using var db = new Data.ApplicationContext();
            
            // this strategy consults the database first
            // var cliente = db.Clientes.Find(1);

            // this strategy does not need to consult the database first
            var cliente = new Cliente {Id = 2};
            db.Attach(cliente); // track client with Entity Framework

            // db.Clientes.Remove(cliente); // Strategy 1
            // db.Remove(cliente); // Strategy 2
            db.Entry(cliente).State = EntityState.Deleted; // strategy 3

            db.SaveChanges();

            Console.WriteLine(cliente);
        }

        private static void AdicionarCliente()
        {
            using var db = new Data.ApplicationContext();
            var cliente = new Cliente()
            {
                Nome = "Beltrano",
                Telefone = "8199999999",
                CEP = "54410235",
                Cidade = "Somewhere",
                Estado = "PE",
            };

            var cliente2 = new Cliente()
            {
                Nome = "Siclano",
                Telefone = "8199999999",
                CEP = "54410235",
                Cidade = "Somewhere",
                Estado = "PE",
            };

            db.AddRange(cliente, cliente2);

            db.SaveChanges();

            Console.WriteLine(db.Clientes.ToList().Count);
        }

        private static void AtualizarCliente()
        {
            using var db = new Data.ApplicationContext();
            // var cliente = db.Clientes.Find(1); // consulta o usuário 1 no banco
            // cliente.Nome = "Rocky Balboa";

            // este método atualiza todos os campos, mesmo os nao alterados
            // db.Update(cliente); // este método nao é recomendado se nem todos os propriedades estao sendo atualizadas

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectato = new
            {
                Nome = "Arnold Balboa ",
                Telefone = "8197579967"
            };

            db.Attach(cliente); // adiciona o objeto para ser trackeado pelo entity framework
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectato); // it sets the object clientDesconectado para o cliente com o Id 1͘͘͘
            // Para alterar apenas os campos que foreram algum tipo de alteracao, basta salver as mudancas
            db.SaveChanges();

            Console.WriteLine(cliente.Id);
        }

        private static void ConsultaPedidos()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
            .Include(_pedido => _pedido.Itens)
            .ThenInclude(item => item.Produto)
            .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void AdicionarPedito()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido()
            {
                ClientId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidate = 1,
                        Valor = 10
                    }
                }
            };

            db.Add(pedido);
            db.SaveChanges();

        }
        private static void ConsultarDatos()
        {
            using var db = new Data.ApplicationContext();
            // var consultaPorSintaxe = (from c in db.Clientes where c.Id>0 select c).ToList();
            var consultaPorMetodo = db.Clientes
            .Where(c => c.Id > 0)
            .OrderBy(c => c.Id)
            .ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consulta por cliente com id: {cliente.Id}");
                // db.Clientes.Find(cliente.Id); consulta o resultado em memoria antes de consultar no banco de dados
                db.Clientes.FirstOrDefault(c => c.Id == cliente.Id);
            }
        }

        private static void addProducts()
        {
            var produto = new Produto()
            {
                Descricao = "Produto 1",
                CodigoBarras = "1234567890",
                Valor = 10,
                TipoProduto = TipoProduto.MecadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);
            db.Set<Produto>().Add(produto);
            db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine($"Adicionando um produto ao banco de dados. Resposta => {registros}");
        }

        private static void AddMultipleData()
        {
            var produto = new Produto()
            {
                Descricao = "Produto 3",
                CodigoBarras = "1234567892",
                Valor = 20,
                TipoProduto = TipoProduto.MecadoriaParaRevenda,
                Ativo = true
            };

            var client = new Cliente()
            {
                Nome = "Ricardo Ferreira",
                Telefone = "11111111111",
                CEP = "54410235",
                Cidade = "Itabaiana",
                Estado = "PE",

            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, client);

            var registros = db.SaveChanges();

            Console.WriteLine($"Foram adicionados {registros}");
        }
    }
}
