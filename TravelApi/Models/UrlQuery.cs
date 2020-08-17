public class UrlQuery
{
    private const int maxPageSize = 100;
    public int? PageNumber { get; set; }

    private int _pageSize = 50;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value < maxPageSize) ? value : maxPageSize;
        }
    }
}