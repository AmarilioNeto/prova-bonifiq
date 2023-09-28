namespace ProvaPub.Services
{
	public class RandomService
	{
		int seed;
		public RandomService()
		{
			seed = 0;
		}
		public int GetRandom()
		{
			return new Random().Next(1000);		 
			
		}

	}
}
