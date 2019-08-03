param([switch]$Elevated)

function IsLocalAdministrator {
    $CurrentUser = New-Object Security.Principal.WindowsPrincipal $([Security.Principal.WindowsIdentity]::GetCurrent())
    $CurrentUser.IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator)
}

if ((IsLocalAdministrator) -eq $false) {
    if ($Elevated -eq $false) {
        Start-Process powershell.exe -Verb RunAs -ArgumentList ('-noprofile -noexit -file "{0}" -elevated' -f ($Myinvocation.MyCommand.Definition))
    }
    exit
}

try {
    Get-NetFirewallRule -DisplayName eDoxa -ErrorAction Stop
    Write-Host "The eDoxa firewall rule for Docker already exists."
}
catch [Exception] {
    New-NetFirewallRule -DisplayName eDoxa -Confirm -Description "eDoxa - Docker Network" -LocalAddress Any -LocalPort 5000-5500 -Protocol TCP -RemoteAddress Any -RemotePort Any -Direction Inbound
}