using Contracts.FileReaders;
using Contracts.FileWriters;
using Contracts.InstructionExecutors;
using Contracts.Map;
using Contracts.Robots;
using Contracts.Settings;
using Services.FileReaders;
using Services.FileWriters;
using Services.InstructionExecutors;
using Services.Robots;
using Services.WalkingStrategies;
using Settings.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace Services.Container
{
    public class Container
    {
        public static UnityContainer BuildContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<PositionState>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
            container.RegisterType<Tracker>(new ContainerControlledLifetimeManager(), new InjectionConstructor(typeof(PositionState)));
            container.RegisterType<IInstructionExecutor, InstructionExecutor>();
            container.RegisterType<IBackOffStrategiesExecutor, BackOffStrategiesExecutor>();
            container.RegisterType<IBackOffInstructionsInitializer, StandardBackOffInstructionsInitializer>();
            container.RegisterType<IWorkParametersProvider, ReadingInput>();
            container.RegisterType<IFileWriter, FileWriter>();
            container.RegisterType<IMachineCleaner, Robot>();

            var backOffSettings = new BackOffSettings();
            backOffSettings.Strategies = AppSettings.SafeGet<string>(backOffSettings.BackOffStrategiesTag);
            container.RegisterInstance(backOffSettings);


            return container;
        }
    }
}
