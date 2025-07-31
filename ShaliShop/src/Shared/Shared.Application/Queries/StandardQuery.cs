namespace Shared.Application.Queries;

public interface IStandardQuery
{
    string? SearchTerm { get; init; }
    string? SortBy { get; init; }
    bool Descending { get; init; }
    int Page { get; init; }
    int PageSize { get; init; }
}

public class StandardQuery : IStandardQuery
{
    public string? SearchTerm { get; init; }
    public string? SortBy { get; init; } 
    public bool Descending { get; init; } = false;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
 