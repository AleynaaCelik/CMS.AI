using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Infrastructure.Services
{
    public class ElasticsearchService : ISearchService<Content>
    {
        private readonly IConfiguration _configuration;
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ElasticsearchService> _logger;

        public ElasticsearchService(IConfiguration configuration, ILogger<ElasticsearchService> logger)
        {
            _logger = logger;
            var url = configuration.GetConnectionString("Elasticsearch");
            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex("contents")
                .EnableDebugMode();

            _elasticClient = new ElasticClient(settings);
            CreateIndexIfNotExists();
        }
        private void CreateIndexIfNotExists()
        {
            var indexExists = _elasticClient.Indices.Exists("contents").Exists;
            if (!indexExists)
            {
                // İndeks yoksa oluşturuyoruz
                var createIndexResponse = _elasticClient.Indices.Create("contents", c => c
                    .Map<Content>(m => m
                        .AutoMap() // Tüm alanları otomatik olarak eşleştir
                        .Properties(p => p
                            .Text(t => t.Name(n => n.Title).Analyzer("standard"))
                            .Text(t => t.Name(n => n.Body).Analyzer("standard"))
                        )
                    )
                );

                if (!createIndexResponse.IsValid)
                {
                    _logger.LogError("Failed to create index: {Error}", createIndexResponse.DebugInformation);
                }
            }
        }

        public async Task<IReadOnlyCollection<Content>> SearchAsync(string query, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            // Mock implementation - return an empty list
            // In a real implementation, we would connect to Elasticsearch
            await Task.Delay(100, cancellationToken); // Simulate network delay
            return new List<Content>().AsReadOnly();
        }

        public async Task IndexDocumentAsync(Content document, CancellationToken cancellationToken = default)
        {
            // Mock implementation - do nothing
            // In a real implementation, we would index the document in Elasticsearch
            await Task.Delay(100, cancellationToken); // Simulate network delay
        }

        public async Task UpdateDocumentAsync(Content document, CancellationToken cancellationToken = default)
        {
            // Mock implementation - do nothing
            // In a real implementation, we would update the document in Elasticsearch
            await Task.Delay(100, cancellationToken); // Simulate network delay
        }

        public async Task DeleteDocumentAsync(string id, CancellationToken cancellationToken = default)
        {
            // Mock implementation - do nothing
            // In a real implementation, we would delete the document from Elasticsearch
            await Task.Delay(100, cancellationToken); // Simulate network delay
        }
    }
}
