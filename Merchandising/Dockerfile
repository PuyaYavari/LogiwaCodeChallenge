#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Merchandising/Merchandising.csproj", "Merchandising/"]
RUN dotnet restore "Merchandising/Merchandising.csproj"
COPY . .
WORKDIR "/src/Merchandising"
RUN dotnet build "Merchandising.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Merchandising.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN rm -f Merchandising.db
RUN touch Merchandising.db
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Merchandising.dll"]