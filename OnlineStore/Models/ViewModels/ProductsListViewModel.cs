using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineStore.Models.ViewModels {
    public class ProductsListViewModel {
        public IEnumerable<Product> Filtered { get; set; }
    }
}
