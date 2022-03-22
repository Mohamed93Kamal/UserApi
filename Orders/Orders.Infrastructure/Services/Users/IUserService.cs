using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAll(string searchkey);
        Task<string> Create(CreateUserDto dto);
        Task<string> Update(UpdateUserDto dto);
        Task<string> Delete(string Id);
        Task<UserViewModel> Get(string Id);
    }
}
