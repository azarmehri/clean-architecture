# clean-architecture
Clean Architecture Solution Template for .NET 6

## Database

install dotnet-ef

``` bash
dotnet tool install --global dotnet-ef
```

``` bash
dotnet ef migrations add "InitialCreate" --project src\Infrastructure --startup-project src\Web --output-dir Persistence\Migrations

dotnet ef migrations bundle --project src\Infrastructure --startup-project src\Web
```