# Specify the Istio version that will be leveraged throughout these instructions
$ISTIO_VERSION = "1.1.3"

# Make sure to add repo istio.io.
helm repo add istio.io https://storage.googleapis.com/istio-release/releases/$ISTIO_VERSION/charts/

# Make sure to update repo.
helm repo update
 
# Create istio-system namespace.
kubectl create namespace istio-system

#& "$PSScriptRoot\deploy-cert-manager.ps1"

# Install istio-init chart into istio-system namespace.
helm install istio-$ISTIO_VERSION/install/kubernetes/helm/istio-init --name istio-init --namespace istio-system --wait

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

# Install istio chart into istio-system namespace with istio tools.
helm install istio-$ISTIO_VERSION/install/kubernetes/helm/istio --namespace istio-system --wait `
    --set global.controlPlaneSecurityEnabled=true `
    --set mixer.adapters.useAdapterCRDs=false `
    --set grafana.enabled=true `
    --set grafana.security.enabled=true `
    --set tracing.enabled=true `
    --set kiali.enabled=true `
    --set gateways.istio-ingressgateway.sds.enabled=true

# Write-Host "Resolving DNS to Gateway public IP" -ForegroundColor Green
# $ipaddress = $(kubectl get service istio-ingressgateway -n istio-system)[1] | ForEach-Object { $_.Split('   ')[9]; }
# $query = "[?ipAddress!=null]|[?contains([ipAddress], '$ipaddress')].[id]"
# $resid = az network public-ip list --query $query --output tsv
# $jsonresponse = az network public-ip update --ids $resid --dns-name edoxa
# $externalDns = ($jsonresponse | ConvertFrom-Json).dnsSettings.fqdn
# Write-Host "$externalDns is pointing to Cluster public ip $ipaddress"

# Remove-Item istio-$ISTIO_VERSION -Recurse -ErrorAction Ignore
# Remove-Item istio-$ISTIO_VERSION.zip -Recurse -ErrorAction Ignore