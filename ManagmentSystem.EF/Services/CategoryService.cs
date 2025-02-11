using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public CategoryService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task<int> AddCategory(Category category)
        {
            try
            {
                var categoryObj = new Category { EName = category.EName, AName = category.AName, Description = category.Description };
                var Category = await _unitOfWork.Categories.Add(categoryObj);
                if (Category != null)
                {
                    await _unitOfWork.CompleteAsync();
                    return 1;
                } else
                {
                    return 0;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteCategory(string Id)
        {
            try
            {
                var images = _unitOfWork.Images.FindAll(i => i.CategoryId == Id);
                if (images.Any())
                {
                    foreach (var image in images)
                    {
                        var filePath = Path.Combine(_env.WebRootPath, image.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    _unitOfWork.Images.DeleteRange(images);
                    await _unitOfWork.CompleteAsync();
                }
                
                var Category = _unitOfWork.Categories.Delete(Id);
                if (Category != 0)
                {
                    await _unitOfWork.CompleteAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(IEnumerable<Category> categories, int totalItems)> GetCategoriesAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null)
        {
            try
            {
                search ??= new Dictionary<string, string>();
                Expression<Func<Category, bool>> match = u =>
                        (string.IsNullOrEmpty(search.GetValueOrDefault("aName")) || u.AName.Contains(search["aName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("eName")) || u.EName.Contains(search["eName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("description")) || u.Description.Contains(search["description"]));

                int totalItems = await _unitOfWork.Categories.CountAsync(match);
                int countItems = await _unitOfWork.Categories.CountAllAsync();

                int skip = (page - 1) * pageSize;
                int take = pageSize;

                var categories = await _unitOfWork.Categories.FindAllAsync(match, take, skip, sort);
                return (categories, totalItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Category> GetCategory(string Id)
        {
            try
            {
                var Category = await _unitOfWork.Categories.GetByIdAsync(Id);
                return Category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateCategory(Category category)
        {
            try
            {
                var categoryObj = new Category {Id = category.Id, EName = category.EName, AName = category.AName, Description = category.Description };
                _unitOfWork.Categories.Update(categoryObj);
                await _unitOfWork.CompleteAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
