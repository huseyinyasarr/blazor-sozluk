using BlazorSozluk.Application;
using BlazorSozluk.Infrastructer.Persistence.Context;
using BlazorSozluk.Infrastructer.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructer.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlazorSozlukContext>(conf =>
        {
            var connStr = configuration["BlazorSozlukDbConnectionString"].ToString();
            conf.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure(); //db'ye bağlanırken bir sıkıntı olursa tekrar deneme yapması için
            });
        });


        //Tekrar tekrar data üretmesin diye yorum satırına alınabilir ama SeedData class'ındaki if(context.Users.Any) bloğu bu durumu kontrol idiyor

        var seedData = new SeedData();
        seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        //services.AddScoped<IUserRepository, IUserRepository>();

        services.AddScoped<IUserRepository, UserRepository>();


        return services;
    }
}
