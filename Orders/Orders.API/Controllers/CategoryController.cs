using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Infrastructure.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAll(string searchkey)
        {
            var categories = _categoryService.GetAll(searchkey);
            return Ok(ResponseGet(categories));
        }
        [HttpPost]
        public IActionResult Create([FromBody]CreateCategoryDto dto)
        {
            var savedId = _categoryService.Create(dto);
            return Ok(ResponseGet(savedId));
        }
        [HttpPut]
        public IActionResult Update(UpdateCategoryDto dto)
        {
            var updatedId = _categoryService.Update(dto);
            return Ok(ResponseGet(updatedId));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var delete = _categoryService.Delete(id);
            return Ok(ResponseGet(delete));
        }
        [HttpGet]
        public IActionResult Get(int id)
        {
            var category = _categoryService.Get(id);
            return Ok(ResponseGet(category));
        }
    }
}
