$AKS_RESOURCE_GROUP="kubernetes--rg"
$AKS_CLUSTER_NAME="edoxa"
$ACR_RESOURCE_GROUP="edoxa-dev-rg"
$ACR_NAME="edoxa"

# Get the id of the service principal configured for AKS
$CLIENT_ID=$(az aks show --resource-group $AKS_RESOURCE_GROUP --name $AKS_CLUSTER_NAME --query "servicePrincipalProfile.clientId" --output tsv)

Write-Host $CLIENT_ID

# Get the ACR registry resource id
$ACR_ID=$(az acr show --name $ACR_NAME --resource-group $ACR_RESOURCE_GROUP --query "id" --output tsv)

Write-Host $ACR_ID

# Create role assignment
az role assignment create --assignee $CLIENT_ID --role acrpull --scope $ACR_ID