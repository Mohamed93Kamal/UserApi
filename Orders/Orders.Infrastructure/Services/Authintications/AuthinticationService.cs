using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Orders.API.Data;
using Orders.Core.Constant;
using Orders.Core.Dtos;
using Orders.Core.Exceptions;
using Orders.Core.Options;
using Orders.Core.ViewModels;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Authintications
{
    public class AuthinticationService : IAuthinticationService
    {
        private readonly OrdersDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtOptions _options;

        public AuthinticationService (OrdersDbContext db, UserManager<User> userManager, IMapper mapper , IOptions<JwtOptions> options)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
            _options = options.Value;
        }

        public async Task<LoginResponseViewModel> Login(LoginDto dto)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == dto.UserName);
            if(user == null)
            {
                throw new InvalidUserNameOrPassword();
            }
           var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if(!result)
            {
                throw new InvalidUserNameOrPassword();
            }
            var response = new LoginResponseViewModel();
            response.AccessToken = await GenerateAccessToken(user);
            response.User = _mapper.Map<UserViewModel>(user);
            return response;
        }

        public async Task<AccessTokenViewModel> GenerateAccessToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(Claims.UserId , user.Id),
                new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
                new Claim(Claims.PhoneNumber , user.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())

            };
            if(roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roles)));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMonths(1);
            var accessToken = new JwtSecurityToken(_options.Issuer,
                _options.Issuer,
                claims,
                expires : expires,
                signingCredentials : credentials
                );

            var result = new AccessTokenViewModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                ExpireToken = expires
            };
            return result;


        }

    }
}
