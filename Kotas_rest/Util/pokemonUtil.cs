using Kotas_rest.Interface;

namespace Kotas_rest.Util
{
	public class pokemonUtil: IpokemonUtil
	{
		public async Task<HashSet<int>> GetRandom(int count, int min, int max)
		{
			var random = new Random();
			var numbers = new HashSet<int>();
			while (numbers.Count < count)
				numbers.Add(random.Next(min, max + 1));
			return numbers;
		}
	}
}
