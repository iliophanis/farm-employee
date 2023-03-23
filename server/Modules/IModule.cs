namespace server.Modules
{
    public interface IModule
    {
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, IConfiguration config);
    }

    public static class ModuleExtensions
    {
        // this could also be added into the DI container
        static readonly List<IModule> registeredModules = new List<IModule>();

        public static WebApplication MapEndpoints(this WebApplication app, IConfiguration config)
        {
            var modules = DiscoverModules();
            foreach (var module in modules)
            {
                module.MapEndpoints(app, config);
            }
            return app;
        }

        private static IEnumerable<IModule> DiscoverModules()
        {
            return typeof(IModule).Assembly
                .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                .Select(Activator.CreateInstance)
                .Cast<IModule>();
        }
    }
}