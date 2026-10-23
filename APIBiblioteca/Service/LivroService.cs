using APIBiblioteca.Data;
using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Service
{
    // Centraliza as regras de negócio relacionadas aos livros e coordena o acesso aos dados.
    public class LivroService
    {
        // O contexto representa a sessão de acesso ao banco gerenciada pelo Entity Framework.
        // Por meio dele, a service consulta os livros e salva alterações.
        // readonly impede que este campo receba outro contexto após a construção da service.
        private readonly ApiDbContext _context;

        // O ASP.NET Core fornece o contexto pelo construtor usando injeção de dependência.
        // A referência recebida fica em _context para ser usada pelos métodos da classe.
        public LivroService(ApiDbContext context)
        {
            _context = context;
        }

        // Busca todos os livros cadastrados no banco de dados.
        // Task<List<Livro>> representa uma operação assíncrona cujo resultado é uma lista de livros.
        // Se não houver registros, o resultado será uma lista vazia.
        public async Task<List<Livro>> ListarAsync()
        {
            try
            {
                // _context.Livros dá acesso ao conjunto de livros mapeado pelo Entity Framework.
                // ToListAsync executa a consulta no banco e transforma os registros em uma List<Livro>.
                // O await aguarda essa operação sem manter a thread bloqueada durante a espera.
                // Quando a consulta termina, return entrega a lista para quem chamou o método.
                return await _context.Livros.ToListAsync();
            }
            // Se ocorrer uma falha no bloco try, como um erro de conexão, o catch captura a exceção.
            // Exception permite capturar diferentes tipos de erro, armazenados na variável ex.
            catch (Exception ex)
            {
                // Lança uma nova exceção com uma mensagem que identifica a operação que falhou.
                // O segundo argumento, ex, preserva a exceção original como InnerException,
                // permitindo investigar a causa. A falha é propagada para quem chamou o método.
                throw new InvalidOperationException(
                    "Não foi possível consultar os livros (Service).", ex);
            }
        }

        // Recebe o livro a cadastrar e devolve o objeto após a gravação no banco.
        // Task<Livro> representa uma operação assíncrona cujo resultado é um Livro.
        public async Task<Livro> InserirAsync(Livro livro)
        {
            // Add faz o Entity Framework acompanhar o objeto com o estado Added (novo registro).
            // Isso prepara a inserção; o comando só será enviado ao banco no SaveChangesAsync.
            _context.Livros.Add(livro);

            try
            {
                // SaveChangesAsync envia ao banco as alterações pendentes acompanhadas pelo contexto.
                // O await aguarda a gravação sem manter a thread bloqueada durante a espera.
                await _context.SaveChangesAsync();
                // Retorna o objeto salvo, incluindo valores gerados pelo banco, como o Id.
                return livro;
            }
            // DbUpdateException captura falhas do Entity Framework ao gravar os dados,
            // como uma violação de restrição no banco. Outros tipos de exceção não entram neste catch.
            catch (DbUpdateException ex)
            {
                // Informa qual operação falhou e preserva ex como InnerException.
                // Quem chamou a service recebe essa exceção e pode tratar a falha.
                throw new InvalidOperationException(
                "Não foi possível cadastrar o livro.", ex);
            }
        }
    }
}
