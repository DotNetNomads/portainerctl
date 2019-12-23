FROM alpine:3.7
WORKDIR /
COPY PortainerClient/bin/release/netcoreapp3.1/linux-x64/publish/PortainerClient /usr/bin/portainerctl
