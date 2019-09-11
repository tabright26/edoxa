# Specify the Istio version that will be leveraged throughout these instructions
$ISTIO_VERSION = "1.1.3"

# Windows
# Use TLS 1.2
[Net.ServicePointManager]::SecurityProtocol = "tls12"
$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest -URI "https://github.com/istio/istio/releases/download/$ISTIO_VERSION/istio-$ISTIO_VERSION-win.zip" -OutFile "istio-$ISTIO_VERSION.zip"
Remove-Item istio-$ISTIO_VERSION -Recurse -ErrorAction Ignore
Expand-Archive -Path "istio-$ISTIO_VERSION.zip" -DestinationPath .

# # Copy istioctl.exe to C:\Istio
# New-Item -ItemType Directory -Force -Path "C:\Istio"
# Copy-Item -Path .\istio-$ISTIO_VERSION\bin\istioctl.exe -Destination "C:\Istio\"

# # Add C:\Istio to PATH. 
# # Make the new PATH permanently available for the current User, and also immediately available in the current shell.
# $PATH = [environment]::GetEnvironmentVariable("PATH", "User") + "; C:\Istio\"
# [environment]::SetEnvironmentVariable("PATH", $PATH, "User") 
# [environment]::SetEnvironmentVariable("PATH", $PATH)