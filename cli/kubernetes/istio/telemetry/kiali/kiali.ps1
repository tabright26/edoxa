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
    kubectl -n istio-system delete gateway kiali-gateway
    kubectl -n istio-system delete virtualservice kiali-vs
}

function Start-Local {
    kubectl port-forward -n istio-system $(kubectl get pod -n istio-system -l app=kiali -o jsonpath='{.items[0].metadata.name}') 20001:20001
}