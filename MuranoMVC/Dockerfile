FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApplication10/WebApplication10.csproj", "WebApplication10/"]
RUN dotnet restore "WebApplication10/WebApplication10.csproj"
COPY . .
WORKDIR "/src/WebApplication10"
RUN dotnet build "WebApplication10.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication10.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication10.dll"]
