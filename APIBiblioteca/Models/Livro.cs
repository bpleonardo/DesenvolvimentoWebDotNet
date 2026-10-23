using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Models
{
    // Representa um livro da biblioteca e seu estado de empréstimo.
    public class Livro
    {
        // Identificador único do livro.
        public int Id { get; set; }

        // Título obrigatório do livro, limitado a 150 caracteres.
        // Inicializado com uma string vazia para evitar um valor nulo.
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        // Nome obrigatório do autor, limitado a 100 caracteres.
        // Inicializado com uma string vazia para evitar um valor nulo.
        [Required]
        [StringLength(100)]
        public  string Autor { get; set; } = string.Empty;

        // Indica se o livro está emprestado: true para emprestado e false para disponível.
        // O valor padrão é false.
        public bool Emprestado { get; set; }

    }
}
