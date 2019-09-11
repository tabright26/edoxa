$ISTIO_VERSION = "1.1.3"

helm delete --purge istio
helm delete --purge istio-init
kubectl delete -f istio-$ISTIO_VERSION\install\kubernetes\helm\istio-init\files
kubectl label namespace default istio-injection-
kubectl delete namespace istio-system
kubectl get crds -o name | Select-String -Pattern 'istio.io' | ForEach-Object { kubectl delete $_ }