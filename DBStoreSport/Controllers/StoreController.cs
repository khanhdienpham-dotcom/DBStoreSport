using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBStoreSport.Models;

namespace DBStoreSport.Controllers
{
    public class StoreController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();

        // GET: Store/Index
        public ActionResult Index(int? categoryId, string keyword, decimal? minPrice, decimal? maxPrice, int page = 1)
        {
            const int pageSize = 6;
            if (page < 1) page = 1;

            var products = db.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                products = products.Where(p => p.ProductName.Contains(keyword)
                                            || (p.Description != null && p.Description.Contains(keyword)));
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if (totalPages == 0) totalPages = 1;
            if (page > totalPages) page = totalPages;

            var model = new StoreHomeViewModel
            {
                Categories = db.Categories.OrderBy(c => c.CategoryName).ToList(),
                Products = products.OrderByDescending(p => p.ProductID)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .Select(p => new ProductCardViewModel
                                   {
                                       ProductID = p.ProductID,
                                       NamePro = p.ProductName,
                                       CategoryName = p.Category.CategoryName,
                                       Price = p.Price,
                                       ImagePro = p.ImageUrl,
                                       ShortDescription = p.Description
                                   })
                                   .ToList(),
                SelectedCategoryId = categoryId,
                SearchTerm = keyword,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }
        public ActionResult AboutStore()
        {
            return View();
        }
    }
}