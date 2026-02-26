# Development Report

## 1. Technical Steps & Architecture Decisions

### Architecture Overview

The project follows a layered architecture with clear separation of responsibilities:

- PresentationLayer: HTTP server, routing, request parsing, endpoint classes.

- BusinessLayer: Domain logic (e.g., `MediaManager`, `FavouriteManager`, `RatingManager`, `StatisticsManager`, `UserManager`, `Authenticator`) and Authentication Modules ( `Authentication` folder).
- DataAccessLayer: Repository interfaces and PostgreSQL implementations.
- SharedObjects: DTOs and shared models (e.g. `MediaFilter`).
- Test: Unit Tests
- CommandLine: Utility to define Dependencies and kickstart the program, home to documentation and Integration Tests (non-code parts).

## Key decisions

### Interfaces for boundaries

Interfaces such as `IAuthenticator`, `IRatingRepository`, and other repository abstractions reduce coupling and enable unit testing.

### Exceptiom model for HTTP

Custom API exceptions provide a consistent way to translate domain errors to HTTP responses.

### Dependency Injection in Centralized Files

All Dependencies for Dependency Inejection can be found in the `Dependencies.cs` file in the MediaRatingsPlatform.CommandLine project. This also defines a Database Repository Factory, which allows for choosing between different database implementations (if implemented, this project only includes a postgres backend)

### Explicit routing

Centralized route mapping for clarity and maintainability in the Presentation Layer in `Routes.cs`.

### Forced Interfaces to ensure Guidelines

`IModel` for models ensures DataAccessLayer Repositories only implement one DTO, Endpoint Dependencies need to implement

`IAuth` to ensure Authentication is always actively decided and not forgotten.

### Tight Database Design

The database is designed to cascade on delete and generate identities for id values to prevent breakage on a database level.

## Class Diagram

I made an attempt to generate it with plantuml but gave up after around 1 hour, because it is just incredibly large and didn't turn out so I would be happy. No class diagram this time :(

## 2. Problems Encountered & Solutions

### Routing complexity and memory overhead

**Problem:**
Loading all endpoints upfront felt wasteful and complicated.  

**Solution:** Reworked routing into a switch-based dispatch to avoid eager loading. This should allow for better scalability later on, where different routes can easily be dispatched in seperate threads.

### Testing HTTP parsing

**Problem:** The HTTP request parser was tightly bound to server flow.

**Solution:** Extracted parser into `CreateHttpRequest` behind an interface for direct unit testing.

### Auth integration consistency

**Problem:** Risk of forgetting authentication in endpoints.

**Solution**: Auth dependency enforced at the interface level, so every endpoint must provide auth.

## 3. Unit Test Strategy

### Business Layer first

Core domain logic tested via unit tests because it’s most critical and easiest to validate independently. This has a Test Coverage of almost 100% (97%).

### Boundary behavior

Authentication and routing behavior verified since errors there affect all features.

### AI Generation

A lot of Unit Tests are AI generated and then human reviewed and adjusted to speed up development time

### What is tested and why

**Authentication** (`AuthenticatorTests`): Ensures token creation/validation works since it gates all protected endpoints.

**Managers** (`MediaManagerTests`, `FavouriteManagerTests`, `RatingManagerTests`, `StatisticsManagerTests`, `UserManagerTests`): Validates domain workflows such as CRUD, aggregates, and profile statistics to prevent regressions.

**Routing/Presentation** (`RoutesTest`): Confirms correct endpoint resolution and integration flow.

**Authentication Modules Tests** (`TestAuth`) Authentication Tests, which make sure that access control is enforced (users cannot access media, ratings, user data or comments that they do not own or have access to view)

This focuses coverage on logic where faults are costly (auth, stats, routing), while keeping integration checks in Bruno.

The DAL depends on a running PostgreSQL instance and real schema state, which makes isolated unit tests less useful. Instead, correctness is validated through integration tests via HTTP endpoints and manual verification against the real database to make sure all the data is saved correctly.

## 4. SOLID Principles (with project examples)

### Single Responsibility Principle (SRP)

- `CreateHttpRequest` focuses only on parsing HTTP requests.
- `MediaManager` focuses on media domain logic (validation, orchestration), not HTTP or database details.

### Dependency Inversion Principle (DIP)

- All Business logic depends on abstractions like `IRatingRepository` and `IAuthenticator`, not PostgreSQL classes.
- Enables swapping repositories or mocks in unit tests without changing managers.

## 5. Time Tracking (Summary by Major Tasks)

| Task                                     | Estimated Time | Notes                                 |
|------------------------------------------|---------------:|---------------------------------------|
| Requirements analysis & initial diagrams |             8h | Spec review + early class/DB diagrams |
| HTTP server & routing                    |             8h | Server, routes, and HTTP parsing      |
| Database design & repositories           |             7h | Schema + PostgreSQL repositories      |
| Business logic implementation            |            30h | Managers, auth, media, ratings, stats |
| Testing (unit + HTTP)                    |            16h | Unit tests + HTTP integration checks  |
| Documentation                            |             10h | Protocol + final adjustments          |

Detailed per-day tracking remains below. Git history is part of the documentation, but especially in the beginning not very clean (gets better over time and is a learning for future projects).

# Detailed Development log & Time Tracking from during the project

All the details here are already mentioned above, this is just included for completeness

## 2.10.2025 2h

- Looked at specification
- slightly adjusted the api spec
- started work on the class diagram

## Zwischendurch

- ganz ganz viele Überlegungen was am schlausten wäre und was nicht geht

## 6.10.2025 6h

- Figured out DB uml diagram
- Made a Class Diagram according to the specifications
- Confusion

## 7.10.2025

- Presentation in class

## 21.10.2025 8h

- Implementation HTTP Server
- Server in HttpServer.cs, Objects in HttpObjects.cs
- Add all APIs to the Presentation Layer with http 501 response
- Integration Tests are done through the .http file, a Jetbrains Standard for Testing http endpoints
- The Postman collection is also present and will be updated for the hand in (but I can't be bothered to constantly
  update if I don't use it)
- Added Routing with a single location for all Routes to keep an overview of what is available
- Uploaded old class diagram (forgot where I put the new one)

## Intermediate Hand In

missing:

- Model classes are not yet implemented (-2 points)
- Register/Login is not yet implemented (although structure is documented in class diagram) (-2 points)

Failed because it didn't compile correctly (no idea what exactly went wrong, but also didn't have the time to check)

## 2.1.2026 10h

- Looked into the old files and checked what I already have and what the next steps are
- The Http Server works and forwards to the relevant Endpoints
- Not much more exists yet
- Setup database in container (db.sh script)
- Reimplemented Routes to use a switch statement to reduce memory overhead (not having every endpoint loaded at all
  times)
- Implemented Auth as an IAuth Interface and Implementations, which are included in the IHttpEndpoints Interface
- All endpoints now require an Auth implementation, which makes sure that you can't forget Auth
- Implemented ApiExceptions, which mirror c# Exceptions, but provide a HttpStatusCode, which is returned thrown while
  resolving an Api Call
- Almost Finished implementing the Presentation Layer, just missing a few Functions which pass down to the Business
  Logic Layer
- Had an Idea about using a 4 step process to handle all requests, but disbanded that idea again because it doesn't give
  me any benefits except make code more complicated

## 3.1.2026 8h

- Added Unit Tests for Presentation Layer
- Moved HttpRequest parser into its own wrapper, which implements an interface, so I can run Unit Tests on it

## 4.1.2026 7h

- Added json parsing in Presentation Layer to create Objects which fit the models
- Finished Database Design
- Added Database Connection, Creation and Repository Model in Database

## 5.1.2025 14h

- Added Authentication functionality in Business Logic Layer
- Added Token and User Database Repository Functionality
- Refactored to use Dependency Injection for the Database Repositories via a global static class (horrible, will probably be changed later when the Bll has more of a concept to it)
- Added Tests for implemented Authentication Methods

## 18.2.2025 6h

- Heute und Gestern bisschen Cleanup, damit ich das noch fertig machen kann
- Implemented Updating User Profiles

## 19.2.2025 3h

- Finale checklist erstellt an todos und was noch implementiert werden muss (merge aus Spec und Checklist aus Moodle)
- Added favouritesManager with existing workflow

## 20.2.2025 5h

- Added Media CRUD
- Thoughts about adding genres. Current best idea is arrays in postgres, which are very ugly, but don't require another table (might also do another table because of recommendations)

## 21.2.2025 4h

- Als erstes draufgekommen genres hab ich schon als table in der Datenbank definiert. Daher habe ich das auch so im Code implementiert
- Media fertig

## 22.2.2025 12h

- Added Ratings, Likes, Leaderboards, favourites lists, average ratings, etc.
- Missing: Recommendations, Media Search, personal statistics on user page
- Missing: Tests and Documentation
- Everything else is completed and manually tested
- Workflow and architecture work very well, it is easy to implement new functions :)

## 23.2.2025 4h

- Added profile statistics (total media added, total ratings, average rating)
- Completed Presentation Layer (3 functions are not yet implemented in the business layer)
- Set up ai so I can try and generate some tests and documentation (although writing myself should also be a rather quick option)

## 24.2.2025 5h

- Added Searching and Filtering Media
- Added Recommendations by Genre
- Added Recommendations by Content
- Finished implementing
- Missing: Tests and Documentation :/

## 25.2.2025 10h

- Unit Tests für den Business Layer mit AI generiert und dann angepasst
- Code gefixt wo Unit Tests Fehler aufgezeigt haben
- Unit Tests für die Authentication Methods erweitert und hinzugefügt
- Protokoll AI generiert und dann nochmals komplett überarbeitet
- Versucht Klassendiagramm zu erstellen (nicht geschafft)
- Code nochmals gefixt wo noch Tests fehlgeschlagen haben

## 26.2.2025 4h

- Ausführliche Integration Tests erstellt
- Nochmal fixes dort wo diese fehlschlagen
