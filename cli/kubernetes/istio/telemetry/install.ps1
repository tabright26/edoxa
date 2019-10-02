# Documentation: https://istio.io/docs/tasks/telemetry/gateways/

Write-Host "Applying telemetry addons certificate..." -ForegroundColor Green

kubectl apply -f ./certificate.yaml

Start-Sleep -Seconds 30

Write-Host "Installing Grafana..." -ForegroundColor Green

kubectl apply -f ./addons/grafana.yaml

Write-Host "Installing Kiali..." -ForegroundColor Green

kubectl apply -f ./addons/kiali.yaml

Write-Host "Installing Rrometheus..." -ForegroundColor Green

kubectl apply -f ./addons/prometheus.yaml

Write-Host "Installing Tracing..." -ForegroundColor Green

kubectl apply -f ./addons/tracing.yaml