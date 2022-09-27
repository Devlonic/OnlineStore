using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return RedirectToAction("Index", nameof(ProductsController).Replace(nameof(Controller), ""));
        }
    }
}
