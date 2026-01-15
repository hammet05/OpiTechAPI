namespace TrafficAccidentsAPI.Application.Dtos
{
    public record PagedResultDto<T>
    {
        public List<T> Items { get; init; } = new();
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public int TotalCount { get; init; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
