# PortainerClient

Console client for the Portainer written in C# :)
Uses Portainer's API as described [here](https://app.swaggerhub.com/apis/deviantony/Portainer/1.23.0/).

How to use?
Just pull the docker image [binali/portainerctl](https://hub.docker.com/r/binali/portainerctl)  and enjoy.

Can be used in CI/CD.  For example in stack deployment stage.

You are welcome with your contributions :)

For now client supports those operations:
  - SwarmStack:
     - Create
     - Update
     - Inspect
     - List
     - Remove
