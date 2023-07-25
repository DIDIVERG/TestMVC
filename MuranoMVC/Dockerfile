FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MuranoMVC/MuranoMVC.csproj", "MuranoMVC/"]
RUN dotnet restore "MuranoMVC/MuranoMVC.csproj"
COPY . .
WORKDIR "/src/MuranoMVC"
RUN dotnet build "MuranoMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MuranoMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MuranoMVC.dll"]
CMD ["bash", "-c", "sleep 10s && dotnet ef database update && dotnet MuranoMVC.dll"]
