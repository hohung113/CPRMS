FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Gateway/Gateway.csproj", "Gateway/"]

RUN dotnet restore "Gateway/Gateway.csproj"

COPY . .

WORKDIR "/src/Gateway"
RUN dotnet build "Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 7107
ENV ASPNETCORE_URLS=http://+:7107
ENTRYPOINT ["dotnet", "Gateway.dll"]