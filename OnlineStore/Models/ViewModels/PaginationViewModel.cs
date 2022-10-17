namespace OnlineStore.Models.ViewModels {
    public class PaginationViewModel {
        public int CurrentPageNumber { get; set; }
        public int TotalPageCount { get; set; }

        public PaginationViewModel(int totalCount, int currentPageNumber, int itemsPerPage) {
            CurrentPageNumber = currentPageNumber;
            TotalPageCount = (int)Math.Ceiling(totalCount/ (double)itemsPerPage);
        }

        public bool HasPrevPage => CurrentPageNumber > 1;
        public bool HasNextPage => CurrentPageNumber < TotalPageCount;
    }
}
