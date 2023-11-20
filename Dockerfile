FROM bitnami/dotnet-sdk:7 AS build
COPY . /app
WORKDIR /app
RUN dotnet publish -c Release
FROM ubuntu:23.10
WORKDIR /
COPY --from=build "/app/PortainerClient/bin/Release/net7.0/linux-x64/publish/PortainerClient"  "/usr/bin/portainerctl"
RUN chmod +x /usr/bin/portainerctl
