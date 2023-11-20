FROM bitnami/dotnet-sdk:7 AS build
COPY . /tmp
WORKDIR /tmp
RUN dotnet publish -c Release
FROM ubuntu:23.10
WORKDIR /app
COPY --from=build "tmp/PortainerClient/src/UltraMafia/bin/Release/net7.0/linux-x64/publish/PortainerClient"  "/usr/bin/portainerctl"
RUN chmod +x /usr/bin/portainerctl
