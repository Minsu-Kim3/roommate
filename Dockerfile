# Build the ASP.NET Core application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore code/backend/src/RoomMate/RoomMate.csproj
RUN dotnet publish code/backend/src/RoomMate/RoomMate.csproj -c Release -o /app/publish

# Run the application
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "RoomMate.dll"]