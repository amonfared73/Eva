using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Eva.Infra.Tools.Hashers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public UserService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<User> GetByUsername(string username)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
        }

        public async Task Register(UserDto userDto)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                string passwordHash = PasswordHasher.Hash(userDto.Password);
                var registrationUser = new User()
                {
                    Username = userDto.Username,
                    PasswordHash = passwordHash,
                };
                await base.InsertAsync(registrationUser);
            }
        }
        public async Task<User> GetUserForLoginAsync(int id)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.Where(e => e.Id == id).FirstOrDefaultAsync();
            }
        }
    }
}
