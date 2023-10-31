# ASP.Net Clean Architecture

## About

Playing with clean architecture in ASP.Net web API, trying to keep it simple.

- Hexagonal architecture
- CQRS
- Anti-corruption layers
- Other cool things

## Infrastructure

Launch MariaDB database

```shell
cd ./infra
docker compose up
```

Generate and apply migrations

```shell
cd ./src/Infrastructure
dotnet ef migrations add MigrationName -o Database/Migrations
dotnet ef database update
```

## TODO

- Unit tests
- Integration tests
- Persistence (Docker): MongoDB ? MySQL ? EF Core ? Unit of work pattern ?
- Secure API ? With API key ?
- Handle configuration (Option Pattern)
- Share things in Nugget packages ? (middlewares, mediator behaviours...)