using ExemploJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExemploJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly List<Usuario> _usuarios = new()
        {
            new Usuario
            {
                Id = 1,
                Email = "ana",
                Senha = "123",
                Role = "Usuario"
            },
            new Usuario
            {
                Id = 2,
                Email = "admin",
                Senha = "admin",
                Role = "Admin"
            }
        };
    }
}
