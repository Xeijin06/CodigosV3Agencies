FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CopaAirlines.RequestsService.Api/CopaAirlines.RequestsService.Api.csproj", "CopaAirlines.RequestsService.Api/"]
RUN dotnet restore "CopaAirlines.RequestsService.Api/CopaAirlines.RequestsService.Api.csproj"
COPY . .
WORKDIR "/src/CopaAirlines.RequestsService.Api"
RUN dotnet build "CopaAirlines.RequestsService.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CopaAirlines.RequestsService.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CopaAirlines.RequestsService.Api.dll"]