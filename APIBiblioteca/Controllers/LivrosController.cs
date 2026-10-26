using APIBiblioteca.Models;
using APIBiblioteca.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBiblioteca.Controllers
{
    // [controller] corresponde a Livros. ApiController ativa a validação automática.
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivroService _livroService;

        // Recebe a Service por injeção de dependência e guarda a referência para as requisições.
        public LivrosController(LivroService livroservice)
        {
            _livroService = livroservice;
        }

        [HttpGet]
        // GET api/Livros: consulta a Service e retorna HTTP 200, inclusive para uma lista vazia.
        // Task indica a operação assíncrona; ActionResult permite devolver dados ou erros HTTP.
        // Falhas são convertidas em HTTP 500 com uma mensagem, sem expor a exceção ao cliente.
        public async Task<ActionResult<List<Livro>>> ListarAsync()
        {
            try
            {
                // Consulta a camada de serviço.
                var livros = await _livroService.ListarAsync();
                return Ok(livros);
            }
            // Captura a falha, mas retorna uma mensagem fixa definida pelo Controller.
            catch
            {
                return StatusCode(500, new
                {
                    mensagem = "Não foi possível consultar os livros (Controller)"
                });
            }
        }

        [HttpPost]
        // POST api/Livros: FromBody recebe o livro do JSON e a Service realiza o cadastro.
        // Retorna HTTP 201 com o livro salvo; ActionResult permite também respostas de erro.
        // InvalidOperationException da Service é convertida em HTTP 500 com uma mensagem.
        public async Task<ActionResult<Livro>> Inserir([FromBody] Livro livro)
        {
            try
            {
                var livroCriado = await _livroService.InserirAsync(livro);

                return StatusCode(201, livroCriado);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    // A resposta usa um texto fixo; ex.Message não é utilizado aqui.
                    mensagem = "Não foi possível Inserir"
                });
            }
        }


        [HttpGet("{id:int}")]
        // GET api/Livros/{id}: retorna HTTP 200 com o livro.
        // Ausência resulta em HTTP 404; InvalidOperationException resulta em HTTP 500.
        // A Service lança a exceção com uma mensagem; o catch a captura em ex.
        // ex.Message é enviado no campo mensagem do JSON da resposta.
        public async Task<ActionResult<Livro>> ObterPorId(int id)
        {
            try
            {
                var livro = await _livroService.ObterPorIdAsync(id);
                return Ok(livro);
            }
            catch (KeyNotFoundException ex)
            {
                // Devolve a mensagem de livro não encontrado lançada pela Service.
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Devolve a mensagem da falha de operação lançada pela Service.
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        // PUT api/Livros/{id}: recebe os dados no corpo e solicita a atualização à Service.
        // Retorna HTTP 200 com o livro, 404 quando ausente ou 500 para InvalidOperationException.
        // O catch captura a exceção da Service; ex.Message fornece o texto devolvido no JSON.
        public async Task<ActionResult<Livro>> Atualizar(int id, [FromBody] Livro dados)
        {
            try
            {
                var livro = await _livroService.AtualizarAsync(id, dados);
                return Ok(livro);
            }
            catch (KeyNotFoundException ex)
            {
                // Devolve a mensagem de livro não encontrado lançada pela Service.
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Devolve a mensagem da falha de operação lançada pela Service.
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        // DELETE api/Livros/{id}: solicita a exclusão e retorna HTTP 204 sem conteúdo.
        // Retorna HTTP 404 quando ausente ou 500 para InvalidOperationException.
        // O catch captura a exceção da Service; ex.Message fornece o texto devolvido no JSON.
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _livroService.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Devolve a mensagem de livro não encontrado lançada pela Service.
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Devolve a mensagem da falha de operação lançada pela Service.
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }
    }
}
