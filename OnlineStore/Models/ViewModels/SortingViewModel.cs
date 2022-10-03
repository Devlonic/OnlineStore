namespace OnlineStore.Models.ViewModels {
    public class SortingViewModel {
        public SortingType TitleSort { get; set; }
        public SortingType PriceSort { get; set; }
        public SortingType CategorySort { get; set; }
        public SortingType CurrentSort { get; set; }
        public bool IsUp { get; set; } = true;

        public SortingViewModel(SortingType current) {
            TitleSort = SortingType.TitleAsc;
            PriceSort = SortingType.PriceAsc;
            CategorySort = SortingType.CategoryAsc;
            IsUp = true;

            if((int)current%2!=0)
                IsUp = false;

            CurrentSort = current switch {
                SortingType.TitleDesc => TitleSort = SortingType.TitleAsc,
                SortingType.PriceAsc => PriceSort = SortingType.PriceDesc,
                SortingType.PriceDesc => PriceSort = SortingType.PriceAsc,
                SortingType.CategoryAsc => CategorySort = SortingType.CategoryDesc,
                SortingType.CategoryDesc => CategorySort = SortingType.CategoryAsc,
                _ => TitleSort = SortingType.TitleDesc,
            };
        }
    }
}
