using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace APIBiblioteca.Data
{
    // Centraliza o acesso aos dados da aplicação.
    public class ApiDbContext : DbContext
    {
        // Recebe as configurações de acesso ao banco pelo construtor.
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
        {
        }

        // Conjuntos de dados utilizados pela aplicação.
        public DbSet<Livro> Livros => Set<Livro>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Emprestimo> Emprestimos => Set<Emprestimo>();


        // Define os dois livros e o usuário disponibilizados como dados iniciais.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Livros iniciais da biblioteca.
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

            modelBuilder.Entity<Livro>().HasData(l1, l2);

            // Usuário inicial para as operações de empréstimo.
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "Maria Silva",
                Email = "maria@email.com"
            };

            modelBuilder.Entity<Usuario>().HasData(usuario);
        }
    }
}
