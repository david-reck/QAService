#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["QAService.API/QAService.API.csproj", "QAService.API/"]
COPY ["QAService.Infrastructure/QAService.Infrastructure.csproj", "QAService.Infrastructure/"]
COPY ["QAService.Domain/QAService.Domain.csproj", "QAService.Domain/"]
COPY ["iPas.Infrastructure.EventBus/iPas.Infrastructure.EventBus.csproj", "iPas.Infrastructure.EventBus/"]
COPY ["QAService.RuleEngine/QAService.RuleEngine.csproj", "QAService.RuleEngine/"]
COPY ["iPas.Infrastructure.IntegrationEventLogEF/iPas.Infrastructure.IntegrationEventLogEF.csproj", "iPas.Infrastructure.IntegrationEventLogEF/"]
COPY ["iPas.Infrastructure.EventBusServiceBus/iPas.Infrastructure.EventBusServiceBus.csproj", "iPas.Infrastructure.EventBusServiceBus/"]
RUN dotnet restore "QAService.API/QAService.API.csproj"
COPY . .
WORKDIR "/src/QAService.API"
RUN dotnet build "QAService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QAService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QAService.API.dll"]
