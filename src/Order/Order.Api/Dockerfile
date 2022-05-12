#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Order/Order.Api/Order.Api.csproj", "src/Order/Order.Api/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
COPY ["src/Order/Order.Infrastructure/Order.Infrastructure.csproj", "src/Order/Order.Infrastructure/"]
COPY ["src/Order/Order.Domain/Order.Domain.csproj", "src/Order/Order.Domain/"]
COPY ["src/Order/Order.Application/Order.Application.csproj", "src/Order/Order.Application/"]
RUN dotnet restore "src/Order/Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/src/Order/Order.Api"
RUN dotnet build "Order.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]
