Write-Host "Resolving DNS to Gateway public IP" -ForegroundColor Green
$ipaddress = $(kubectl get service istio-ingressgateway -n istio-system)[1] | ForEach-Object { $_.Split('   ')[9]; }
$query = "[?ipAddress!=null]|[?contains([ipAddress], '$ipaddress')].[id]"
$resid = az network public-ip list --query $query --output tsv
$jsonresponse = az network public-ip update --ids $resid --dns-name edoxa
$externalDns = ($jsonresponse | ConvertFrom-Json).dnsSettings.fqdn
Write-Host "$externalDns is pointing to Cluster public ip $ipaddress"