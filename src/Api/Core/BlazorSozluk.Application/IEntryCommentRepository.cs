using BlazorSozluk.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application
{
    public class IEntryCommentRepository : IGenericRepository<EntryComment>
    {
        int IGenericRepository<EntryComment>.Add(EntryComment entity)
        {
            throw new NotImplementedException();
        }

        int IGenericRepository<EntryComment>.Add(IEnumerable<EntryComment> entities)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<EntryComment>.AddAsync(EntryComment entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<EntryComment>.AddAsync(IEnumerable<EntryComment?> entities)
        {
            throw new NotImplementedException();
        }

        int IGenericRepository<EntryComment>.AddOrUpdate(EntryComment entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<EntryComment>.AddOrUpdateAsync(EntryComment entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<EntryComment> IGenericRepository<EntryComment>.AsQueryable()
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<EntryComment>.BulkAdd(IEnumerable<EntryComment> entities)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<EntryComment>.BulkDelete(Expression<Func<EntryComment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<EntryComment>.BulkDelete(IEnumerable<EntryComment> entities)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<EntryComment>.BulkDeleteById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<EntryComment>.BulkUpdate(IEnumerable<EntryComment> entities)
        {
            throw new NotImplementedException();
        }

        int IGenericRepository<EntryComment>.Delete(EntryComment entity)
        {
            throw new NotImplementedException();
        }

        int IGenericRepository<EntryComment>.Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<EntryComment>.DeleteAsync(EntryComment entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<EntryComment>.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        bool IGenericRepository<EntryComment>.DeleteRange(Expression<Func<EntryComment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task<bool> IGenericRepository<EntryComment>.DeleteRangeAsync(Expression<Func<EntryComment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task<EntryComment> IGenericRepository<EntryComment>.FirstOrDefaultAsync(Expression<Func<EntryComment, bool>> predicate, bool noTracking, params Expression<Func<EntryComment, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        IQueryable<EntryComment> IGenericRepository<EntryComment>.Get(Expression<Func<EntryComment, bool>> predicate, bool noTracking, params Expression<Func<EntryComment, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        Task<List<EntryComment>> IGenericRepository<EntryComment>.GetAll(bool noTracking)
        {
            throw new NotImplementedException();
        }

        Task<EntryComment> IGenericRepository<EntryComment>.GetByIdAsync(Guid id, bool noTracking, params Expression<Func<EntryComment, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        Task<List<EntryComment>> IGenericRepository<EntryComment>.GetList(Expression<Func<EntryComment, bool>> predicate, bool noTracking, Func<IQueryable<EntryComment>, IOrderedQueryable<EntryComment>> orderBy, params Expression<Func<EntryComment, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        Task<EntryComment> IGenericRepository<EntryComment>.GetSingleAsync(Expression<Func<EntryComment, bool>> predicate, bool noTracking, params Expression<Func<EntryComment, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        int IGenericRepository<EntryComment>.Update(EntryComment entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<EntryComment>.UpdateAsync(EntryComment entity)
        {
            throw new NotImplementedException();
        }
    }
}
