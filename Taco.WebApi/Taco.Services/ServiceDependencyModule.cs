using Autofac;
using Taco.Services.Restaurant;

namespace Taco.Services
{
    public class ServiceDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RestaurantService>().As<IRestaurantService>();
        }
    }
}
