# Getting Started

## Installation

1. Download [Git](https://git-scm.com/)
1. Download [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?view=azure-cli-latest)
1. Download [Docker Desktop for Windows](https://hub.docker.com/editions/community/docker-ce-desktop-windows/)
1. Download [Node.js](https://nodejs.org/)
1. Download [Visual Studio Code](https://code.visualstudio.com/)

Visual Studio Code extensions (required):

- Docker
- PowerShell
- ESLint
- Prettier
- Debugger for Chrome

<strong>Note:</strong>
For now, the backend is developed using `Google Chrome` development tools. To avoid unnecessary compatibility issues during the development process, `Google Chrome` is recommended as the default debugger.

---

### Step 1: Clone the eDoxa git repository:

<small>Note: Use git bash only for this step.</small>

```sh
git clone https://edoxa.visualstudio.com/eDoxa/_git/eDoxa.Web.Spa
```

---

<strong>Note: Use the built-in VSCode Powershell Console to perform all the following steps.</strong>

### Step 2: Depending on your Docker installation, do one of the following:

#### Docker Desktop for Windows (Hyper-V)

Run the following command to obtain the local IPv4 address of your computer:

```
ipconfig
```

<small>Note: Choose IPv4 with a default gateway (Ethernet or Wi-Fi).</small>

---

### Step 3: Update the local IP references in the .env file with your `IPv4 Address`.

If you have a problem configuring the front-end environment with your local address, you can use `localhost` as the `HOST` variable in the `.env` file. The ip part of the `REACT_APP_REDIRECT_URI` variable must also be changed to `localhost`. DO NOT modify other variables in the `.env` file.

---

### Step 4: Install eDoxa development environment:

This script uses docker-compose to pull the Docker images from the Azure container registry, and then start them as a detach process on your local computer:

```ps
.\docker-compose
```

| Service              | Host                        | Port | Description |
| :------------------- | --------------------------- | :--: | :---------: |
| mssql                | IPv4 Address                | 1433 |     ---     |
| redis                | IPv4 Address                | 6379 |     ---     |
| rabbitmq             | IPv4 Address                | 5672 |     ---     |
| idsrv                | IPv4 Address                | 5000 |     ---     |
| identity.api         | IPv4 Address                | 5001 |     ---     |
| cashier.api          | IPv4 Address                | 5002 |     ---     |
| arena.challenges.api | IPv4 Address                | 5003 |     ---     |
| web.aggregator       | IPv4 Address                | 5100 |     ---     |
| web.gateway          | IPv4 Address                | 5200 |     ---     |
| web.spa              | IPv4 Address or `localhost` | 5300 |     ---     |
| web.status           | IPv4 Address                | 5500 |     ---     |

---

### Step 5: Install the React npm packages dependencies:

```
npm install
```

---

### Step 6: Launch the React project:

```
npm start
```

# Build

No build.

# Development

<strong>IdentityServer credential</strong><br>
Email: admin@edoxa.gg<br>
Password: Pass@word1<br>
UserId: e4655fe0-affd-4323-b022-bdb2ebde6091

<strong>SQL Server credential</strong><br>
Server Name: 127.0.0.1,1433<br>
Authentication: SQL Server Authentication<br>
Username: sa<br>
Password: fnU3Www9TnBDp3MA

# Testing

Use the following command to start project test suites:

```
npm test
```

# Documentation

| Dependencies     | References                                                       |
| :--------------- | ---------------------------------------------------------------- |
| React            | https://reactjs.org/docs/getting-started                         |
| React-Router     | https://reacttraining.com/react-router/web/example/basic         |
| React-Redux      | https://redux.js.org/                                            |
| React-Bootstrap  | https://react-bootstrap.github.io/                               |
| Create React App | https://facebook.github.io/create-react-app/docs/getting-started |
| Jest             | https://jestjs.io/docs/en/getting-started                        |
| OpenIdConnect    | https://www.npmjs.com/package/oidc-client                        |
|                  | https://www.npmjs.com/package/react-oidc                         |
|                  | https://reacttraining.com/react-router/web/example/auth-workflow |
