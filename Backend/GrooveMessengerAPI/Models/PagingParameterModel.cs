namespace GrooveMessengerAPI.Models
{
    public class PagingParameterModel
    {
        private const int maxPageSize = 20;

        public int pageNumber { get; set; } = 1;

        public int _pageSize { get; set; } = 10;

        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }

        public string SearchKey { get; set; } = null;
    }
}