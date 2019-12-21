FROM mcr.microsoft.com/dotnet/core/runtime:2.2.5-bionic

WORKDIR /app

COPY Rustamov.PortainerClient/bin/Release/netcoreapp2.2/publish/ .

RUN echo '#!/bin/bash\n  dotnet /app/Rustamov.PortainerClient.dll "$@"' > /usr/bin/portainerctl && \
    chmod +x /usr/bin/portainerctl