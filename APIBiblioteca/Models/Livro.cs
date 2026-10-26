using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Models
{
    // Representa um livro e sua disponibilidade para empréstimo.
    // Título e autor são obrigatórios, com limites de 150 e 100 caracteres.
    // As strings começam vazias para evitar valores nulos.
    public class Livro
    {
        // Identificador do livro.
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        // Título do livro.
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        // Autor do livro.
        public  string Autor { get; set; } = string.Empty;

        // true indica emprestado; false indica disponível.
        public bool Emprestado { get; set; }

    }
}
