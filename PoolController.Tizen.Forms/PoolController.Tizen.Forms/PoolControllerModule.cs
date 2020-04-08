using Autofac;
using eHub.Common.Api;
using eHub.Common.Models;
using eHub.Common.Services;
using PoolController.Tizen.Forms.Models;
using PoolController.Tizen.Forms.Views;
using System.IO;

namespace PoolController.Tizen.Forms
{
    public class PoolControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PoolControllerInjector>()
                .SingleInstance()
                .AutoActivate();

            builder.Register(ctx =>
            {
                //TODO read from manifest
                return new AppVersion { VersionName = "1.0.0", VersionNumber = 1000 }; 
            }).As<AppVersion>();

            builder.Register(ctx =>
            {
                Configuration config = ctx.Resolve<Configuration>();
                return new WebInterface(config);
            })
            .As<IWebInterface>()
            .SingleInstance();

            builder.Register(ctx =>
            {
                IWebInterface webApi = ctx.Resolve<IWebInterface>();
                return new PoolApi(webApi);
            })
            .As<IPoolApi>()
            .SingleInstance();

            builder.Register(ctx =>
            {
                IPoolApi poolApi = ctx.Resolve<IPoolApi>();
                return new PoolService(poolApi);
            })
            .As<IPoolService>()
            .SingleInstance();
        }
    }
}