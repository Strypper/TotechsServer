namespace Intranet.Tests;

public interface IEFMock<T> : IQueryable<T>, IAsyncEnumerable<T>
{
}
