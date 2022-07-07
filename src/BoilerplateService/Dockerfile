FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    libxml2-utils \
    awscli \
    && rm -rf /var/lib/apt/lists/*
WORKDIR /src
COPY ["BoilerplateService.sln", "."]
COPY ["BoilerplateService/BoilerplateService.csproj", "BoilerplateService/"]
COPY ["BoilerplateService.UnitTest/BoilerplateService.UnitTest.csproj", "BoilerplateService.UnitTest/"]
RUN dotnet restore "BoilerplateService.sln"
COPY . .
WORKDIR "/src/BoilerplateService"
RUN dotnet build "BoilerplateService.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR /src/BoilerplateService.UnitTest
RUN ./run_test_coverage.sh
ENTRYPOINT ["./run_test_coverage.sh"]

FROM build AS publish
RUN dotnet publish "/src/BoilerplateService/BoilerplateService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoilerplateService.dll"]
