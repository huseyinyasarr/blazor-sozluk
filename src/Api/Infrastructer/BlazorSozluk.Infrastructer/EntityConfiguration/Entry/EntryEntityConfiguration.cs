using BlazorSozluk.Infrastructer.Persistence.Context;
using BlazorSozluk.Infrastructer.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructer.Persistence.EntityConfiguration.Entry;

public class EntryEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Entry>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
    {
        base.Configure(builder);

        builder.ToTable("entry", BlazorSozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(e => e.CreatedBy)
            .WithOne()
            .HasForeignKey<Api.Domain.Models.Entry>(x => x.CreatedById)
            .IsRequired();

        builder.Property(e => e.CreatedDate)
    .HasDefaultValueSql("getdate()");

        builder.HasMany(x => x.EntryVotes)
            .WithOne(x => x.Entry)
            .HasForeignKey(x => x.EntryId)
            .IsRequired();

    }
}
