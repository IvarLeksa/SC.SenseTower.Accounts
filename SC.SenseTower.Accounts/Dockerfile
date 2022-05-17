#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SC.SenseTower.Accounts/SC.SenseTower.Accounts.csproj", "SC.SenseTower.Accounts/"]
RUN dotnet restore "SC.SenseTower.Accounts/SC.SenseTower.Accounts.csproj"
COPY . .
WORKDIR "/src/SC.SenseTower.Accounts"
RUN dotnet build "SC.SenseTower.Accounts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SC.SenseTower.Accounts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SC.SenseTower.Accounts.dll"]