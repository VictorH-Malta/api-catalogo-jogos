using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository: IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("ca33ab28-098c-4e5f-8f3a-d7c951da7936"), new Jogo{Id = Guid.Parse("ca33ab28-098c-4e5f-8f3a-d7c951da7936"), Nome = "Fifa 21", Produtora = "EA", Preco = 200} },
            {Guid.Parse("9b01f8eb-12e8-4bfa-979f-f62a1b8f0f86"), new Jogo{Id = Guid.Parse("9b01f8eb-12e8-4bfa-979f-f62a1b8f0f86"), Nome = "Fifa 20", Produtora = "EA", Preco = 190} },
            {Guid.Parse("8bbfd389-d805-4ed8-a464-6b5f8515632f"), new Jogo{Id = Guid.Parse("8bbfd389-d805-4ed8-a464-6b5f8515632f"), Nome = "Fifa 19", Produtora = "EA", Preco = 180} },
            {Guid.Parse("42fe971d-fe26-461f-8801-ee6255998341"), new Jogo{Id = Guid.Parse("42fe971d-fe26-461f-8801-ee6255998341"), Nome = "Fifa 18", Produtora = "EA", Preco = 170} },
            {Guid.Parse("889ca674-22f2-4063-af39-d8b89e3aab36"), new Jogo{Id = Guid.Parse("889ca674-22f2-4063-af39-d8b89e3aab36"), Nome = "Street Fighter V", Produtora = "Capcom", Preco = 80} },
            {Guid.Parse("50a37d07-5ac4-4388-a730-277040523680"), new Jogo{Id = Guid.Parse("50a37d07-5ac4-4388-a730-277040523680"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 190} }
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
            {
                return null;
            }

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach (var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                {
                    retorno.Add(jogo);
                }
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco se tivessemos.
        }
    }
}
