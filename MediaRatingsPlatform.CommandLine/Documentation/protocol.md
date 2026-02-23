# Development log

Jan Pleschberger
Github link: https://github.com/Janybanny/MediaRatingsPlatform/

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

USEFUL SQL STATEMENTS

"INSERT INTO table(text, username) VALUES (@text, @username) RETURNING id"

## 19.2.2025 3h

- Finale checklist erstellt an todos und was noch implementiert werden muss (merge aus Spec und Checklist aus Moodle)
- Added favouritesManager with existing workflow

## 20.2.20255h

- Added Media CRUD
- Thoughts about adding genres. Current best idea is arrays in postgres, which are very ugly, but don't require another table (might also do another table because of recommendations)

## 21.2.2025 4h

- Als erstes draufgekommen genres hab ich schon als table in der Datenbank definiert. Daher habe ich das auch so im Code implementiert
- Medien fertig

## 22.2.2025 12h

- Added Ratings, Likes, Leaderboards, favourites lists, average ratings, etc.
- Missing: Recommendations, Media Search, personal statistics on user page
- Missing: Tests and Documentation
- Everything else is completed and manually tested
- Workflow and architecture work very well, it is easy to implement new functions :)

## 23.2.2025

- Added profile statistics (total media added, total ratings, average rating)
- Set up ai so I can try and generate some tests and documentation (although writing myself should also be a rather quick option)