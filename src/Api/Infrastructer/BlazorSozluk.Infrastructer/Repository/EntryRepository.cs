using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Application;
using BlazorSozluk.Infrastructer.Persistence.Context;

namespace BlazorSozluk.Infrastructer.Persistence.Repository
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(BlazorSozlukContext dbcontext) : base(dbcontext)
        {
        }
    }
}
