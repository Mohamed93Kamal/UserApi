using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Authintications
{
    public interface IAuthinticationService
    {
        Task<LoginResponseViewModel> Login(LoginDto dto);
        Task<AccessTokenViewModel> GenerateAccessToken(User user);
    }
}
