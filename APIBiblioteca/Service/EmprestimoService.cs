using APIBiblioteca.Data;
using APIBiblioteca.Dto;
using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Service
{
    public class EmprestimoService
    {
        private readonly ApiDbContext _context;

        // Recebe a dependência de acesso aos dados pelo construtor.
        public EmprestimoService(ApiDbContext context)
        {
            _context = context;
        }

        // Realiza o empréstimo após verificar usuário, livro e disponibilidade.
        // Registra a data em UTC, deixa a devolução em aberto e marca o livro como emprestado.
        // Retorna o empréstimo salvo. Ausências e indisponibilidade são comunicadas por exceções;
        // uma falha de gravação preserva a exceção original para investigação.
        public async Task<Emprestimo> RealizarAsync(RealizarEmprestimoDto dados)
        {
            // Verifica se o usuário existe.
            var usuario = await _context.Usuarios.FindAsync(dados.UsuarioId);

            if (usuario is null)
            {
                throw new KeyNotFoundException(
                    $"Usuario com ID {dados.UsuarioId} nao encontrado.");
            }
            // Verifica se o livro existe.
            var livro = await _context.Livros.FindAsync(dados.LivroId);
            if (livro is null)
            {
                throw new KeyNotFoundException(
                    $"Livro com ID {dados.LivroId} nao encontrado.");
            }
            // Impede um novo empréstimo de um livro indisponível.
            if (livro.Emprestado)
            {
                throw new ArgumentException("O livro ja esta emprestado.");
            }

            // Prepara os dados do empréstimo.
            var emprestimo = new Emprestimo
            {
                UsuarioId = usuario.Id,
                LivroId = livro.Id,
                DataCriacao = DateTime.UtcNow,
                DataDevolucao = null
            };
            _context.Emprestimos.Add(emprestimo);
            // Atualiza a disponibilidade do livro.
            livro.Emprestado = true;
            try
            {
                // Grava o empréstimo e a alteração do livro.
                await _context.SaveChangesAsync();
                return emprestimo;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException(
                    "Nao foi possivel realizar o emprestimo.", ex);
            }
        }
    }
}
