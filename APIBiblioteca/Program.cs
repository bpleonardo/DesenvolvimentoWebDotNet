using APIBiblioteca.Data;
using APIBiblioteca.Service;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Registrando o Serviço da ApiDbContext
            builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<LivroService>();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(); //Configuração do Swagger

            var app = builder.Build();

            //Se estiver em ambiente de dev, habilita o Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); //Configuração do Swagger
                app.UseSwaggerUI(); //Configuração do Swagger
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
