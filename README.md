# SmogFightClub
Sample project that shows how to build modular monolith

any suggestions contact me https://www.szylhabel.pl/contactme/

## Status
[![.NET](https://github.com/mr0zek/SmogFightClub/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mr0zek/SmogFightClub/actions/workflows/dotnet.yml)

## Architecture documentation
- [C4 documentation](https://github.com/mr0zek/SmogFightClub/blob/master/c4.md)

## Technologies
- AspNetCore 
- FluentMigrator 
- Autofac

## ToDo:
- CRUD module Template
- Integrate with identity server
- Implement delayed commands based on hangfire
  
## Design patterns
- Command - Command Handler
- Mediator - ICommandBus, IEventBus
- Repository
- ValueObject
- FeatureFolders
- Composition UI
- Events (inversion of control)
- Saga (Automatonymous)
