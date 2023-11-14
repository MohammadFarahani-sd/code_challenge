#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj", "Mc2.CrudTest.Presentation/Server/"]
COPY ["Application/Mc2.CrudTest.Application/Mc2.CrudTest.Application.csproj", "Application/Mc2.CrudTest.Application/"]
COPY ["Domain/Mc2.CrudTest.Domain/Mc2.CrudTest.Domain.csproj", "Domain/Mc2.CrudTest.Domain/"]
COPY ["Infrastructure/Mc2.CrudTest.Infrastructure.csproj", "Infrastructure/"]
COPY ["Query/Mc2.CrudTest.Query/Mc2.CrudTest.Query.csproj", "Query/Mc2.CrudTest.Query/"]
RUN dotnet restore "Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj"
COPY . .
WORKDIR "/src/Mc2.CrudTest.Presentation/Server"
RUN dotnet build "Mc2.CrudTest.Presentation.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.Presentation.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Presentation.Server.dll"]