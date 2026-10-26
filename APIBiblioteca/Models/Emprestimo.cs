namespace APIBiblioteca.Models
{
    // Representa o empréstimo de um livro a um usuário.
    // Mantém os identificadores envolvidos e as datas de criação e devolução.
    public class Emprestimo
    {
        // Identificador do empréstimo.
        public int Id { get; set; }
        // Usuário responsável pelo empréstimo.
        public int UsuarioId { get; set; }
        // Livro emprestado.
        public int LivroId { get; set; }
        // Data em que o empréstimo foi registrado.
        public DateTime DataCriacao { get; set; }
        // Sem valor enquanto a devolução estiver em aberto.
        public DateTime? DataDevolucao { get; set; }
    }
}
