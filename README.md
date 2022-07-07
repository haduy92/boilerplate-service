# Boilerplate service #

This service is responsible for providing all boilerplate related information

### Table of Contents ###

- [Boilerplate service](#boilerplate-service)
    - [Table of Contents](#table-of-contents)
  - [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
    - [Installing](#installing)
  - [Running the tests](#running-the-tests)
    - [Viewing Test Coverage](#viewing-test-coverage)
      - [Running tests in command line (locally):](#running-tests-in-command-line-locally)
      - [Running tests in container (devops):](#running-tests-in-container-devops)
  - [Built With](#built-with)

## Getting Started ##

Clone the project - `git clone git@bitbucket.org:ha_duy/boilerplate-service.git`

## Prerequisites ##

- Visual Studio 2017, Rider, VSCode
- .NET 6.0 SDK ([Install](https://dotnet.microsoft.com/en-us/download/dotnet/6.0))
- Docker ([Install](https://docs.docker.com/engine/install/))
- AWS CLI ([Install](https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html))

### Installing ###

Set up your AWS CLI installation
```
aws configure
AWS Access Key ID [None]: my_access_key
AWS Secret Access Key [None]: my_secret_key
Default region name [None]: us-west-2
Default output format [None]:
```

Navigate to `/src` folder
To start, run:
```
docker-compose up --build -d
```

Visit API documentation:
```
http://localhost:7001/swagger/index.html
```

To stop, run
```
docker-compose down
```

Database migration

```
dotnet ef migrations add <NAME>
```

## Running the tests ##

Run all tests from Test explorer

### Viewing Test Coverage ###
We are using Coverlet + ReportGenerator for test coverage
First, install report generator tool
```
dotnet tool install --global dotnet-reportgenerator-globaltool
```

#### Running tests in command line (locally): ####
1. Run script file with command `powershell ./run_test_coverage.ps1` for Windows or `sh run_test_coverage.sh` for linux/macOS located in `BoilerplateService.UnitTest` folder
2. This will generate a `.coverage-report` folder at current working directory where it will contain all the coverage files
3. Open `index.html` file in browser to see the project test coverage
4. To update line coverage threshold, edit `THRESHOLD` variable in `run-test-coverage` files

#### Running tests in container (devops): ####
Move to ```src``` folder
```
docker-compose build --no-cache
```

Coverage reports are kept in /var/coverage_report folder in the container. Create a {localFolder} and mount it to the container.
eg: ```docker run -v C:/Projects/coverage_report:/var/coverage_report testcoverage:latest```

## Built With

* [.NET 6.0](https://github.com/dotnet/aspnetcore/releases/tag/v6.0.5) - The web framework used
* [Docker](https://www.docker.com/) - Container engine
* [xUnit](https://xunit.github.io/) - Unit test
* [FluentAssertion](https://fluentassertions.com/) - Assertion test library
* [NSubstitute](https://github.com/nsubstitute/NSubstitute) - Mocking framework
* [Swagger](https://swagger.io/) - API documentation generation library
* [dotnet-reportgenerator-globaltool](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool) - Test coverage tool