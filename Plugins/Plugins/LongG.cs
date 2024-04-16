using System;
using System.Linq;
using Faker;

namespace Plugins
{
    class LongG: IGenerator
    {
		public Type[] PossibleTypes => new[] { typeof(long) };
		private const int minValue = -2147483648, maxValue = 2147483647;
		private Random _numGen;

		public LongG()
		{
			_numGen = new Random();
		}

		public LongG(Random numGen)
		{
			_numGen = numGen ?? throw new ArgumentNullException();
		}

		public object Generate(Type type)
		{
			if (!PossibleTypes.Contains(type))
				throw new ArgumentException();

			long result = (long)_numGen.Next(minValue, maxValue) * _numGen.Next();

			if (result == 0)
				result = 1;

			return result;
		}
	}
}
