using APIBiblioteca.Data;
using APIBiblioteca.Service;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca
{
    public class Program
    {
        // Prepara as configurações e os serviços, monta a aplicação e inicia o atendimento HTTP.
        // O Swagger fica disponível apenas em desenvolvimento.
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura a conexão com o banco.
            builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registra as Services utilizadas pelos Controllers.
            builder.Services.AddScoped<EmprestimoService>();
            builder.Services.AddScoped<LivroService>();


            // Habilita os Controllers.
            builder.Services.AddControllers();

            // Configura a documentação da API.
            builder.Services.AddSwaggerGen();

            // Constrói a aplicação.
            var app = builder.Build();

            // Disponibiliza o Swagger em desenvolvimento.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // Redireciona as requisições para HTTPS.
            app.UseHttpsRedirection();

            app.UseAuthorization();


            // Disponibiliza as rotas dos Controllers.
            app.MapControllers();

            // Inicia o atendimento das requisições.
            app.Run();
        }
    }
}
