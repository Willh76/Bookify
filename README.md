# Introduction
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

# Architecture Layers
## Domain Layer
### Domain Entities
- Folder per Entity
  - Better represents what is in the folder
- Make entities sealed if they are not being inherited from
- Anemic design objects contain only data, we wish to aim for rich domain objects
- Entity is an object in the domain which satisfies two properties
  - It has an identity
  - It should be continuous
- Abstract class for entity (can only inherit from)
  - init on the setter of ID so it cannot be changed
- Value Objects for Solving Primitive Obsession
  - Begins to build rich domain model
  - Shared can be moved into shared folder (Money)
  - Add a record to represent your value object
    - Allows structural equality
  - Replace strings
  - Can create lists of items such as currencies
    - Define methods to get currencies or to sum currencies
    - Devine an internal currency that cannot be accessed out of domain for None
- Private setters are used as properties should not be changable outside of the scope of the model

### Static Factory Pattern
- Make constructor private
- Make a static factory for creating entity
- Call the constructor from the static factory
- Create Id in the factory
- Hides constructor which may not want to be exposed
- Increased encapsulation
- Allows the implementation of Domain events

### Domain Events
- Implement with MediatR.Contracts
- INotification interface implemented
  - Pubish notifications to the domain
- Update Entity class to Read, clear and Raise domain events
- Events folder to hold the events in records
  - Takes an ID from the entity   
- Raise event from your static pattern
- Can be subscribed to to trigger events such as email users
  
### Repositories and Unit of Work
- Required for a rich domain model
- Repository pattern can be used for adding an entity to the repository
  - Sits inside the entity folder
- Unit of work is used for persisting the entity
  - Sits inside abstractions
    
### Domain Service
- For where calculations/methods don't belong on your entities

### Result and Error Classes
- Result and error classes made in abstractions folder
- Allows for a defensive design
- Removes checks from the application layer and shifts them into the domain within our entities
- Returns a descriptive result item of success or an error with a code showing which business rule was broken

## Application Layer
### CQRS (Command Query Responsibility Segregation)
- Seperate writing and reading data into seperate objects
- Single responsibility is followed
- Improves testability
- Having only a handle method helps with interface segregation
- Decorator patterns can be used for cross cutting concerns
- Loose coupling
- Can cause indirection

<img width="1092" height="885" alt="image" src="https://github.com/user-attachments/assets/6c3a69b3-750c-458c-a97f-0ef9010eec1f" />

### Queries
- IQuery Enforces a type of result to have either success or failure

## Infrastructure Layer

## Presentation Layer

# Authentication and Authorization
Authentication verifies who you are, while authorization determines what you’re allowed to do.

## Authentication
Authentication is the process of verifying who a user is.

## Authorization
### Role Based Authorization
Access is determined by the role assigned to a user
Each role has a predefined set of permissions. If a user has a role, they automatically inherit all the permissions associated with that role.

### Permission Based Authorization
Access is granted based on individual permissions rather than roles.
Users (or roles) are assigned specific permissions, often very granular.

### Resource Based Authorization
Access decisions are made based on the specific resource being accessed and the user’s relationship to that resource.
Authorization checks occur at the object/resource level using Id to check if the user created the booking

# Advanced Topics
## Logging

## Caching

## Health Checks

## API Versioning

## Transactional Outbox Pattern

## Minimal APIs

# Testing
## Domain Layer Unit Testing

## Application Layer Unit Testing

## Integration Testing

## Functional Testing

## Architecture Testing
