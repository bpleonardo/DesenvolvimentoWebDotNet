using ExemploJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExemploJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private static readonly List<Produto> _produtos = new()
        {
            new Produto { Id = 1, Nome = "Teclado" },
            new Produto { Id = 2, Nome = "Mouse" },
            new Produto { Id = 3, Nome = "Monitor" }
        };

        [HttpGet("publico")]
        public IActionResult Publico()
        {
            return Ok(_produtos);
        }

        [HttpGet("autenticado")]
        public IActionResult Autenticado()
        {
            return Ok(new
            {
                mensagem = "Acesso autenticado",
                produtos = _produtos
            });
        }

        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok(new
            {
                mensagem = "Acesso administrativo",
                produtos = _produtos
            });
        }
    }
}
