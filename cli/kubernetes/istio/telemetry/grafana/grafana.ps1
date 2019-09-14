# https://istio.io/docs/tasks/telemetry/gateways/
function Install-Gateway {
    kubectl apply -f gateway.yaml
}

function Install-VirtualService {
    kubectl apply -f virtualservice.yaml
}

function Install-DestinationRule {
    kubectl apply -f destinationrule.yaml
}

function Install {
    Install-Gateway
    Install-VirtualService
    Install-DestinationRule
}

function Uninstall {
    kubectl -n istio-system delete gateway grafana-gateway
    kubectl -n istio-system delete virtualservice grafana-vs
}

function Start-Local {
    kubectl -n istio-system port-forward $(kubectl -n istio-system get pod -l app=grafana -o jsonpath='{.items[0].metadata.name}') 3000:3000
}