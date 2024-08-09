using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Application;
using BlazorSozluk.Infrastructer.Persistence.Context;

namespace BlazorSozluk.Infrastructer.Persistence.Repository
{
    public class EntryCommentRepository : GenericRepository<EntryComment>, IEntryCommentRepository
    {
        public EntryCommentRepository(BlazorSozlukContext dbcontext) : base(dbcontext)
        {
        }
    }
}
