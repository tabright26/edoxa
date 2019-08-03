# Azure AD login credentials
& "$PSScriptRoot\cli\az-login.ps1"
& "$PSScriptRoot\cli\az-acr-login.ps1"

# Docker install
& "$PSScriptRoot\cli\docker-compose-pull.ps1"
& "$PSScriptRoot\cli\docker-compose-up.ps1"