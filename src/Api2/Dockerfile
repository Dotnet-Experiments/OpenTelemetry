FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Api2
COPY ["./Api2.csproj", "."]
RUN pwd
RUN ls -la /Api2
RUN dotnet restore "Api2.csproj"
COPY . .
WORKDIR "/Api2"
RUN dotnet build "Api2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api2.dll"]
