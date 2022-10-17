using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineStore.Models.ViewModels {
    public class ProductsListViewModel {
        public IEnumerable<Product>? Result { get; set; }
        public PaginationViewModel? PaginationViewModel { get; set; }
        public SortingViewModel? SortingViewModel { get; set; }
        public FilteringViewModel? FilteringViewModel { get; set; }
    }
}
