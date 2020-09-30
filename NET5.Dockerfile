#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/sdk:5.0.100-rc.1 AS build
WORKDIR /src
COPY *.cs .
COPY source.jpg .
COPY source_small.jpg .
COPY *.csproj .
ENV TargetFramework=netcoreapp5.0
CMD dotnet run -c Release -- /results