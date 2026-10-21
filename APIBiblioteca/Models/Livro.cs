using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public  string Autor { get; set; } = string.Empty;

        public bool Emprestado { get; set; }

    }
}
