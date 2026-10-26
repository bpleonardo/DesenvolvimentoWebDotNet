using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Dto
{
    // Representa os dados recebidos para solicitar um empréstimo.
    // Os dois identificadores devem ser inteiros positivos.
    public class RealizarEmprestimoDto
    {
        [Range(1, int.MaxValue)]
        // Usuário que solicita o empréstimo.
        public int UsuarioId { get; set; }

        [Range(1, int.MaxValue)]
        // Livro solicitado.
        public int LivroId { get; set; }
    }
}
