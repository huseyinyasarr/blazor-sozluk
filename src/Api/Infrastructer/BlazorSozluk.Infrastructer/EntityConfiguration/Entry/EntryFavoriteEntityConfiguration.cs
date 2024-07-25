using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructer.Persistence.Context;
using BlazorSozluk.Infrastructer.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructer.Persistence.EntityConfiguration.Entry;

public class EntryFavoriteEntityConfiguration : BaseEntityConfiguration<EntryFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("entryfavorite", BlazorSozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.Entry)
            .WithMany(i => i.EntryFavorites)
            .HasForeignKey(i => i.EntryId);

        builder.HasOne(i => i.CreatedUser)
            .WithMany(i => i.EntryFavorites)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(i => i.CreatedBy)
        //    .WithMany(i => i.EntryFavorites)
        //    .HasForeignKey(i => i.CreatedById)
        //    .OnDelete(DeleteBehavior.Restrict);
    }

}




