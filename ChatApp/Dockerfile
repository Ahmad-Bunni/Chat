FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChatApp/ChatApp.csproj", "ChatApp/"]
COPY ["ChatApp.Domain/ChatApp.Domain.csproj", "ChatApp.Domain/"]
COPY ["ChatApp.Data/ChatApp.Data.csproj", "ChatApp.Data/"]
RUN dotnet restore "ChatApp/ChatApp.csproj"
COPY . .
WORKDIR "/src/ChatApp"
RUN dotnet build "ChatApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatApp.dll"]