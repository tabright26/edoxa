# Getting Started
1. Installation process
    1. Download [Git](https://git-scm.com/).
    1. Download [Visual Studio 2017](https://visualstudio.microsoft.com/).
    1. Download [Docker Desktop for Windows](https://hub.docker.com/editions/community/docker-ce-desktop-windows/).
    1. Open Git Bash in a folder and type ```git clone https://edoxa.visualstudio.com/eDoxa/_git/eDoxa```.
    1. Click the eDoxa.sln file located at the root of the repository to launch Visual Studio.
    1. Edit the ```.env``` file and copy the IPv4 address of the Ethernet adapter of your local computer into the ```IP_DOCKER_EXTERNAL``` variable (this step is necessary for IdentityServer4 to work in a Docker environment).
    1. Right-click the ```docker-compose``` file and navigate to Properties -> General -> Service URL, and edit the URL with the ```IP_DOCKER_EXTERNAL``` value you just provided in the ```.env``` file.
    1. Finally, click ```F5``` to run the solution.
1.	Software dependencies
    - [SQL Server](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads/)
    - [RabbitMQ](https://www.rabbitmq.com/)
    - [IdentityServer4](http://docs.identityserver.io/en/latest/)
1.	API references
    - [SendGrid](https://sendgrid.com/docs/api-reference/)
    - [Stripe API](https://stripe.com/docs/api/)
    - [Riot Games API](https://developer.riotgames.com/)

# Builds

[![Build Status](https://edoxa.visualstudio.com/eDoxa/_apis/build/status/eDoxa-CI?branchName=master)](https://edoxa.visualstudio.com/eDoxa/_build/latest?definitionId=5&branchName=master)

## Docker

| Service       | Host       | Port |
| :------------ | ---------- | ---: |
| idsrv         | {localurl} | 5000 |
| identity.api  | {localurl} | 5001 |
| cashier.api   | {localurl} | 5002 |
| challenge.api | {localurl} | 5003 |
| web.spa       | localhost  | 5300 |
| web.status    | {localurl} | 5500 |

## Identity Server

Discovery document: {localurl}:5000/.well-known/openid-configuration