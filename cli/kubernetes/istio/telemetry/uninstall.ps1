# Documentation: https://istio.io/docs/tasks/telemetry/gateways/

# Remove all related Gateways
kubectl -n istio-system delete gateway grafana-gateway kiali-gateway prometheus-gateway tracing-gateway

# Remove all related Virtual Services
kubectl -n istio-system delete virtualservice grafana-vs kiali-vs prometheus-vs tracing-vs

# Remove the gateway certificate
kubectl -n istio-system delete certificate telemetry-gw-cert