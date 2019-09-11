$CERT_MANAGER_VERSION = "0.7"

kubectl apply -f https://raw.githubusercontent.com/jetstack/cert-manager/release-$CERT_MANAGER_VERSION/deploy/manifests/00-crds.yaml

kubectl create namespace cert-manager

kubectl label namespace cert-manager certmanager.k8s.io/disable-validation=true

helm repo add jetstack https://charts.jetstack.io 

helm repo update

helm install --name cert-manager --namespace cert-manager jetstack/cert-manager --version v$CERT_MANAGER_VERSION --wait

"apiVersion: certmanager.k8s.io/v1alpha1
kind: Issuer
metadata:
  name: letsencrypt-staging
  namespace: istio-system
spec:
  acme:
    email: admin@edoxa.gg
    server: https://acme-staging-v02.api.letsencrypt.org/directory
    privateKeySecretRef:
      name: letsencrypt-staging
    http01: {}" | kubectl apply -f -