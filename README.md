# SmogFightClub
Sample project that shows how to build modular monolith

any suggestions contact me https://www.szylhabel.pl/contactme/

## Status
[![.NET](https://github.com/mr0zek/SmogFightClub/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mr0zek/SmogFightClub/actions/workflows/dotnet.yml)
[![Coverage Status](https://coveralls.io/repos/github/mr0zek/SmogFightClub/badge.svg?branch=master)](https://coveralls.io/github/mr0zek/SmogFightClub?branch=master)

## Architecture documentation
- [C4 documentation](https://github.com/mr0zek/SmogFightClub/blob/master/c4.md)

## Technologies
- AspNetCore 
- FluentMigrator 
- Autofac
- HangFire

## Covered Areas
- time triggers
- trace

## ToDo:
- CRUD module Template
- Integrate with identity server
- Implement delayed commands based on hangfire
- component diagrams generation using PlantUml
- sequence diagram generation using PlantUml
- integrate with external services
  
## Design patterns
- Command - Command Handler
- Mediator - ICommandBus, IEventBus
- CorrelationId
- Repository
- ValueObject
- FeatureFolders
- Composition UI
- Events (inversion of control)
- Saga (Automatonymous)
