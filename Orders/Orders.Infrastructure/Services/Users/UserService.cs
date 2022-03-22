using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orders.API.Data;
using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly OrdersDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService(OrdersDbContext db, UserManager<User> userManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GetAll(string searchkey)
        {
            var users = _db.Users.Where(x => x.FullName.Contains(searchkey) || x.PhoneNumber.Contains(searchkey) || string.IsNullOrWhiteSpace(searchkey)).ToList();
          return  _mapper.Map<List<UserViewModel>>(users);
          
        }

        public async Task<string> Create(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.UserName = dto.PhoneNumber;
           await _userManager.CreateAsync(user, dto.Password);
            return user.Id;
        }

        public async Task<string> Update(UpdateUserDto dto)
        {
            var user =await _db.Users.SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (user == null)
            {
                //Exception
            }
            var updateUser = _mapper.Map(dto, user);
            _db.Users.Update(updateUser);
            _db.SaveChanges();
            return user.Id;
        }

        public async Task<string> Delete(string Id)
        {
            var user =await _db.Users.SingleOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                //Exception
            }
            user.IsDelete = true;
            _db.Users.Update(user);
            _db.SaveChanges();
            return user.Id;
        }

        public async Task<UserViewModel> Get(string Id)
        {
            var user =await _db.Users.SingleOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                //Exception
            }
            return _mapper.Map<UserViewModel>(user);
           
        }

    }
}
