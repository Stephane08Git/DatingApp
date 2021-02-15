using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDTo> GetMemberAsync(string username)
        {
            return await _context.Users
                            .Where(x => x.UserName == username)
                            .ProjectTo<MemberDTo>(_mapper.ConfigurationProvider)
                            .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDTo>> GetMembersAsync(UserParams usersParams)
        {
            var query = _context.Users.AsQueryable();

            query = query.Where(u => u.UserName != usersParams.CurrentUsername);
            query = query.Where(u => u.Gender == usersParams.Gender);

            var minDob = DateTime.Today.AddYears(-usersParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-usersParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= minDob &&
                                u.DateOfBirth <= maxDob);

            query = usersParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive)
            };

            return await PagedList<MemberDTo>.CreateAsync(
                query.ProjectTo<MemberDTo>(_mapper.ConfigurationProvider).AsNoTracking(),
                usersParams.PageNumber,
                usersParams.PageSize);
        }

        public async Task<AppUser> GetUserByIdAscync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsermnameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}