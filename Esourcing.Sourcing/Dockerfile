#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Esourcing.Sourcing/Esourcing.Sourcing.csproj", "Esourcing.Sourcing/"]
COPY ["EventBusRabbitMq/EventBusRabbitMq.csproj", "EventBusRabbitMq/"]
RUN dotnet restore "Esourcing.Sourcing/Esourcing.Sourcing.csproj"
COPY . .
WORKDIR "/src/Esourcing.Sourcing"
RUN dotnet build "Esourcing.Sourcing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Esourcing.Sourcing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Esourcing.Sourcing.dll"]