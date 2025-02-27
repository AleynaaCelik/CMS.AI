using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Common.Interfaces
{
    public interface ISearchService<T> where T : class
    {
        Task<IReadOnlyCollection<T>> SearchAsync(string query, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task IndexDocumentAsync(T document, CancellationToken cancellationToken = default);
        Task UpdateDocumentAsync(T document, CancellationToken cancellationToken = default);
        Task DeleteDocumentAsync(string id, CancellationToken cancellationToken = default);
    }


}
