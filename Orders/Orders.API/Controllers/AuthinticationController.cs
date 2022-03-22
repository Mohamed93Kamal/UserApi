using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Infrastructure.Services.Authintications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    public class AuthinticationController : BaseController
    {
        private readonly IAuthinticationService _authinticationService;
        public AuthinticationController(IAuthinticationService authinticationService)
        {
            _authinticationService = authinticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            var user =await _authinticationService.Login(dto);
            return Ok(ResponseGet(user));
        }
    }
}
