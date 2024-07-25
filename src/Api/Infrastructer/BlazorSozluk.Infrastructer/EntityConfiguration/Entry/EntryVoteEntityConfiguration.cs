using BlazorSozluk.Api.Domain.Models;
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

public class EntryVoteEntityConfiguration : BaseEntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entryvote", BlazorSozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.CreatedBy)
            .WithMany(i => i.EntryVotes)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Entry)
            .WithMany(i => i.EntryVotes)
            .HasForeignKey(i => i.EntryId);

    }
}
