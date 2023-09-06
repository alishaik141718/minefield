namespace MinefieldGameConsole
{
	public static class Utils
	{
        public static int GetRandomNumber(int min = 0, int max = 0)
        {
            return new Random().Next(min, max);
        }
    }
}

