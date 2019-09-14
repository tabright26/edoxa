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
    kubectl -n istio-system delete gateway tracing-gateway
    kubectl -n istio-system delete virtualservice tracing-vs
}