using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructer.Persistence.EntityConfiguration;

public class UserEntityConfiguration : BaseEntityConfiguration<User>
{

    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("user", BlazorSozlukContext.DEFAULT_SCHEMA);

        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("getdate()");



        //builder.hasmany(x => x.entries)
        //    .withone(x => x.createdby)
        //    .hasforeignkey(x => x.createdbyıd)
        //    .ısrequired();

        //builder.HasMany(x => x.EntryVotes)
        //    .WithOne(x => x.CreatedBy)
        //    .HasForeignKey(x => x.CreatedById)
        //    .IsRequired();

    }

}


