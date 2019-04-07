# SmogFightClub
Sample project that shows how to build modular monolith

## Technologies
- AspNetCore 
- FluentMigrator - tworzenie / aktualizacja bazy danych
- Autofac

## Architecture documantation
- C4 + Enterprise Architect /Doc/smog_EA.xml
  
## Design patterns
.column50[
- Command - Command Handler
- Mediator - ICommandBus, IEventBus
- Repository
- ValueObject
]
.column50[
- FeatureFolders
- Composition UI
- Events (inversion of control)
- Saga (Automatonymous)
]
