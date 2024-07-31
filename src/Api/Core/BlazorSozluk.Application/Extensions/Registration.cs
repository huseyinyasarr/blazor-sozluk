using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Extensions;

public static class Registration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assm = Assembly.GetExecutingAssembly(); // şu anda çalışan derlemeden (.dll, .exe) Assembly nesnesini almamızı sağlar

        services.AddMediatR(x => x.RegisterServicesFromAssembly(assm));
        services.AddAutoMapper(assm);
        services.AddValidatorsFromAssembly(assm);

        return services;
    }
    
}
