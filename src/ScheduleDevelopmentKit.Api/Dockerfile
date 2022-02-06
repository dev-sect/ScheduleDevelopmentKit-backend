#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ScheduleDevelopmentKit.Api/ScheduleDevelopmentKit.Api.csproj", "src/ScheduleDevelopmentKit.Api/"]
COPY ["src/ScheduleDevelopmentKit.DataAccess/ScheduleDevelopmentKit.DataAccess.csproj", "src/ScheduleDevelopmentKit.DataAccess/"]
COPY ["src/ScheduleDevelopmentKit.Domain/ScheduleDevelopmentKit.Domain.csproj", "src/ScheduleDevelopmentKit.Domain/"]
COPY ["src/ScheduleDevelopmentKit.Modules.Core/ScheduleDevelopmentKit.Modules.Core.csproj", "src/ScheduleDevelopmentKit.Modules.Core/"]
COPY ["src/ScheduleDevelopmentKit.Common/ScheduleDevelopmentKit.Common.csproj", "src/ScheduleDevelopmentKit.Common/"]
RUN dotnet restore "src/ScheduleDevelopmentKit.Api/ScheduleDevelopmentKit.Api.csproj"
COPY . .
WORKDIR "/src/src/ScheduleDevelopmentKit.Api"
RUN dotnet build "ScheduleDevelopmentKit.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScheduleDevelopmentKit.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduleDevelopmentKit.Api.dll"]