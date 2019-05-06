FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Firefly.AuthManager.Api/Firefly.AuthManager.Api.csproj", "Firefly.AuthManager.Api/"]
RUN dotnet restore "Firefly.AuthManager.Api/Firefly.AuthManager.Api.csproj"
COPY . .
WORKDIR "/src/Firefly.AuthManager.Api"
RUN dotnet build "Firefly.AuthManager.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Firefly.AuthManager.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Firefly.AuthManager.Api.dll"]