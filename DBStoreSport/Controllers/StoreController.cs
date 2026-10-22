using DBSportStore.Models;
using DBStoreSport.Models;
using System.Linq; // Thêm dòng này ở đầu file
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class StoreController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities(); // Thêm dòng này

        public ActionResult Index()
        {
            // Thay đổi hoàn toàn nội dung hàm Index thành:
            var products = db.Products
                .Where(p => p.IsActive)
                .Select(p => new ProductCardViewModel
                {
                    ProductID = p.ProductID,
                    NamePro = p.ProductName,
                    CategoryName = p.Category.CategoryName,
                    Price = p.Price,
                    ImagePro = p.ImageUrl
                })
                .ToList();
            return View(products);
        }

        public ActionResult AboutStore() { return View(); }
    }
}