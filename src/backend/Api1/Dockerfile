FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Api1
COPY ["./Api1.csproj", "."]
RUN pwd
RUN ls -la /Api1
RUN dotnet restore "Api1.csproj"
COPY . .
WORKDIR "/Api1"
RUN dotnet build "Api1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api1.dll"]
