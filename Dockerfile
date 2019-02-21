FROM microsoft/dotnet:sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY /. .
RUN dotnet restore

# copy and build app and libraries
RUN dotnet publish -c Release -o out




FROM microsoft/dotnet:2.2-runtime-deps-stretch-slim AS runtime
WORKDIR /app
COPY --from=build /app/FileToFolder/out ./
ENTRYPOINT ["dotnet", "FileToFolder.dll"]