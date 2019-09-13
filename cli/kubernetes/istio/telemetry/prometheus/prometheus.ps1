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
    kubectl -n istio-system delete gateway prometheus-gateway
    kubectl -n istio-system delete virtualservice prometheus-vs
}

function Start-Local {
    kubectl -n istio-system port-forward $(kubectl -n istio-system get pod -l app=prometheus -o jsonpath='{.items[0].metadata.name}') 9090:9090
}