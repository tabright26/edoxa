# Specify the Istio version that will be leveraged throughout these instructions
$ISTIO_VERSION = "1.2.5"

# Windows
# Use TLS 1.2
[Net.ServicePointManager]::SecurityProtocol = "tls12"
$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest -URI "https://github.com/istio/istio/releases/download/$ISTIO_VERSION/istio-$ISTIO_VERSION-win.zip" -OutFile "istio-$ISTIO_VERSION.zip"
Remove-Item istio-$ISTIO_VERSION -Recurse -ErrorAction Ignore
Expand-Archive -Path "istio-$ISTIO_VERSION.zip" -DestinationPath .

helm delete --purge istio
helm delete --purge istio-init
kubectl delete -f istio-$ISTIO_VERSION\install\kubernetes\helm\istio-init\files

Remove-Item istio-$ISTIO_VERSION -Recurse -ErrorAction Ignore
Remove-Item istio-$ISTIO_VERSION.zip -Recurse -ErrorAction Ignore