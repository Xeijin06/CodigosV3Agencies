FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CopaAirlines.AgenciesService.API/CopaAirlines.AgenciesService.API.csproj", "CopaAirlines.AgenciesService.API/"]
RUN dotnet restore "CopaAirlines.AgenciesService.API/CopaAirlines.AgenciesService.API.csproj"
COPY . .
WORKDIR "/src/CopaAirlines.AgenciesService.API"
RUN dotnet build "CopaAirlines.AgenciesService.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CopaAirlines.AgenciesService.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CopaAirlines.AgenciesService.API.dll"]
    