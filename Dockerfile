FROM ubuntu:20.04 

WORKDIR /
COPY "PortainerClient/bin/Release/net7.0/linux-x64/publish/PortainerClient" "/usr/bin/portainerctl"

RUN chmod +x /usr/bin/portainerctl
