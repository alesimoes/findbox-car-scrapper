# Hexagonal and Clean Architecture by Alexandre Simões Silva
The simplest demo on how to implement a Web Api using .NET Core and MongoDb that protects the business rules from framework dependencies by following the Clean Architecture Principles.

### Running Application
Bring up the latest MongoDB database as well as a nice admin panel that You’ll be able to use to see what is actually happening. To get started, run:

    $ docker-compose up -d


# Critique
This project was designed to be an Exampe of scraper.

The best way to improve the requests is use the same request to get Makers and Models.

There are improvements that need to be done in algorithm that find data in the html file.

There are improvements that need to be done to handle conversion erros.

There are improvements that need to be done to store the pictures, probably save the images in any storage like S3.
Probably enrich the car entity downloading the picture as async to not make the process slow.


# Architecture
## Motivation

Build an architecture where the Application and domain layers are free of
frameworks following the design of hexagonal architecture aligned with the practices of Clean architecture

## Application Layer
The application layer is responsible for connecting the inputs and outputs to the outside world.
This connection is made through the ports (Request and Response).
Use Cases are the ones who orchestrate the business rules within the application layer.

### Use Case
Use Case is responsible for orchestrating the business rules defined for application.
The use case receives Request it executes the business rules triggering the domain.
After the domain is returned, the Use Case provides the return the response to the caller.


## Domain Layer (DDD)
The domain layer follows the DDD Standard using rich entities, Value Objects, Event Sourcing and Domain Service.
This layer is responsible for handling all rules regarding the domain.

### Domain Service
Communication between the Application and the Domain is through a service provided by the domain.

The application must not be able to manipulate entities in written mode without going through a domain service.
This allows decoupling of domain rules by encapsulating calls into domain services, which tend to be more complex.

### Domain Exceptions
Domain exceptions are the domain's way of informing the upper tier that something is wrong, be it data entry or some domain rule that has been broken.

## References
Evans, Eric - Domain Driven Design

Martin C. Robert - Clean Architecture