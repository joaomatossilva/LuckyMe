using Autofac;
using LuckyMe.Core.Data;

namespace LuckyMe.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
        }
    }
}
