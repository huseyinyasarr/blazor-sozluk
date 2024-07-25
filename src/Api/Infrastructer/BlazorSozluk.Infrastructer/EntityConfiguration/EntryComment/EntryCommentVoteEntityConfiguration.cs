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

namespace BlazorSozluk.Infrastructer.Persistence.EntityConfiguration.EntryComment;

public class EntryCommentVoteEntityConfiguration : BaseEntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycommentvote", BlazorSozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.CreatedBy)
            .WithMany(i => i.EntryCommentVotes)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.EntryComment)
            .WithMany(i => i.EntryCommentVotes)
            .HasForeignKey(i => i.EntryCommentId);
    }
}
