using APIBiblioteca.Data;
using APIBiblioteca.Dto;
using APIBiblioteca.Models;
using APIBiblioteca.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBiblioteca.Controllers
{
    // Define o endereço base api/Emprestimo.
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoService _emprestimoService;

        // Recebe a Service responsável pelas regras de empréstimo.
        public EmprestimoController(EmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpPost]
        // POST api/Emprestimo: recebe os IDs pelo corpo JSON e solicita o empréstimo.
        // Retorna 201 com o registro criado, 404 se usuário ou livro não existir,
        // 400 quando o livro estiver indisponível e 500 para a falha de operação tratada.
        // A Service lança a exceção com uma mensagem; o catch correspondente a captura em ex.
        // ex.Message contém esse texto, enviado no campo mensagem do JSON da resposta.
        public async Task<ActionResult<Emprestimo>> Realizar([FromBody] RealizarEmprestimoDto dados)
        {
            try
            {
                // Encaminha a solicitação à Service.
                var emprestimo = await _emprestimoService.RealizarAsync(dados);
                // Confirma a criação.
                return StatusCode(201, emprestimo);
            }
            catch (KeyNotFoundException ex)
            {
                // Devolve a mensagem de usuário ou livro não encontrado.
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // Devolve a mensagem da regra que impediu o empréstimo.
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Devolve a mensagem da falha de operação lançada pela Service.
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }
    }
}
