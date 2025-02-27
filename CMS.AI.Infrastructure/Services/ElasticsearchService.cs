using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Infrastructure.Services
{
    public class ElasticsearchService<T> : ISearchService<T> where T : class
    {
        private readonly IElasticClient _elasticClient;

        public ElasticsearchService(IConfiguration configuration)
        {
            var url = configuration.GetConnectionString("Elasticsearch") ?? "http://localhost:9200";
            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(typeof(T).Name.ToLower());

            _elasticClient = new ElasticClient(settings);
        }

        public async Task<IReadOnlyCollection<T>> SearchAsync(string query, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var searchResponse = await _elasticClient.SearchAsync<T>(s => s
                .From((page - 1) * pageSize)
                .Size(pageSize)
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(fs => fs.Field("*"))
                        .Query(query)
                        .Type(TextQueryType.BestFields)
                        .Fuzziness(Fuzziness.Auto)
                    )
                ),
                cancellationToken);

            if (!searchResponse.IsValid)
            {
                throw new Exception($"Error searching documents: {searchResponse.DebugInformation}");
            }

            return searchResponse.Documents;
        }

        public async Task IndexDocumentAsync(T document, CancellationToken cancellationToken = default)
        {
            var response = await _elasticClient.IndexDocumentAsync(document, cancellationToken);

            if (!response.IsValid)
            {
                throw new Exception($"Error indexing document: {response.DebugInformation}");
            }
        }

        public async Task UpdateDocumentAsync(T document, CancellationToken cancellationToken = default)
        {
            var response = await _elasticClient.UpdateAsync<T>(document, u => u.Doc(document), cancellationToken);

            if (!response.IsValid)
            {
                throw new Exception($"Error updating document: {response.DebugInformation}");
            }
        }

        public async Task DeleteDocumentAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = await _elasticClient.DeleteAsync<T>(id, cancellationToken);

            if (!response.IsValid)
            {
                throw new Exception($"Error deleting document: {response.DebugInformation}");
            }
        }
    }
}
