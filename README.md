# MB.io - SINFO Test Drive Challenge API (backend)

SINFO Backend API Project for the Mercedes-Benz.io Test Drive Challenge

## Getting Started

This project requires .NET Core 2.0 to build and run.
All the following commands assume you are currently in the root folder of the project.

You can download the .NET Core 2.0 [here](https://www.microsoft.com/net/download/windows).

## Dependencies
Dependency | Version
------------- | -------------
.NET Core SDK | 2.0.0
Swashbuckle.AspNetCore | 2.2.0

## Building the application

```sh
$ cd '.\Mercedes-Benz.io Challenge\'
$ dotnet build 
```

## Getting the API running

```sh
$ cd '.\Mercedes-Benz.io Challenge\'
$ dotnet run
```
That will get the API running on http://localhost:42373
The API Documentation is running on http://localhost:42373/swagger

## Running the tests
The test coverage can be analyzed within Visual Studio - it currently sits at around 63%

```sh
$ cd '.\Unit Test\'
$ dotnet test
```

## Deploying the API
```sh
$ cd '.\Mercedes-Benz.io Challenge\'
$ dotnet publish -o "../Release" --self-contained --runtime win10-x64 (for Win10)
```
Other runtimes can be found [here](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog).
The Application is now ready to be run as a standalone, without requiring any framework on the target machine. It runs by simply executing the resulting .exe file. The default port is 5000.

## API Documentation
The documentation is sitting on http://localhost:42373/swagger


## Assumptions, Exceptions and Notable Remarks
* All API methods that have a vehicle parameter filter have two ways of working: A string parameter (as shown on the documentation) or as an array of strings, for multi-parameter searches. An example of this would be { "fuel": ["gasoline", "hybrid"] } which would return all vehicles with either gasoline or hybrid fuel types.
* The main instance of the manager works on the Singleton design pattern, as a replacement for the lack of a persistance data system (such as a database). This could be solved/changed by using EntityFramework and persisting the objects on your preferred database.
* The Vehicle search endpoint works as a POST to enable the dual use of strings and arrays of string. A GET method could also be done where a single string parameter can be passed on the URL.
* The Polygon endpoint requires that the coordinates be separated in latitude and longitude, both in the same position of the array. 
* The Dealer parameter can be used as a name search or as an ID search.

## Author
- Frederico Santos (frederico.f.santos@tecnico.ulisboa.pt)