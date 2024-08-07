namespace Kotas_rest.Interface
{
	public interface IpokemonUtil
	{
		public Task <HashSet<int>> GetRandom(int count, int min, int max);
	}
}
