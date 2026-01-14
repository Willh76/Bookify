## Introduction
Notes from a course on pragmatic clean architecture

Learning architectural best practices will help to improve maintainability and testability and reducing loose coupling. It enforces this through good design.

Design principles include:
- Seperation of concerns - Seperate core domain from external interfaces such as the domain and external interfaces
- Encapsulation - hide information from components from other components and remove influence from other components
- Dependency inversions - allows you to depend on abstractions at compile time and implementations at runtime
- Explicit dependencies - everthing is honest about what it depends apon
- Single responsibility - Components should have one responsibility only
- DRY - Don't Repeat Yourself, reduce duplication of code by encapsulating
- Persistence ignorence - Domain should domain such that it doesn't matter where you persist your entities
- Bounded contexts - group related contexts together
  
## Domain Layer
- Folder per Entity
  - Better represents what is in the folder
- Make entities sealed if they are not being inherited from
- Anemic design objects contain only data, we wish to aim for rich domain objects
- Entity is an object in the domain which satisfies two properties
  - It has an identity
  - It should be continuous
- Abstract class for entity (can only inherit from)
  - init on the setter of ID so it cannot be changed
### Value Objects for Solving Primitive Obsession

### Private setters

### Static Factory Pattern

## Application Layer


## Infrastructure Layer


## Presentation Layer


## Authentication


## Authorization


## Advanced Topics


## Testing


## Other Notes
