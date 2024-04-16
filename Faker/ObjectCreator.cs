using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public class ObjectCreator
    {
		private Dictionary<Type, IGenerator> generators=new Dictionary<Type, IGenerator>();
		private Plugin plugins = new Plugin();
		private Random random=new Random();

		public ObjectCreator()
		{
			InitGenerator();
			InitPlugins();
		}

		public object CreateInstance(Type type)
		{
			Type typeDefinition;

			if (type.IsGenericType)
				typeDefinition = type.GetGenericTypeDefinition();
			else if (type.IsArray)
				typeDefinition = typeof(Array);
			else
				typeDefinition = type;

			IGenerator generator;
			if (generators.TryGetValue(typeDefinition, out generator))
				if (type==typeof(string))
					return generator.Generate(type);
				else
					return generator.Generate(type);
			else
				return null;
		}

		private void InitGenerator()
		{
			IGenerator[] _allGenerators = { new IntG(random),
										  new FloatG(random),
										  new StringG(random),
											new DateTimeG(random),
										  new ArrayG(random, this),
										  new CollectionG(this) };
			foreach (IGenerator generator in _allGenerators)
				foreach (Type type in generator.PossibleTypes)
					generators.Add(type, generator);
		}

		private void InitPlugins()
		{
			foreach (IGenerator generator in plugins.Plugins)
				foreach (Type type in generator.PossibleTypes)
					generators.Add(type, generator);
		}
	}
}
