using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAscync(int id);
        Task<AppUser> GetUserByUsermnameAsync(string username);
        Task<IEnumerable<MemberDTo>> GetMembersAsync();
        Task<MemberDTo> GetMemberAsync(string username);
    }
}