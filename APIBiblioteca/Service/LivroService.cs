using System;
using APIBiblioteca.Data;
using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Service
{
    public class LivroService
    {
        private readonly ApiDbContext _context;

        // Recebe a dependência de acesso aos dados pelo construtor.
        public LivroService(ApiDbContext context)
        {
            _context = context;
        }

        // Consulta os livros cadastrados e devolve uma lista, vazia quando não há registros.
        // O resultado é assíncrono. Se a operação falhar, comunica uma mensagem e preserva a causa.
        public async Task<List<Livro>> ListarAsync()
        {
            try
            {
                Console.WriteLine("Teste");
                
                return await _context.Livros.OrderBy(livro => livro.Id).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Não foi possível consultar os livros (Service).", ex);
            }
        }

        // Recebe um livro, solicita sua gravação e retorna os dados salvos, incluindo o ID.
        // O resultado é assíncrono; falhas de gravação preservam a causa original.
        public async Task<Livro> InserirAsync(Livro livro)
        {
            // Prepara o cadastro.
            _context.Livros.Add(livro);

            try
            {
                await _context.SaveChangesAsync();
                return livro;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException(
                "Não foi possível cadastrar o livro.", ex);
            }
        }

        // Busca o livro pelo ID e devolve seus dados.
        // Se não existir, comunica a ausência com KeyNotFoundException.
        public async Task<Livro> ObterPorIdAsync(int id)
        {
            try
            {
                var livro = await _context.Livros.FindAsync(id);
                if (livro is null)
                {
                    throw new KeyNotFoundException($"Livro com ID{id} não encontrado.");
                }
                return livro;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(
                "Não foi possível consultar o livro.", ex);
            }
        }

        // Localiza o livro, aplica título, autor e disponibilidade recebidos e salva as mudanças.
        // Comunica a ausência do registro ou uma falha de gravação, preservando a causa do erro.
        public async Task<Livro> AtualizarAsync(int id, Livro dados)
        {
            try
            {
                var livro = await _context.Livros.FindAsync(id);
                if (livro is null)
                {
                    throw new KeyNotFoundException($"Livro com ID{id} não encontrado.");
                }
                // Aplica os dados recebidos.
                livro.Titulo = dados.Titulo;
                livro.Autor = dados.Autor;
                livro.Emprestado = dados.Emprestado;
                await _context.SaveChangesAsync();
                return livro;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException(
                "Não foi possível atualizar o livro.", ex);
            }
        }

        // Localiza o livro e solicita sua exclusão.
        // Comunica a ausência do registro ou uma falha de gravação.
        public async Task RemoverAsync(int id)
        {
            try
            {
                var livro = await _context.Livros.FindAsync(id);
                if (livro is null)
                {
                    throw new KeyNotFoundException($"Livro com ID {id} nao encontrado.");
                }
                // Solicita a exclusão.
                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException(
                    "Não foi possível remover o livro.", ex);
            }
        }
    }
}
