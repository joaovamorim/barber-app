#!/bin/bash

# criar solution e projetos (execute no diretório raiz do código)
dotnet new sln -n barber.app

mkdir -p src src/Barber.App.Domain src/Barber.App.Application src/Barber.App.Infrastructure src/Barber.App.Api

# criar projects (ou use dotnet new manualmente)
dotnet new classlib -n Barber.App.Domain -o src/Barber.App.Domain
dotnet new classlib -n Barber.App.Application -o src/Barber.App.Application
dotnet new classlib -n Barber.App.Infrastructure -o src/Barber.App.Infrastructure
dotnet new webapi   -n Barber.App.Api -o src/Barber.App.Api

# adicionar referências entre projetos
dotnet add src/Barber.App.Application/Barber.App.Application.csproj reference src/Barber.App.Domain/Barber.App.Domain.csproj
dotnet add src/Barber.App.Infrastructure/Barber.App.Infrastructure.csproj reference src/Barber.App.Application/Barber.App.Application.csproj
dotnet add src/Barber.App.Infrastructure/Barber.App.Infrastructure.csproj reference src/Barber.App.Domain/Barber.App.Domain.csproj
dotnet add src/Barber.App.Api/Barber.App.Api.csproj reference src/Barber.App.Infrastructure/Barber.App.Infrastructure.csproj

# adicionar projects à solution
dotnet sln barber.app.sln add src/Barber.App.Domain/Barber.App.Domain.csproj
dotnet sln barber.app.sln add src/Barber.App.Application/Barber.App.Application.csproj
dotnet sln barber.app.sln add src/Barber.App.Infrastructure/Barber.App.Infrastructure.csproj
dotnet sln barber.app.sln add src/Barber.App.Api/Barber.App.Api.csproj
