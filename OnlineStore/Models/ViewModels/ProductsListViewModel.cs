using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineStore.Models.ViewModels {
    public class ProductsListViewModel {
        public IEnumerable<Product> Result { get; set; }
        public SortingViewModel SortingViewModel { get; set; }
    }
}
