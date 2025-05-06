FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Auth.API/Auth.API.csproj", "Auth.API/"]
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Core.CPRMS/Core.CPRMS.csproj", "Core.CPRMS/"]
COPY ["Core.CPRMSServiceComponents/Core.CPRMSServiceComponents.csproj", "Core.CPRMSServiceComponents/"]
COPY ["Core.Customized/Core.Customized.csproj", "Core.Customized/"]
COPY ["Core.DataOperate/Core.DataOperate.csproj", "Core.DataOperate/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["Core.Infrastructure/Core.Infrastructure.csproj", "Core.Infrastructure/"]
COPY ["Core.Utility/Core.Utility.csproj", "Core.Utility/"]

RUN dotnet restore "Auth.API/Auth.API.csproj"

COPY . .

WORKDIR "/src/Auth.API"
RUN dotnet build "Auth.API.csproj" -c Release -o /app/build

# Explicitly copy appsettings.json files to avoid conflicts
FROM build AS publish
COPY ["Auth.API/appsettings.json", "/app/publish/appsettings.json"]
COPY ["Auth.API/appsettings.Development.json", "/app/publish/appsettings.Development.json"]
RUN dotnet publish "Auth.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 7189
ENV ASPNETCORE_URLS=http://+:7189
ENTRYPOINT ["dotnet", "Auth.API.dll"]