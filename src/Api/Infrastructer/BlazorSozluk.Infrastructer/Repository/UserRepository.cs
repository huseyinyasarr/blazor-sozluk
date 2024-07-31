using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Application;
using BlazorSozluk.Infrastructer.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructer.Persistence.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(BlazorSozlukContext dbcontext) : base(dbcontext)
    {
    }
}
