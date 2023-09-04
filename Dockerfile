# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY UnitOfWork.Api/*.csproj ./UnitOfWork.Api/
COPY UnitOfWork.Application/*.csproj ./UnitOfWork.Application/
COPY UnitOfWork.Domain/*.csproj ./UnitOfWork.Domain/
COPY UnitOfWork.Infrastructure/*.csproj ./UnitOfWork.Infrastructure/
RUN dotnet restore

# copy everything else and build app
COPY UnitOfWork.Api/ ./UnitOfWork.Api/
COPY UnitOfWork.Application/ ./UnitOfWork.Application/
COPY UnitOfWork.Domain/ ./UnitOfWork.Domain/
COPY UnitOfWork.Infrastructure/ ./UnitOfWork.Infrastructure/
RUN dotnet publish -c release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /source/out .
ENTRYPOINT ["dotnet", "UnitOfWork.Api.dll"]