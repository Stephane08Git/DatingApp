using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(DataContext context,
                                 ITokenService tokenService,
                                 IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTo>> Register(RegisterDTo registerDTo)
        {
            if (await UserExists(registerDTo.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registerDTo);

            user.UserName = registerDTo.Username.ToLower();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTo.Password);

            this._context.Add(user);
            await this._context.SaveChangesAsync();

            return new UserDTo
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs

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
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs
            };
        }

        public async Task<bool> UserExists(string userName)
        {
            return await this._context.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }
    }
}
