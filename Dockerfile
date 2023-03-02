FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "user-service.dll"]