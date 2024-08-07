using Kotas_rest.Models;

namespace Kotas_rest.Interface
{
	public interface IpokemonService
	{
		public Task<pokemonSelect> GetPokemon(string pokemon);
		public Task<List<pokemonSelect>> GetPokemonRandom();
		public Task<List<Mestre>> PostInsetMestrePokemon(Mestre mestre);
		public Task<IEnumerable<Mestre>> GetMestrePokemon();
		public Task<Mestre> UpdateMestrePokemon(Mestre mestre);
		public Task<Mestre> GetSelectMestrePokemon(int id);
		public Task<string> DeleteMestrePokemon(int id);
		public Task<CapturadoMestrePokemon> PostInsetMestrePokemon(Capturado capturado);
		public Task<List<CapturadoMestrePokemon>> GetMestrePokemon(int id);
		public Task<string> DeleteCapturaPokemon(int id);
	}
}
