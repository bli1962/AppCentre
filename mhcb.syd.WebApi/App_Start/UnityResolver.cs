//using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;

namespace mhcb.syd.WebApi.App_Start
{
	public class UnityResolver : IDependencyResolver
	{
		private readonly IUnityContainer _container;

		public UnityResolver(IUnityContainer container)
		{
			_container = container;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return _container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return null;
				//throw new InvalidOperationException(
				//	$"Unable to resolve service for type {serviceType}.", ResolutionFailedException);
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return _container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
				//throw new InvalidOperationException(
				//	 $"Unable to resolve service for type {serviceType}.", ResolutionFailedException);
			}
		}

		public IDependencyScope BeginScope()
		{
			var child = _container.CreateChildContainer();
			return new UnityResolver(child);
		}

		public void Dispose()
		{
			_container.Dispose();
		}
	}

}