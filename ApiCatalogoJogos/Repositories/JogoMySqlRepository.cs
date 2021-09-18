using ApiCatalogoJogos.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoMySqlRepository : IJogoRepository
    {
        private readonly MySqlConnection mySqlConnection;

        public JogoMySqlRepository(IConfiguration configuration)
        {
            mySqlConnection = new MySqlConnection(configuration.GetConnectionString("Default"));
        }
        public async Task Atualizar(Jogo jogo)
        {
            var comando = $"UPDATE catalogo SET Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco = '{jogo.Preco.ToString().Replace(",", ".")}' WHERE Id = '{jogo.Id}'";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            mySqlConnection?.Close();
            mySqlConnection?.Dispose();
        }

        public async Task Inserir(Jogo jogo)
        {
            var comando = $"INSERT catalogo (Id, Nome, Produtora, Preco) VALUES ('{jogo.Id}', '{jogo.Nome}', '{jogo.Produtora}', '{jogo.Preco.ToString().Replace(",", ".")}')";
            
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();

            var comandos = $"SELECT * FROM catalogo ORDER BY Id LIMIT {quantidade} OFFSET {(pagina - 1) * quantidade}";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comandos, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)await mySqlCommand.ExecuteReaderAsync();

            while (mySqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = Guid.Parse((string)mySqlDataReader["Id"]),
                    Nome = (string)mySqlDataReader["Nome"],
                    Produtora = (string)mySqlDataReader["Produtora"],
                    Preco = (double)mySqlDataReader["Preco"]
                });
            }

            await mySqlConnection.CloseAsync();

            return jogos;
        }

        public async Task<Jogo> Obter(Guid id)
        {
            Jogo jogo = null;

            var comandos = $"SELECT * FROM catalogo WHERE Id = '{id}'";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comandos, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)await mySqlCommand.ExecuteReaderAsync();

            while (mySqlDataReader.Read())
            {
                jogo = new Jogo
                {
                    Id = Guid.Parse((string)mySqlDataReader["Id"]),
                    Nome = (string)mySqlDataReader["Nome"],
                    Produtora = (string)mySqlDataReader["Produtora"],
                    Preco = (double)mySqlDataReader["Preco"]
                };
            }

            await mySqlConnection.CloseAsync();

            return jogo;
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();

            var comandos = $"SELECT * FROM catalogo WHERE Nome = '{nome}' AND Produtora = '{produtora}'";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comandos, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)await mySqlCommand.ExecuteReaderAsync();

            while (mySqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = Guid.Parse((string)mySqlDataReader["Id"]),
                    Nome = (string)mySqlDataReader["Nome"],
                    Produtora = (string)mySqlDataReader["Produtora"],
                    Preco = (double)mySqlDataReader["Preco"]
                });
            }

            await mySqlConnection.CloseAsync();

            return jogos;
        }

        public async Task Remover(Guid id)
        {
            var comando = $"DELETE FROM catalogo WHERE Id = '{id}' ";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();
        }
    }
}
