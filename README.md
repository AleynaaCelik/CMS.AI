# AI-Powered Content Management System

This project implements a modern content management system (CMS) with AI capabilities, leveraging microservices architecture, domain-driven design principles, and various cloud-native technologies.

## Features

- **AI-Powered Content Analysis**: Integrate with OpenAI to generate SEO recommendations, keywords, and content improvements
- **Efficient Caching**: Redis implementation for high-performance content delivery
- **Advanced Search**: Elasticsearch integration for full-text and fuzzy search capabilities
- **Multi-language Support**: Content metadata for internationalization
- **Version Control**: Track content history and changes
- **Clean Architecture**: Structured using domain-driven design principles
- **Docker Support**: Containerized services for easy deployment

## Technology Stack

- **.NET 8**: Latest .NET platform for high-performance APIs
- **Entity Framework Core**: ORM for database operations
- **MediatR & CQRS**: Command Query Responsibility Segregation pattern
- **FluentValidation**: Robust input validation
- **Redis**: Distributed caching
- **Elasticsearch**: Advanced search capabilities
- **SQL Server**: Primary database (containerized)
- **Swagger**: API documentation
- **Docker & Docker Compose**: Container orchestration

## Architecture

The solution follows Clean Architecture and DDD principles:

- **Domain Layer**: Core entities, business rules, and interfaces
- **Application Layer**: Commands, queries, DTOs, and business logic
- **Infrastructure Layer**: Implementation of interfaces, database, and external services
- **API Layer**: REST endpoints and controllers

### Project Structure

```
CMS.AI/
├── src/
│   ├── CMS.AI.Domain/
│   ├── CMS.AI.Application/
│   ├── CMS.AI.Infrastructure/
│   └── CMS.AI.Api/
├── tests/
└── docker/
    └── docker-compose.yml
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 or VS Code

### Running the Application

1. Clone the repository:
```bash
git clone https://github.com/AleynaaCelik/CMS.AI.git
cd CMS.AI
```

2. Start the required services using Docker:
```bash
cd docker
docker-compose up -d
```

3. Configure OpenAI API key using user secrets:
```bash
cd ../CMS.AI.Api
dotnet user-secrets init
dotnet user-secrets set "OpenAI:ApiKey" "your-actual-api-key-here"
```

4. Run the application:
```bash
dotnet run
```

5. Access the API at:
   - Swagger UI: https://localhost:7226/swagger
   - API Endpoints: https://localhost:7226/api

### Docker Services

- SQL Server: `localhost:1433`
- Redis: `localhost:6379`
- Elasticsearch: `localhost:9200`
- Kibana: `localhost:5601`

## API Endpoints

- **Content Management**
  - `GET /api/content` - List all content
  - `GET /api/content/{id}` - Get content by ID
  - `POST /api/content` - Create new content
  - `PUT /api/content/{id}` - Update content
  - `DELETE /api/content/{id}` - Delete content
  - `POST /api/content/{id}/metadata` - Add metadata to content

- **AI Features**
  - `POST /api/ai/analyze` - Analyze content
  - `POST /api/ai/generate` - Generate AI content
  - `POST /api/ai/enhance` - Enhance existing content

- **Search**
  - `GET /api/search` - Search content
  - `POST /api/search/index` - Index all content
  - `POST /api/search/index/{id}` - Index specific content

## Future Enhancements

- User authentication and authorization
- Media asset management
- A/B testing capabilities
- Content scheduling and publishing workflows
- Frontend admin panel (React/Angular)

## License

MIT

![WhatsApp Görsel 2025-03-04 saat 23 25 49_e9396d3b](https://github.com/user-attachments/assets/d26b2538-008f-4ba7-bdd2-6dd5ba5f9c25)

![front](https://github.com/user-attachments/assets/478634e0-65db-4f0f-9b10-169898aca20f)

![search](https://github.com/user-attachments/assets/09abe87f-ce59-4541-ae12-95774165240b)

![add-content](https://github.com/user-attachments/assets/cf2ff3bc-6d83-4ee6-9f84-aec373171819)

![AıAnalyses](https://github.com/user-attachments/assets/7ed4b94d-4afb-4eea-8480-31bbe1c8f592)







