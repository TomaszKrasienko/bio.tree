FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY src/bio.tree.server.domain/bio.tree.server.domain.csproj ./bio.tree.server.domain/
COPY src/bio.tree.server.application/bio.tree.server.application.csproj ./bio.tree.server.application
COPY src/bio.tree.server.infrastructure/bio.tree.server.infrastructure.csproj ./bio.tree.server.infrastructure/
COPY src/bio.tree.server.api/bio.tree.server.api.csproj ./bio.tree.server.api/
RUN dotnet restore ./bio.tree.server.api/bio.tree.server.api.csproj -s https://api.nuget.org/v3/index.json 
COPY . ./
RUN dotnet publish ./bio.tree.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "bio.tree.server.api.dll"]

ENV ASPNETCORE_ENVIRONMENT="Development"
ENV TZ="Europe/Warsaw"
EXPOSE 80
EXPOSE 443
