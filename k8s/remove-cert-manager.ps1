helm delete --purge cert-manager
kubectl delete ns cert-manager
kubectl get crds -o name | Select-String -Pattern 'certmanager.k8s.io' | ForEach-Object { kubectl delete $_ }