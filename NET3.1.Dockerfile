#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY *.cs .
COPY source.jpg .
COPY source_small.jpg .
COPY *.csproj .
ENV TargetFramework=netcoreapp3.1
CMD dotnet run -c Release -- /results