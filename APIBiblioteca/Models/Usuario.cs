namespace APIBiblioteca.Models
{
    // Representa o usuário da biblioteca e seus dados de identificação e contato.
    public class Usuario
    {
        // Identificador do usuário.
        public int Id { get; set; }
        // Nome do usuário.
        public string Nome { get; set; }
        // E-mail de contato.
        public string Email { get; set; }

    }
}
