// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ServicesAssemblyDependentServicesInstaller.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;
using MediatR;
using MoneybaseTask.Common.Core.Infrastructure.Builder;
using MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;
using MoneybaseTask.Hosting.WebApi.Behaviours;

namespace MoneybaseTask.Hosting.WebApi.Installers;

internal class ServicesAssemblyDependentServicesInstaller : IInstaller
{
    private const string ServicesAssemblyName = "MoneybaseTask.Business.Services";

    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var servicesAssembly = AppDomain.CurrentDomain
            .GetAssemblies()
            .FirstOrDefault(a => a.FullName != null && a.FullName.StartsWith(ServicesAssemblyName, StringComparison.InvariantCulture));

        if (servicesAssembly is null)
            return;

        //This registers all implementations of type T interface 
        services.Scan(scan => scan.FromAssemblies(servicesAssembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IBuilder<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IMoneybaseAsyncMapper<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        //MediatR
        services.AddMediatR(servicesAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        //FluentValidation
        services.AddValidatorsFromAssembly(servicesAssembly);

        //AutoMapper
        services.AddAutoMapper(servicesAssembly);
    }
}