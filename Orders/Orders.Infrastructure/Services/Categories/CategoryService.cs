using AutoMapper;
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

namespace Orders.Infrastructure.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;
        public CategoryService(OrdersDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetAll(string searchkey)
        {
            var categories = _db.Categories.Where(x => x.Name.Contains(searchkey) || string.IsNullOrWhiteSpace(searchkey)).Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                MealCount = _db.Meals.Count(x => x.CategoryId == x.Id)
            }).ToList();
            return categories;
        }

        public async Task<int> Create(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category.Id;
        }

        public async Task<int> Update(UpdateCategoryDto dto)
        {
            var category =  _db.Categories.SingleOrDefault(x => x.Id == dto.Id);
            if (category == null)
            {
                //Exception
            }
           var updateCategory = _mapper.Map(dto, category);
            _db.Categories.Update(updateCategory);
            _db.SaveChanges();
            return category.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.Id == Id);
            if(category == null)
            {
                //Exception
            }
            category.IsDelete = true;
            _db.Categories.Update(category);
            _db.SaveChanges();
            return category.Id;
        }

        public async Task<CategoryViewModel> Get(int Id)
        {
            var category =  _db.Categories.SingleOrDefault(x => x.Id == Id);
            if (category == null)
            {
                //Exception
            }
           var categoriesVM =  _mapper.Map<CategoryViewModel>(category);
            categoriesVM.MealCount = _db.Meals.Count(x => x.CategoryId == category.Id);
            return categoriesVM;
        }


    }
}
