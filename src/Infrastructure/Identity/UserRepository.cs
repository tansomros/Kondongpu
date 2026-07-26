using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kondongpu.Application.Identity.Interfaces;
using Kondongpu.Domain.Entities;
using Kondongpu.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kondongpu.Infrastructure.Identity;
#nullable enable
public sealed class UserRepository : IUserRepository
{
    private readonly KondongpuDatabaseContext _context;

    public UserRepository(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<User?> FindByUsernameAsync(
        string username,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x =>
                x.Username == username &&
                x.IsActive &&
                x.DeleteFlag != true,
                cancellationToken);
    }

    //public async Task UpdateAsync(
    //    User user,
    //    CancellationToken cancellationToken)
    //{
    //    _context.Users.Update(user);
    //    await _context.SaveChangesAsync(cancellationToken);
    //}
    public async Task SaveChangesAsync(
    CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
