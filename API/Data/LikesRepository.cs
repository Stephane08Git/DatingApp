using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await this._context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        public async Task<PagedList<LikeDTo>> GetUserLikes(LikesParams likesParams)
        {
            var users = this._context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = this._context.Likes.AsQueryable();


            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(l => l.SourceUserId == likesParams.UserId);
                users = likes.Select(l => l.LikedUser);
            }

            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(l => l.LikedUserId == likesParams.UserId);
                users = likes.Select(l => l.SourceUser);
            }

            var likedUsers = users.Select(user => new LikeDTo
            {
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                City = user.City,
                PhotoUrl = user.Photos.SingleOrDefault(p => p.IsMain).Url,
                Id = user.Id
            });

            return await PagedList<LikeDTo>.CreateAsync(likedUsers, 
                likesParams.PageNumber, 
                likesParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int UserId)
        {
            return await this._context.Users
                .Include(x => x.LikedUsers)
                .SingleOrDefaultAsync(x => x.Id == UserId);
        }
    }
}