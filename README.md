# UnitOfWokrProject

UnitOfWorkProject is a small CRUD api in order to practice and demonstrate UnitOfWork pattern.
It is created for learning purposes and it is open to any comments and recomendations.

## Running the app

Via Docker

```bash
cd UnitOfWorkProject
docker-compose up -d
```

You can read api documentation at
```
localhost/swagger/index.html
```

Via dotnet CLI

Make sure you have Postgres (any version) installed or via docker container.

```bash
cd UnitOfWorkProject
dotnet run --project ./UnitOfWork.Api/
```

You can read api documentation at
```
localhost:5072/swagger/index.html
```