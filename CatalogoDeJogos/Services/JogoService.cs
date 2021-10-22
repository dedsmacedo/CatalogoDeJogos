using APICatalogoDeJogos.Entities;
using APICatalogoDeJogos.Exceptions;
using APICatalogoDeJogos.InputModel;
using APICatalogoDeJogos.Repositorio;
using APICatalogoDeJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogoDeJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepositorio _jogoRepositorio;

        public JogoService(IJogoRepositorio jogoRepositorio)
        {
            _jogoRepositorio = jogoRepositorio;
        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(id);
            if (entidadeJogo == null)
                throw new JogoNaoCadastradoExpection();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;
             
            await _jogoRepositorio.Atualizar(entidadeJogo);

        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(id);
            if (entidadeJogo == null)
                throw new JogoNaoCadastradoExpection();

            await _jogoRepositorio.Remover(id);
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(jogo.Nome, jogo.Produtora);
            if (entidadeJogo.Count > 0)
            throw new JogoJaCadastradoException();

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepositorio.Inserir(jogoInsert);
            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };    
                          
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepositorio.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel>Obter(Guid id)
        {
            var jogo = await _jogoRepositorio.Obter(id);

            if (jogo == null)
                return null;

                return new JogoViewModel
                {
                    Id = jogo.Id,
                    Nome = jogo.Nome,
                    Produtora = jogo.Produtora,
                    Preco = jogo.Preco

                };
            
        }

        public async Task Remover(Guid id)
        {
            var jogo = _jogoRepositorio.Obter(id);
            if (jogo == null)
                throw new JogoNaoCadastradoExpection();

            await _jogoRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepositorio.Dispose();
        }
    }

    
}
