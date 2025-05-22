FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and projects explicitly
COPY ["CPRMS.BE.sln", "./"]
COPY ["tests", "./tests"]
COPY ["src", "./src"]
ENV ASPNETCORE_ENVIRONMENT=Development 

# Restore, build and then testing all in docker
RUN dotnet restore "CPRMS.BE.sln"
RUN dotnet test "CPRMS.BE.sln" --no-restore
#Publish 
RUN dotnet publish "CPRMS.BE.sln" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
EXPOSE 443
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Rms.API.dll"]