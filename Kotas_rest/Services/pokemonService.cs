using Kotas_rest.Interface;
using Kotas_rest.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System;
using Kotas_rest.Util;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Diagnostics.Metrics;

namespace Kotas_rest.Services
{
	public class pokemonService : IpokemonService
	{
		private static string resposta = string.Empty;
		private readonly IpokemonUtil  _IpokemonUtil;

		public pokemonService(IpokemonUtil ipokemonUtil)
		{
			_IpokemonUtil = ipokemonUtil;
		}

		public async Task<pokemonSelect> GetPokemon(string pokemon)
		{
			pokemonModel retorno = new pokemonModel();
			pokemonSelect pokemonSelect = new pokemonSelect();

			string url = $"https://pokeapi.co/api/v2/pokemon/{pokemon}";

			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, url);
			var response = await client.SendAsync(request);
			resposta = response.StatusCode.ToString();

			if (resposta == "OK")
			{
				var resultClient = await response.Content.ReadAsStringAsync();
				retorno = JsonConvert.DeserializeObject<pokemonModel>(resultClient);
				pokemonSelect.id = retorno.id;
				pokemonSelect.name = retorno.name;
			}

			return pokemonSelect;

		}
		public async Task<List<pokemonSelect>> GetPokemonRandom()
		{

			pokemonModel retorno = new pokemonModel();
			List<pokemonSelect> pokemonSelects = new List<pokemonSelect>();

			var random =  await _IpokemonUtil.GetRandom(10, 1, 100);

			foreach(var Item in random)
			{
				pokemonSelect pokemonSelect = new pokemonSelect();
				List<pokemonEvolucao> pokemonEvolucao = new List<pokemonEvolucao>();

				string url = $"https://pokeapi.co/api/v2/pokemon/{Item}";

				var client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Get, url);
				var response = await client.SendAsync(request);
				resposta = response.StatusCode.ToString();

				if (resposta == "OK")
				{
					var resultClient = await response.Content.ReadAsStringAsync();
					retorno = JsonConvert.DeserializeObject<pokemonModel>(resultClient);
					pokemonSelect.id = retorno.id;
					pokemonSelect.name = retorno.name;
					pokemonSelect.sprites = retorno.sprites.front_default;
					pokemonEvolucao = await GetPokemonEvolution(Item);
					pokemonSelect.pokemonEvolucao = pokemonEvolucao;
					pokemonSelects.Add(pokemonSelect);
				}			

			}
			return pokemonSelects;

		}
		public async Task<pokemonSelect> GetPokemonUnico(int id)
		{

			pokemonModel retorno = new pokemonModel();
			List<pokemonSelect> pokemonSelects = new List<pokemonSelect>();
			pokemonSelect pokemonSelect = new pokemonSelect();
			List<pokemonEvolucao> pokemonEvolucao = new List<pokemonEvolucao>();

			string url = $"https://pokeapi.co/api/v2/pokemon/{id}";

			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, url);
			var response = await client.SendAsync(request);
			resposta = response.StatusCode.ToString();

			if (resposta == "OK")
			{
				var resultClient = await response.Content.ReadAsStringAsync();
				retorno = JsonConvert.DeserializeObject<pokemonModel>(resultClient);
				pokemonSelect.id = retorno.id;
				pokemonSelect.name = retorno.name;
				pokemonSelect.sprites = retorno.sprites.front_default;
				pokemonEvolucao = await GetPokemonEvolution(retorno.id);
				pokemonSelect.pokemonEvolucao = pokemonEvolucao;
			}
			
			return pokemonSelect;

		}
		public async Task<List<pokemonEvolucao>> GetPokemonEvolution(int id)
		{
			pokemonEvolutionModel pokemonEvolution = new pokemonEvolutionModel();
			List<pokemonEvolucao> pokemonEvolucaos = new List<pokemonEvolucao>();

			string url = $"https://pokeapi.co/api/v2/evolution-chain/{id}";

			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, url);
			var response = await client.SendAsync(request);
			resposta = response.StatusCode.ToString();

			if (resposta == "OK")
			{
				var resultClient = await response.Content.ReadAsStringAsync();
				var retorno = JsonConvert.DeserializeObject<pokemonEvolutionModel>(resultClient);
				
				foreach(var item in retorno.chain.evolves_to)
				{
					pokemonEvolucao pokemonEvolucao = new pokemonEvolucao();
					pokemonEvolucao.id = id;
					pokemonEvolucao.evolucao = item.species.name.ToString();
					pokemonEvolucaos.Add(pokemonEvolucao);
				}
			}
			return pokemonEvolucaos;
		}

		//MESTRE
		public async Task<List<Mestre>> PostInsetMestrePokemon(Mestre mestre)
		{
			List<Mestre> mestres= new List<Mestre>();

			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				connection.Open();
				await CreateTableMestre(connection);
				await InsertMestre(connection, mestre);
				var resultado = PostReadMestrePokemon(connection);
				foreach(var item in resultado.Result)
				{
					Mestre _mestre = new Mestre();
					_mestre.nome = item.nome;
					_mestre.idade = item.idade;
					_mestre.cpf = item.cpf;
					mestres.Add(_mestre);
				}
				
				return mestres;

			}
		}
		public async Task<IEnumerable<Mestre>> PostReadMestrePokemon(SqliteConnection connection)
		{
			{
				var sql = "SELECT * FROM Mestre";
				return connection.Query<Mestre>(sql);
			}
		}
		public async Task<IEnumerable<Capturado>> PostReadCapturadoPokemon(SqliteConnection connection)
		{
			{
				var sql = "SELECT * FROM Capturado";
				return connection.Query<Capturado>(sql);
			}
		}
		private async Task InsertMestre(SqliteConnection connection, Mestre mestre)
		{
			var sql = $"INSERT INTO Mestre (Nome, Idade, CPF) VALUES ('{mestre.nome}', {mestre.idade}, '{mestre.cpf}')";
			connection.Execute(sql, mestre);
		}
		private async Task InsertCapturadoPokemon(SqliteConnection connection, Capturado capturado)
		{
			var sql = $"INSERT INTO Capturado (Id_Mestre,Id_Pokemon) VALUES ({capturado.id_Mestre}, {capturado.id_Pokemon})";
			connection.Execute(sql, capturado);
		}
		private async Task CreateTableMestre(SqliteConnection connection)
		{
			var sql = @"CREATE TABLE IF NOT EXISTS Mestre (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nome TEXT NOT NULL,
                            Idade INTEGER NOT NULL,
                            CPF TEXT NOT NULL UNIQUE)";

			connection.Execute(sql);
		}
		private async Task CreateTableMestrePokemon(SqliteConnection connection)
		{
			var sql = @"CREATE TABLE IF NOT EXISTS Capturado (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
							Id_Mestre INTEGER NOT NULL,
							Id_Pokemon INTEGER NOT NULL)";

			connection.Execute(sql);
		}		
		public async Task<IEnumerable<Mestre>> GetMestrePokemon()
		{
			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				connection.Open();
				var sql = "SELECT * FROM Mestre";
				return connection.Query<Mestre>(sql);
			}
		}
		public async Task<Mestre> GetSelectMestrePokemon(int id)
		{
			Mestre _mestre = new Mestre();

			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				connection.Open();
				var sql = $"SELECT * FROM Mestre WHERE id = {id}";
				_mestre = connection.QueryFirstOrDefault<Mestre>(sql, new { Id = id });
				return _mestre;
			}
		}
		public async Task<Mestre> UpdateMestrePokemon(Mestre mestre)
		{
			Mestre _mestre = new Mestre();

			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				var sql = $"UPDATE Mestre SET Nome = '{mestre.nome}', Idade = {mestre.idade}, CPF = '{mestre.cpf}' WHERE Id = {mestre.id}";
				connection.Execute(sql, mestre);
				_mestre = await GetSelectMestrePokemon(mestre.id);
				return _mestre;
			}
		}
		public async Task<string> DeleteMestrePokemon(int id)
		{
			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				var sql = $"DELETE FROM Mestre WHERE Id = {id}";
				connection.Execute(sql, new { Id = id });
				return "Registro deletado com sucesso!";
			}
		}

		//CAPTURADO
		public async Task<IEnumerable<Capturado>> GetCapturadoMestre(int id)
		{
			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				connection.Open();
				var sql = $"SELECT * FROM Capturado WHERE Id_Mestre = {id}";
				var capturado = connection.Query<Capturado>(sql, new { Id = id });
				return capturado;
			}
		}
		public async Task<CapturadoMestrePokemon> PostInsetMestrePokemon(Capturado capturado)
		{
			CapturadoMestrePokemon capturadoMestre = new CapturadoMestrePokemon();
			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				connection.Open();
				await CreateTableMestrePokemon(connection);
				await InsertCapturadoPokemon(connection, capturado);
				var resultado = await GetCapturadoMestre(capturado.id_Mestre);
				var nome = await GetSelectMestrePokemon(capturado.id_Mestre);
				capturadoMestre.nome = nome.nome.ToString();
				foreach (var item in resultado)
				{
					var pokemon = await GetPokemonUnico(item.id_Mestre);
					capturadoMestre.pokemon = pokemon;
				}

				return capturadoMestre;

			}

		}
		public async Task<List<CapturadoMestrePokemon>> GetMestrePokemon(int id)
		{
			List<CapturadoMestrePokemon> capturadoMestres = new List<CapturadoMestrePokemon>();
			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				connection.Open();
				var resultado = await GetCapturadoMestre(id);

				foreach(var item in resultado)
				{
					CapturadoMestrePokemon capturadoMestre = new CapturadoMestrePokemon();
					var result = await GetSelectMestrePokemon(id);
					capturadoMestre.nome = result.nome;
					var pokemon = await GetPokemonUnico(item.id_Pokemon);
					capturadoMestre.pokemon = pokemon;
					capturadoMestre.id = item.id;
					capturadoMestres.Add(capturadoMestre);
				}

				return capturadoMestres;

			}

		}
		public async Task<string> DeleteCapturaPokemon(int id)
		{
			using (var connection = new SqliteConnection("Data Source=./database.sqlite"))
			{
				var sql = $"DELETE FROM Capturado WHERE Id = {id}";
				connection.Execute(sql, new { Id = id });
				return "Registro deletado com sucesso!";
			}
		}
	}
}
