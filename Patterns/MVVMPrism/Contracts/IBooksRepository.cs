using Models;

namespace Contracts
{
    public interface  IBooksRepository : IQueryRepository<Book, int>, IUpdateRepository<Book, int>
    {
    }
}
