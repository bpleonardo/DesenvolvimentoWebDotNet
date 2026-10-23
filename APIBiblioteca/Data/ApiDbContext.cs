using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace APIBiblioteca.Data
{
    // Representa a sessão de acesso ao banco de dados da aplicação.
    // Ao herdar de DbContext, permite consultar entidades, acompanhar alterações
    // e gravá-las no banco usando os recursos do Entity Framework Core.
    public class ApiDbContext : DbContext
    {
        // Recebe as opções configuradas para este contexto por injeção de dependência.
        // Essas opções podem definir o provedor do banco e a conexão utilizada.
        // base(options) repassa a configuração ao construtor da classe DbContext.
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
        {
        }

        // Expõe o conjunto de entidades Livro para realizar consultas e preparar alterações.
        // Set<Livro>() obtém esse conjunto no contexto; a expressão => define o acesso à propriedade.
        // Acessar Livros não carrega todos os registros: a consulta é executada ao solicitar
        // os resultados, por exemplo com ToListAsync. Alterações são gravadas com SaveChangesAsync.
        public DbSet<Livro> Livros => Set<Livro>();


        // Sobrescreve OnModelCreating para configurar o seeding: os dados iniciais da aplicação.
        // Os livros definidos aqui serão adicionados ao banco quando for aplicada a migration
        // gerada com essa configuração de dados.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Executa primeiro a configuração da classe base antes de adicionar a desta aplicação.
            base.OnModelCreating(modelBuilder);

            // Livros que serão usados no seeding do banco de dados.
            Livro l1 = new()
            {
                Id = 1,
                Titulo = "Dom Casmurro",
                Autor = "Machado de Assis",
                Emprestado = false
            };

            Livro l2 = new()
            {
                Id = 2,
                Titulo = "O Pequeno Principe",
                Autor = "Antoine de Saint-Exupery",
                Emprestado = false
            };

            // Entity<Livro>() seleciona a entidade Livro no modelo para configurá-la.
            // HasData(l1, l2) recebe os objetos que serão usados como dados iniciais dessa entidade.
            modelBuilder.Entity<Livro>().HasData(l1, l2);
        }
    }
}
