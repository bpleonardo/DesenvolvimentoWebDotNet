using APIBiblioteca.Models;
using APIBiblioteca.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBiblioteca.Controllers
{
    // Recebe requisições HTTP, encaminha as operações à service e monta as respostas da API.
    // [controller] é substituído pelo nome da classe sem o sufixo Controller: api/Livros.
    [Route("api/[controller]")]
    // Ativa comportamentos de API, como retornar 400 automaticamente para um modelo inválido.
    [ApiController]
    // ControllerBase oferece recursos como Ok e StatusCode para criar respostas HTTP.
    public class LivrosController : ControllerBase
    {
        // Guarda a service que coordena as operações relacionadas aos livros.
        // readonly impede trocar essa referência após a construção do controller.
        private readonly LivroService _livroService;

        // O ASP.NET Core fornece a service por injeção de dependência.
        // O controller guarda a referência para utilizá-la ao atender as requisições.
        public LivrosController(LivroService livroservice)
        {
            _livroService = livroservice;
        }

        // Atende requisições GET em api/Livros para consultar os livros cadastrados.
        // ActionResult<List<Livro>> permite retornar a lista ou uma resposta HTTP de erro.
        // Task indica que o resultado será produzido por uma operação assíncrona.
        [HttpGet]
        public async Task<ActionResult<List<Livro>>> ListarAsync()
        {
            try
            {
                // Delega a consulta à service e aguarda o resultado sem bloquear a thread na espera.
                var livros = await _livroService.ListarAsync();
                // Ok gera uma resposta 200 com a lista no corpo, inclusive se ela estiver vazia.
                return Ok(livros);
            }
            // Captura falhas do bloco try, incluindo as exceções propagadas pela service.
            catch
            {
                // Retorna 500 (erro interno) com um objeto que contém a mensagem para o cliente.
                // Os detalhes da exceção não são incluídos no corpo da resposta.
                return StatusCode(500, new
                {
                    mensagem = "Não foi possível consultar os livros (Controller)"
                });
            }
        }

        // Atende requisições POST em api/Livros para cadastrar um novo livro.
        // [FromBody] indica que os dados de Livro vêm do corpo da requisição, normalmente em JSON.
        // ActionResult<Livro> permite devolver o livro criado ou uma resposta HTTP de erro.
        [HttpPost]
        public async Task<ActionResult<Livro>> Inserir([FromBody] Livro livro)
        {
            try
            {
                // Envia o livro à service e aguarda a conclusão da gravação no banco.
                var livroCriado = await _livroService.InserirAsync(livro);

                // Retorna 201 (Created) para indicar a criação, com o livro salvo no corpo da resposta.
                return StatusCode(201, livroCriado);
            }
            // Trata InvalidOperationException, usada pela service para comunicar uma falha no cadastro.
            // Outros tipos de exceção não são capturados por este bloco.
            catch (InvalidOperationException ex)
            {
                // Retorna 500 com uma mensagem para o cliente. A variável ex não é usada aqui,
                // portanto os detalhes da exceção não aparecem nesta resposta.
                return StatusCode(500, new
                {
                    mensagem = "Não foi possível Inserir"
                });
            }
        }
    }
}
