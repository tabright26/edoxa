# Specify the Istio version that will be leveraged throughout these instructions
$ISTIO_VERSION = "1.2.5"

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

# # Create istio-system namespace.
kubectl create namespace istio-system

Write-Host "Make sure you have a service account with the cluster-admin role defined for Tiller..." -ForegroundColor Green

kubectl apply -f istio-$ISTIO_VERSION/install/kubernetes/helm/helm-service-account.yaml

Write-Host "Install Tiller with the service account..." -ForegroundColor Green

helm init --service-account tiller

Start-Sleep -Seconds 30

Write-Host "Install the istio-init chart to bootstrap all the Istio’s CRDs..."

helm install istio-$ISTIO_VERSION/install/kubernetes/helm/istio-init --name istio-init --namespace istio-system `
    --set certmanager.enabled=true

Write-Host "Verify that 28 Istio CRDs were installed to the Kubernetes cluster..."

if ((kubectl get crds | Select-String -Pattern 'istio.io|certmanager.k8s.io' | Measure-Object -Line).Lines -eq 28) {
    Write-Host "The Istio CRDs have been installed correctly in the cluster." -ForegroundColor Green
}
else {
    Write-Host "Warning: The Istio CRDs have not been installed correctly in the cluster!" -ForegroundColor Yellow
}

Start-Sleep -Seconds 5

Write-Host "Apply grafana secret..." -ForegroundColor Green

# Apply grafana secret.
$GRAFANA_USERNAME = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes("grafana"))
$GRAFANA_PASSPHRASE = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes("uP&^3uZ_9DCxKPv6=zV_"))

"apiVersion: v1
kind: Secret
metadata:
  name: grafana
  namespace: istio-system
  labels:
    app: grafana
type: Opaque
data:
  username: $GRAFANA_USERNAME
  passphrase: $GRAFANA_PASSPHRASE" | kubectl apply -f -

Write-Host "Apply kiali secret..." -ForegroundColor Green

# Apply kiali secret.
$KIALI_USERNAME = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes("kiali"))
$KIALI_PASSPHRASE = [Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes("_GqGDmXX^=c?Fe%DRB4%"))

"apiVersion: v1
kind: Secret
metadata:
  name: kiali
  namespace: istio-system
  labels:
    app: kiali
type: Opaque
data:
  username: $KIALI_USERNAME
  passphrase: $KIALI_PASSPHRASE" | kubectl apply -f -

Start-Sleep -Seconds 15

Write-Host "Installing the istio chart..." -ForegroundColor Green

# Install istio-init chart into istio-system namespace.
helm install istio-$ISTIO_VERSION/install/kubernetes/helm/istio --name istio --namespace istio-system `
    --set gateways.enabled=true `
    --set gateways.istio-ingressgateway.enabled=true `
    --set gateways.istio-ingressgateway.sds.enabled=true `
    --set certmanager.enabled=true `
    --set certmanager.email=admin@edoxa.gg `
    --set grafana.enabled=true `
    --set kiali.enabled=true `
    --set prometheus.enabled=true `
    --set tracing.enabled=true

Start-Sleep -Seconds 30

Write-Host "The services of istio-system namespace:" -ForegroundColor Green

kubectl get svc -n istio-system

Write-Host "The pods of istio-system namespace:" -ForegroundColor Green

kubectl get pods -n istio-system

Start-Sleep -Seconds 30

Write-Host "The external IP address of the istio-ingressgateway service:" -ForegroundColor Green

kubectl -n istio-system get service istio-ingressgateway