FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/Barber.App.Api/Barber.App.Api.csproj", "src/Barber.App.Api/"]
COPY ["src/Barber.App.Infrastructure/Barber.App.Infrastructure.csproj", "src/Barber.App.Infrastructure/"]
COPY ["src/Barber.App.Application/Barber.App.Application.csproj", "src/Barber.App.Application/"]
COPY ["src/Barber.App.Domain/Barber.App.Domain.csproj", "src/Barber.App.Domain/"]
RUN dotnet restore "src/Barber.App.Api/Barber.App.Api.csproj"
COPY . .
WORKDIR /src/src/Barber.App.Api
RUN dotnet publish "Barber.App.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Barber.App.Api.dll"]
