using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructer.Persistence.Context
{
    public class BlazorSozlukContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo"; //veritabanı şemasını belirtir

        public BlazorSozlukContext()
        {

        }

        public BlazorSozlukContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } //User'ı kullanabilmek için Domain'i Infrastructer'a referans olarak ekleyebiliriz

        public DbSet<Entry> Entrys { get; set; } //entry'ler tablosunu oluşturduk

        public DbSet<EntryFavorite> EntryFavorites { get; set; }

        public DbSet<EntryVote> EntryVotes { get; set; }




        public DbSet<EntryComment> EntryComments { get; set; }

        public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }

        public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
        


        public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Server=(localdb)\\localDB1;Database=StajBlazorSozluk;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\localDB1;Database=BlazorSozluk;Trusted_Connection=True;");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //yazılan entity'leri ve bunlardan türemiş class'ları otomatik olarak kendine ekleyecektir. 
        }

        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);        
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntites = ChangeTracker.Entries()
                                    .Where(i => i.State == EntityState.Added)
                                    .Select(i => (BaseEntity)i.Entity);
        }

        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreatedDate == DateTime.MinValue)
                entity.CreatedDate = DateTime.Now;
            }
        }

    }
}
