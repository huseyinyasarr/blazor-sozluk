using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Infrastructer;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructer.Persistence.Context;

internal class SeedData
{

    private static List<User> GetUsers()
    {
        var result = new Faker<User>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.UserName, i => i.Internet.UserName())
                .RuleFor(i => i.Password, i => PasswordEncryptor.Encrpt(i.Internet.Password()))
                .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
                .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
            .Generate(3);


        return result;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder(); //db context oluşturmasını sağlar
        dbContextBuilder.UseSqlServer(configuration["BlazorSozlukDbConnectionString"]);

        var context = new BlazorSozlukContext(dbContextBuilder.Options); //Dışarıdan blazor context'i set etmemizi sağlar

        var users = GetUsers();
        var userIds = users.Select(i => i.Id);

        await context.Users.AddRangeAsync(users);


        var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
        int counter = 0;

        var entries = new Faker<Entry>("tr")
                    .RuleFor(i => i.Id, i => guids[counter++])
                    .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                    .RuleFor(i => i.Subject, i => i.Lorem.Sentence(5, 5))
                    .RuleFor(i => i.Content, i => i.Lorem.Paragraph(3))
                    .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .Generate(5);

        await context.Entrys.AddRangeAsync(entries);

        foreach (var entry in entries)
        {
            var comments = new Faker<EntryComment>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .RuleFor(i => i.EntryId, entry.Id)
            .Generate(10);

            await context.EntryComments.AddRangeAsync(comments);
        }

        

        await context.SaveChangesAsync();

    }
}
