FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Document.API/Document.API.csproj", "Document.API/"]
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Core.CPRMS/Core.CPRMS.csproj", "Core.CPRMS/"]
COPY ["Core.CPRMSServiceComponents/Core.CPRMSServiceComponents.csproj", "Core.CPRMSServiceComponents/"]
COPY ["Core.Customized/Core.Customized.csproj", "Core.Customized/"]
COPY ["Core.DataOperate/Core.DataOperate.csproj", "Core.DataOperate/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["Core.Infrastructure/Core.Infrastructure.csproj", "Core.Infrastructure/"]
COPY ["Core.Utility/Core.Utility.csproj", "Core.Utility/"]

RUN dotnet restore "Document.API/Document.API.csproj"

COPY . .

WORKDIR "/src/Document.API"
RUN dotnet build "Document.API.csproj" -c Release -o /app/build

# Explicitly copy appsettings.json files to avoid conflicts
FROM build AS publish
COPY ["Document.API/appsettings.json", "/app/publish/appsettings.json"]
COPY ["Document.API/appsettings.Development.json", "/app/publish/appsettings.Development.json"]
RUN dotnet publish "Document.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 7190
ENV ASPNETCORE_URLS=http://+:7190
ENTRYPOINT ["dotnet", "Document.API.dll"]