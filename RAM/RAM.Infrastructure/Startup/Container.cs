using Autofac;

namespace RAM.Infrastructure.Startup
{
	public class Container
	{
		private static IContainer _container;

		static Container()
		{
			Initialize();
		}

		public static T Resolve<T>()
			=> _container.Resolve<T>();

		public static void Initialize()
		{
			if (_container == null)
				_container = new BootStraper().BootStrap();
		}
	}
}