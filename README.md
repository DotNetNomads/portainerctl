# PortainerClient (portainerctl)
License: MIT  
Console client for the Portainer written in C# :)
## Main information
Uses Portainer's API as described [here](https://app.swaggerhub.com/apis/deviantony/Portainer/1.23.0/).

How to use?
Just pull the docker image [dotnetnomads/portainerctl](https://hub.docker.com/r/dotnetnomads/portainerctl)  and enjoy.

Can be used in CI/CD.  For example in stack deployment stage.

You are welcome with your contributions :)

For now client supports those operations:
  - SwarmStack:
     - Create
     - Update
     - Inspect
     - List
     - Remove
## Dependencies
- McMaster.Extensions.CommandLineUtils - CMD commands API.
- RestSharp - REST API requests.
- YamlDotNet - used for pretty-print data.
## TODO
The list order doesn't provide information about priority :)
 - [ ] CI/CD
 - [ ] Windows / Chocolatey integration.
 - [ ] Linux / apt/yum package.
 - [ ] macOS / brew integration.
 - [ ] Documentation :)
 - [ ] Integration tests
 - [ ] Implement more APIs :)

