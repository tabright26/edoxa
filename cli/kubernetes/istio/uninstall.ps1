$ISTIO_VERSION = "1.2.5"

helm delete --purge istio
helm delete --purge istio-init
kubectl delete -f istio-$ISTIO_VERSION\install\kubernetes\helm\istio-init\files

Remove-Item istio-$ISTIO_VERSION -Recurse -ErrorAction Ignore
Remove-Item istio-$ISTIO_VERSION.zip -Recurse -ErrorAction Ignore