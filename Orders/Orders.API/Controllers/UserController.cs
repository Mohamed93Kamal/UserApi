using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Infrastructure.Services.Categories;
using Orders.Infrastructure.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    
    public class UserController : BaseController
    {
        private readonly IUserService _UserService;
        public UserController(IUserService userService)
        {
            _UserService = userService;
        }
        [HttpGet]
        public IActionResult GetAll(string searchkey)
        {
            var users = _UserService.GetAll(searchkey);
            return Ok(ResponseGet(users));
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Create([FromBody]CreateUserDto dto)
        {
            var savedId =await _UserService.Create(dto);
            return Ok(ResponseGet(savedId));
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            var updatedId = await _UserService.Update(dto);
            return Ok(ResponseGet(updatedId));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var delete =await _UserService.Delete(id);
            return Ok(ResponseGet(delete));
        }
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var category = await _UserService.Get(id);
            return Ok(ResponseGet(category));
        }
    }
}
