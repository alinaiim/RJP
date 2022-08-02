# RJP

This project was done using the clean architrcture approach

## Layers
There are 4 layers in the project (and one test project)

### Domain Layer
The domain layer is the heart of our application it is where all the business entities(models) and exceptions are present.
The domain layer does not have any dependencies on any other layers, other layers depend on it.

### DAL Layer
The Data Access Layer is the layer that connects us to the database. Within it resides our context class and migration folders.
Also, all the database configurations can be added to this layer.

### Application Layer
The application layer is where we write our app logic, all our usecases classes are in here.
Classes here are services that get injected to our dependency injection container and we use it in our controllers.

### API Layer
The api layer is our presentation layer that exposes our endpoints and calls the services in the Application layer.
It contains dtos to not show the user everything in the business entities (and for better separations of concerns).

### Tests
Some unit tests are done to the services.
I mocked some services using the Moq library and I used the InMemory database provided by EF Core.
I gave each database in each unit test a unique name because if I did not the database will sometimes interfere with different tests.
