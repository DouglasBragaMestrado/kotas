using Kotas_rest.Interface;
using Kotas_rest.Models;
using Kotas_rest.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kotas_rest.Controllers.pokemon
{
	[ApiController]

	public class pokemonController : Controller
	{
		private readonly IpokemonService _pokemonService;
		public pokemonController(IpokemonService pokemonServiceLocal)
		{
			_pokemonService = pokemonServiceLocal;
		}

		
		[Route("api/GetPokemon")]
		[HttpGet]
		public async Task<ActionResult> GetPokemon(string pokemon)
		{
			var resultado = await _pokemonService.GetPokemon(pokemon);
			return Json(resultado);
		}

		[Route("api/GetPokemonRandom")]
		[HttpGet]
		public async Task<ActionResult> GetPokemonRandom()
		{
			var resultado = await _pokemonService.GetPokemonRandom();
			return Json(resultado);
		}

		[Route("api/PostInsetMestrePokemon")]
		[HttpPost]
		public async Task<ActionResult> PostInsetMestrePokemon(string Nome, int Idade, string CPF)
		{
			Mestre mestre = new Mestre();
			mestre.nome = Nome;
			mestre.idade = Idade;
			mestre.cpf = CPF;

			var resultado = await _pokemonService.PostInsetMestrePokemon(mestre);
			return Json(resultado);
		}

		[Route("api/GetMestrePokemon")]
		[HttpGet]
		public async Task<ActionResult> GetMestrePokemon()
		{
			var resultado = await _pokemonService.GetMestrePokemon();
			return Json(resultado);
		}

		[Route("api/GetSelectMestrePokemon")]
		[HttpGet]
		public async Task<ActionResult> GetSelectMestrePokemon(int id)
		{
			var resultado = await _pokemonService.GetSelectMestrePokemon(id);
			return Json(resultado);
		}

		[Route("api/UpdateMestrePokemon")]
		[HttpPost]
		public async Task<ActionResult> UpdateMestrePokemon(int Id,string Nome, int Idade, string CPF)
		{
			Mestre mestre = new Mestre();
			mestre.id = Id;
			mestre.nome = Nome;
			mestre.idade = Idade;
			mestre.cpf = CPF;

			var resultado = await _pokemonService.UpdateMestrePokemon(mestre);
			return Json(resultado);
		}
		[Route("api/DeleteMestrePokemon")]
		[HttpDelete]
		public async Task<ActionResult> DeleteMestrePokemon(int id)
		{
			var resultado = await _pokemonService.DeleteMestrePokemon(id);
			return Json(resultado);
		}
		[Route("api/Capturar")]
		[HttpPost]
		public async Task<ActionResult> PostInsetMestrePokemon(int id_Mestre, int id_pokemon)
		{
			Capturado capturado = new Capturado();
			capturado.id_Mestre = id_Mestre;
			capturado.id_Pokemon = id_pokemon;

			var resultado = await _pokemonService.PostInsetMestrePokemon(capturado);
			return Json(resultado);
		}
		[Route("api/Capturar/mestre")]
		[HttpGet]
		public async Task<ActionResult> GetMestrePokemon(int id_Mestre)
		{
			var resultado = await _pokemonService.GetMestrePokemon(id_Mestre);
			return Json(resultado);
		}
		[Route("api/Capturar/delete")]
		[HttpDelete]
		public async Task<ActionResult> DeleteCapturaPokemon(int id)
		{
			var resultado = await _pokemonService.DeleteCapturaPokemon(id);
			return Json(resultado);
		}
	}
}
