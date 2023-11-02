# ASP.Net Clean Architecture

## About

Playing with clean architecture in ASP.Net web API, trying to keep it simple.

- Hexagonal architecture
- CQRS, MediatR & Fluent validation
- MariaDB persistence, Repository & Unit of work patterns

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

## Enhancements

- Unit tests
- Integration tests
- Secure API ? With API key ?
- Share things in Nugget packages ? (middlewares, mediator behaviours...)
- Add documentation