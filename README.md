Logiwa Assignment
---

Welcome to my Merchandising application!

### Introduction

This is a simple e-commerce merchandising management system that creates a business flow for CRUD transactions in "Product" domain.

### Technologies

#### [SQLite](https://www.sqlite.org/index.html)

An in memory SQLite database is used to store the data.

#### [Docker](https://www.docker.com/)

The application runs inside a Docker Linux container. Docker sets up everything needed for the application on the container start; it sets up the databas and automatically starts the application. This isolates the application from the host OS and makes deployment easier.

#### [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

Used to generate Swagger documentation.

#### [Serilog](https://serilog.net/)

Logging is done using Serilog. Logging is kept simple due to the small size of the application.

#### [xUnit](https://xunit.net/)

Used for unit and integration tests.

#### [Moq](https://github.com/moq/moq4)

Used to mock methods for tests.

### Deployment

You need Docker with Linux containers backend to be able to run the application.

Run `docker-compose build ` to build the containers
and then run `docker-compose up` to start the application.

### Error Handling

In case of any errors an error model is being returned to the client. The error model contains a code and a message.
Message is the message of the problem that caused the error. Code is a unique indicator of any problem and error. In
programs current state error codes are as below:

|Code|Meaning|
|----|-------|
|0|Unknown Error|
|1-99|Internal Server Errors|
|100|Missing Product Title|
|101|Unkown Category|
|102-999|Other Client Related Errors|

### Tests

Solution contains a test project and a sample test but tests could not be done completely due to time constraint.

### TODO

* Detailed unit tests
* Detailed integration tests covering different scenarios
* Code coverage using Cobertura or Coverlet
* Detailed swagger documentation

