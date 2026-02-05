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
The Infrastructure layer provides implementations of your domain abstractions and cross‑cutting concerns. It references the Domain and Application layers but must not be referenced by them.

### Responsibilities
Data access, repository & unit‑of‑work implementations
Outbox/inbox and transactional messaging
Caching,
Logging
Health Checks

## Presentation Layer
The Presentation layer exposes your application to users/clients . It references the Application layer and maps HTTP requests to commands/queries (CQRS).
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
Configure Serilog sinks + enrichment
- Sends logs to Console and Seq.
- Adds contextual metadata: correlation ID, machine name, thread ID.
- Sets sensible log levels.

Adds correlation ID logging middleware
- Extracts X-Correlation-Id from request headers.
- Falls back to TraceIdentifier if none provided.
- Pushes the correlation ID into Serilog’s log context.
- Ensures every log during a request includes that ID.
  
## Caching
Caching with Redis provides a fast, reliable way to store frequently accessed data in memory, reducing load on backend services and databases. In systems that manage authentication and authorization, Redis is often used to cache roles, permissions, and user access rules so applications can validate a user’s rights without repeatedly querying a slower relational database or identity provider. This dramatically improves request performance and reduces latency for secured endpoints. Redis can also cache query results—such as precomputed views, filtered lists, or expensive lookup operations—allowing the application to respond quickly while offloading read pressure from the underlying data stores. By storing these values with appropriate expiration policies and invalidating them when changes occur (like role updates or permission modifications), Redis helps maintain consistency while ensuring that the system remains highly responsive and scalable.

## Health Checks
Health checks provide a simple and standardised way to monitor the status of an application and its dependencies, such as databases, external APIs, message queues, or file storage. By exposing a dedicated endpoint, the application can report whether it is healthy, degraded, or unhealthy. This makes health checks extremely useful to determine when to route traffic or restart failing services. Ultimately, health checks help improve application reliability, reduce downtime, and enable faster diagnosis of issues in production environments.

## API Versioning
API versioning ensures that changes to an API—especially breaking changes, such as altered request/response formats, removed fields, behavioural changes, or modified validation rules—can be introduced without disrupting existing clients. When an API evolves, new versions allow older integrations to continue functioning while newer consumers adopt the updated contract. In .NET, API versioning helps maintain backward compatibility, simplifies migration, and provides a clear lifecycle for deprecating old endpoints.

### Query parameter
Versioning via query parameters places the version directly in the URL’s query string, such as ?api-version=2.0. This approach is easy to implement, visible to consumers, and works well when versioning needs to be flexible without altering route structures. While simple, it can be less REST‑friendly because it mixes resource identification with metadata.

### Header
Header-based versioning uses a custom or standard header (e.g. api-version: 2.0 or Accept: application/json; version=2.0). This keeps URLs clean and aligns better with REST principles by separating versioning from resource paths. It also provides more flexibility for content negotiation, though it can be less discoverable for developers inspecting requests manually.

### URL
Versioning via the URL path embeds the version directly into the route, such as /api/v2/orders. This is the most common and most easily discoverable approach. It makes versioning explicit and simple to manage across multiple endpoints, but it can lead to route duplication when many endpoints must be versioned simultaneously.

## Transactional Outbox Pattern

## Minimal APIs

# Testing
## Domain Layer Unit Testing

## Application Layer Unit Testing

## Integration Testing

## Functional Testing

## Architecture Testing
