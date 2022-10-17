using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Models.ViewModels {
    public class FilteringViewModel {
        public FilteringViewModel(string? title, int? category, ref IQueryable<Product> query) {
            this.Title = title;
            this.Category = category;

            if ( title != null ) {
                query = query.Where(p => p.Title.ToLower().Contains(title.ToLower()));
            }

            if ( category != null ) {
                query = query.Where(p => p.ID_Category == category);
            }

            this.Query = query;
        }

        public string? Title { get; set; }
        public int? Category { get; set; }
        public IQueryable<Product> Query { get; private set; }

        public async Task<IList<Product>> ToListAsync() {
            return await Query.ToListAsync();
        }
    }
}
