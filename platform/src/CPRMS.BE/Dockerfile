FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and projects explicitly
COPY ["CPRMS.BE.sln", "./"]
COPY ["tests", "./tests"]
COPY ["src", "./src"]

# Restore and publish
# RUN dotnet restore "CPRMS.BE.sln"
RUN dotnet build "CPRMS.BE.sln"
RUN dotnet publish "CPRMS.BE.sln" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Rms.API.dll"]