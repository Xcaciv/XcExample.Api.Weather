FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

COPY XcExample.Api.Weather/bin/Debug/net5.0/publish/ app/
WORKDIR /app
ENTRYPOINT ["dotnet" "XcExample.Api.Weather.dll"]
ENV COMPlus_EnableDiagnostics=0
ENV DOTNET_EnableDiagnostics=0
ENV ASPNETCORE_ENVIRONMENT="Development"