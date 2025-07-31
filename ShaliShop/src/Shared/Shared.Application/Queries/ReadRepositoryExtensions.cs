namespace Shared.Application.Queries;

public static class ReadRepositoryExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, IStandardQuery parameter) where T : class
    {
        query = query
            .Skip((parameter.Page - 1) * parameter.PageSize)
            .Take(parameter.PageSize);
        return query;
    }
}