FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Document_API/Document_API.csproj", "Document_API/"]
COPY ["AuthExtension/AuthExtension.csproj", "AuthExtension/"]
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Core.CPRMS/Core.CPRMS.csproj", "Core.CPRMS/"]
COPY ["Core.CPRMSServiceComponents/Core.CPRMSServiceComponents.csproj", "Core.CPRMSServiceComponents/"]
COPY ["Core.Customized/Core.Customized.csproj", "Core.Customized/"]
COPY ["Core.DataOperate/Core.DataOperate.csproj", "Core.DataOperate/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["Core.Infrastructure/Core.Infrastructure.csproj", "Core.Infrastructure/"]
COPY ["Core.Repository/Core.Repository.csproj", "Core.Repository/"]
COPY ["Core.Utility/Core.Utility.csproj", "Core.Utility/"]

RUN dotnet restore "Document_API/Document_API.csproj"

COPY . .

WORKDIR "/src/Document_API"
RUN dotnet build "Document_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Document_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 7127
ENV ASPNETCORE_URLS=http://+:7127
ENTRYPOINT ["dotnet", "Document_API.dll"]