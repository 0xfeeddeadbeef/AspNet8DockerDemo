FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

RUN curl -fSL -o /usr/bin/dotnet-dump https://aka.ms/dotnet-dump/linux-x64

COPY *.csproj ./
RUN dotnet restore

COPY . ./
WORKDIR /app

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
COPY --from=build ["/usr/bin/dotnet-dump", "/usr/bin/dotnet-dump"]

EXPOSE 8080

ENV ASPNETCORE_URLS http://*:8080

ENTRYPOINT ["dotnet", "AspNet8DockerDemo.dll"]
