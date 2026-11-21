using ManagmentSystem.Core.Consts;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.Resources;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static ManagmentSystem.Core.VModels.VMUserProfile;

namespace ManagmentSystem.EF.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IDistributedCache _cache;

        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment env, IStringLocalizer<SharedResource> localizer, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _localizer = localizer;
            _cache = cache;
        }
        //public async Task<(IEnumerable<VMProduct> products, int totalItems)> GetProductsAll(
        //    int page,
        //    int pageSize,
        //    string lang = "ar",
        //    Dictionary<string, string> search = null,
        //    ObjSort sort = null)
        //{
        //    var isArabic = string.Equals(lang, "ar", StringComparison.OrdinalIgnoreCase);

        //    var query =
        //        from p in _context.Productes.AsNoTracking()
        //        join cp in _context.CategoryProductes.AsNoTracking() on p.Id equals cp.ProductId into cpGroup
        //        from cp in cpGroup.DefaultIfEmpty()
        //        join c in _context.Categories.AsNoTracking() on cp.CategoryId equals c.Id into cGroup
        //        from c in cGroup.DefaultIfEmpty()
        //        select new VMProduct
        //        {
        //            Id = p.Id,
        //            AName = p.AName,
        //            EName = p.EName,
        //            Description = p.Description,
        //            Quantity = p.Quantity,
        //            Status = p.Status,
        //            IsDiscount = p.IsDiscount,
        //            CategoryId = c != null ? c.Id : null,
        //            CategoryAName = c != null ? c.AName : null,
        //            CategoryEName = c != null ? c.EName : null,
        //            CategoryName = c != null
        //                ? (isArabic ? (c.AName ?? c.EName) : (c.EName ?? c.AName))
        //                : string.Empty
        //        };

        //    if (search != null)
        //    {
        //        if (search.TryGetValue("aName", out var aName) && !string.IsNullOrWhiteSpace(aName))
        //            query = query.Where(x => x.AName.Contains(aName));

        //        if (search.TryGetValue("eName", out var eName) && !string.IsNullOrWhiteSpace(eName))
        //            query = query.Where(x => x.EName.Contains(eName));

        //        if (search.TryGetValue("description", out var desc) && !string.IsNullOrWhiteSpace(desc))
        //            query = query.Where(x => x.Description != null && x.Description.Contains(desc));

        //        if (search.TryGetValue("categoryId", out var catId) && !string.IsNullOrWhiteSpace(catId))
        //            query = query.Where(x => x.CategoryId == catId);

        //        if (search.TryGetValue("categoryName", out var catName) && !string.IsNullOrWhiteSpace(catName))
        //            query = query.Where(x => x.CategoryName.Contains(catName));

        //        if (search.TryGetValue("quantity", out var qtyStr) && double.TryParse(qtyStr, out var qtyVal))
        //            query = query.Where(x => x.Quantity == qtyVal);

        //        if (search.TryGetValue("status", out var statusVal) && TryParseYesNoBool(statusVal, out var statusBool))
        //            query = query.Where(x => x.Status == statusBool);

        //        if (search.TryGetValue("isDiscount", out var discVal) && TryParseYesNoBool(discVal, out var discBool))
        //            query = query.Where(x => x.IsDiscount == discBool);
        //    }

        //    if (sort != null && !string.IsNullOrWhiteSpace(sort.column))
        //    {
        //        var desc = string.Equals(sort.state, Const.cnstDescending, StringComparison.OrdinalIgnoreCase);
        //        query = sort.column.ToLower() switch
        //        {
        //            "aname" => desc ? query.OrderByDescending(x => x.AName) : query.OrderBy(x => x.AName),
        //            "ename" => desc ? query.OrderByDescending(x => x.EName) : query.OrderBy(x => x.EName),
        //            "description" => desc ? query.OrderByDescending(x => x.Description) : query.OrderBy(x => x.Description),
        //            "quantity" => desc ? query.OrderByDescending(x => x.Quantity) : query.OrderBy(x => x.Quantity),
        //            "categoryname" => desc ? query.OrderByDescending(x => x.CategoryName) : query.OrderBy(x => x.CategoryName),
        //            "status" => desc ? query.OrderByDescending(x => x.Status) : query.OrderBy(x => x.Status),
        //            "isdiscount" => desc ? query.OrderByDescending(x => x.IsDiscount) : query.OrderBy(x => x.IsDiscount),
        //            _ => query.OrderBy(x => x.AName)
        //        };
        //    }
        //    else
        //    {
        //        query = query.OrderBy(x => x.AName);
        //    }

        //    var totalItems = await query.CountAsync();
        //    var items = await query
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return (items, totalItems);
        //}

        public async Task<(IEnumerable<VMProduct> products, int totalItems)> GetProductsAll(
            int page,
            int pageSize,
            string lang = "ar",
            Dictionary<string, string> search = null,
            ObjSort sort = null)
        {
            var isArabic = string.Equals(lang, "ar", StringComparison.OrdinalIgnoreCase);
            var isDesc = sort != null && string.Equals(sort.state, Const.cnstDescending, StringComparison.OrdinalIgnoreCase);

            Expression<Func<Product, bool>> match = BuildProductMatchExpression(search);
            var (orderBy, sortAfterProjection) = BuildProductOrderExpression(sort);

            var safePage = page < 1 ? 1 : page;
            var safePageSize = pageSize < 1 ? 10 : pageSize;
            var skip = (safePage - 1) * safePageSize;

            var (items, totalItems) = await _unitOfWork.Productes.FindByAnyDataProjectedPagedList<Product, VMProduct, object>(
                projection: BuildProductProjection(isArabic),
                match: match,
                orderBy: orderBy,
                orderByDescending: isDesc,
                skip: skip,
                take: safePageSize,
                includes: null
            );

            if (sortAfterProjection)
            {
                items = isDesc
                    ? items.OrderByDescending(x => x.CategoryName).ToList()
                    : items.OrderBy(x => x.CategoryName).ToList();
            }

            return (items, totalItems);
        }
        public async Task<VMProduct> GetProduct(string id, string lang = "ar")
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            var isArabic = string.Equals(lang, "ar", StringComparison.OrdinalIgnoreCase);

            return await _unitOfWork.Productes.FindByAnyDataProjectedFirstOrDefault(
                projection: BuildProductProjection(isArabic),
                match: p => p.Id == id,
                includes: null
            );
        }
        public async Task<int> AddProduct(VMProductPost model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.CategoryId))
                throw new ArgumentException("CategoryId is required when adding a product.");

            var category = await _unitOfWork.Categories.GetByIdAsync(model.CategoryId);
            if (category == null)
                throw new KeyNotFoundException("Selected category does not exist.");

            var product = new Product
            {
                AName = model.AName,
                EName = model.EName,
                Description = model.Description,
                Quantity = model.Quantity,
                Status = model.Status,
                IsDiscount = model.IsDiscount
            };

            await _unitOfWork.Productes.AddAsync(product);

            await _unitOfWork.CategoryProductes.AddAsync(new CategoryProduct
            {
                ProductId = product.Id,
                CategoryId = model.CategoryId,
                AddedOn = DateTime.UtcNow
            });

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateProduct(VMProductPost model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Id))
                throw new ArgumentException("Product Id is required for update.");

            var product = await _unitOfWork.Productes.GetByIdAsync(model.Id);
            if (product == null) return 0;

            if (!string.IsNullOrWhiteSpace(model.CategoryId))
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(model.CategoryId);
                if (category == null)
                    throw new KeyNotFoundException("Selected category does not exist.");
            }

            product.AName = model.AName;
            product.EName = model.EName;
            product.Description = model.Description;
            product.Quantity = model.Quantity;
            product.Status = model.Status;
            product.IsDiscount = model.IsDiscount;
            _unitOfWork.Productes.Update(product);

            if (!string.IsNullOrWhiteSpace(model.CategoryId))
            {
                var existingLinks = await _unitOfWork.CategoryProductes
                    .FindAllAsync(cp => cp.ProductId == product.Id);

                foreach (var link in existingLinks)
                     _unitOfWork.CategoryProductes.Delete(link.Id.ToString());

                await _unitOfWork.CategoryProductes.AddAsync(new CategoryProduct
                {
                    ProductId = product.Id,
                    CategoryId = model.CategoryId,
                    AddedOn = DateTime.UtcNow
                });
            }

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteProduct(string Id)
        {
            try
            {
                var images = _unitOfWork.Images.FindAll(i => i.ProductId == Id);
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

                var Product = _unitOfWork.Productes.Delete(Id);
                if (Product != 0)
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

        private static bool TryParseYesNoBool(string raw, out bool value)
        {
            value = false;
            if (string.IsNullOrWhiteSpace(raw)) return false;

            switch (raw.Trim().ToLowerInvariant())
            {
                case "yes":
                case "true":
                case "1":
                    value = true;
                    return true;
                case "no":
                case "false":
                case "0":
                    value = false;
                    return true;
                default:
                    return false;
            }
        }

        private static Expression<Func<Product, bool>> BuildProductMatchExpression(Dictionary<string, string> search)
        {
            Expression<Func<Product, bool>> predicate = p => true;

            if (search == null) return predicate;

            if (search.TryGetValue("aName", out var aName) && !string.IsNullOrWhiteSpace(aName))
                predicate = CombineAnd(predicate, p => p.AName != null && p.AName.Contains(aName));

            if (search.TryGetValue("eName", out var eName) && !string.IsNullOrWhiteSpace(eName))
                predicate = CombineAnd(predicate, p => p.EName != null && p.EName.Contains(eName));

            if (search.TryGetValue("description", out var desc) && !string.IsNullOrWhiteSpace(desc))
                predicate = CombineAnd(predicate, p => p.Description != null && p.Description.Contains(desc));

            if (search.TryGetValue("categoryId", out var catId) && !string.IsNullOrWhiteSpace(catId))
                predicate = CombineAnd(predicate, p => p.CategoriesProductes.Any(cp => cp.CategoryId == catId));

            if (search.TryGetValue("categoryName", out var catName) && !string.IsNullOrWhiteSpace(catName))
                predicate = CombineAnd(predicate, p => p.CategoriesProductes.Any(cp =>
                    cp.Category != null &&
                    ((cp.Category.AName != null && cp.Category.AName.Contains(catName)) ||
                     (cp.Category.EName != null && cp.Category.EName.Contains(catName)))));

            if (search.TryGetValue("quantity", out var qtyStr) && double.TryParse(qtyStr, out var qtyVal))
                predicate = CombineAnd(predicate, p => p.Quantity == qtyVal);

            if (search.TryGetValue("status", out var statusVal) && TryParseYesNoBool(statusVal, out var statusBool))
                predicate = CombineAnd(predicate, p => p.Status == statusBool);

            if (search.TryGetValue("isDiscount", out var discVal) && TryParseYesNoBool(discVal, out var discBool))
                predicate = CombineAnd(predicate, p => p.IsDiscount == discBool);

            return predicate;
        }

        private static Expression<Func<T, bool>> CombineAnd<T>(
            Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var leftBody = ReplacingExpressionVisitor.Replace(first.Parameters[0], param, first.Body);
            var rightBody = ReplacingExpressionVisitor.Replace(second.Parameters[0], param, second.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftBody, rightBody), param);
        }
        private static (Expression<Func<Product, object>> orderBy, bool sortAfterProjection) BuildProductOrderExpression(ObjSort sort)
        {
            var col = sort?.column?.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(col))
                return (p => (object)p.AName, false);

            return col switch
            {
                "aname" => (p => (object)p.AName, false),
                "ename" => (p => (object)p.EName, false),
                "description" => (p => (object)p.Description, false),
                "quantity" => (p => (object)p.Quantity, false),
                "status" => (p => (object)p.Status, false),
                "isdiscount" => (p => (object)p.IsDiscount, false),
                "categoryname" => (null, true),
                _ => (p => (object)p.AName, false)
            };
        }
        private static Expression<Func<Product, VMProduct>> BuildProductProjection(bool isArabic)
        {
            return p => new VMProduct
            {
                Id = p.Id,
                AName = p.AName,
                EName = p.EName,
                Description = p.Description,
                Quantity = p.Quantity,
                Status = p.Status,
                IsDiscount = p.IsDiscount,
                CategoryId = p.CategoriesProductes
                    .Select(cp => cp.CategoryId)
                    .FirstOrDefault(),
                CategoryAName = p.CategoriesProductes
                    .Select(cp => cp.Category != null ? cp.Category.AName : null)
                    .FirstOrDefault(),
                CategoryEName = p.CategoriesProductes
                    .Select(cp => cp.Category != null ? cp.Category.EName : null)
                    .FirstOrDefault(),
                CategoryName = p.CategoriesProductes
                    .Select(cp => cp.Category != null
                        ? (isArabic
                            ? (cp.Category.AName ?? cp.Category.EName ?? string.Empty)
                            : (cp.Category.EName ?? cp.Category.AName ?? string.Empty))
                        : string.Empty)
                    .FirstOrDefault() ?? string.Empty
            };
        }
    }
}