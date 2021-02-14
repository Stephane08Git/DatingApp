using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTo>> Register(RegisterDTo registerDTo)
        {
            if (await UserExists(registerDTo.Username)) return BadRequest("Username is taken");

            var user = new AppUser
            {
                UserName = registerDTo.Username.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTo.Password)
            };

            this._context.Add(user);
            await this._context.SaveChangesAsync();

            return new UserDTo
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTo>> Login(LoginDTo loginDTo)
        {
            var user = await this._context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDTo.Username);

            if (user == null) return Unauthorized("Username is not valid.");

            if (!BCrypt.Net.BCrypt.Verify(loginDTo.Password, user.PasswordHash)) return Unauthorized("Incorrect password");

            return new UserDTo
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
            };
        }

        public async Task<bool> UserExists(string userName)
        {
            return await this._context.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }
    }
}
