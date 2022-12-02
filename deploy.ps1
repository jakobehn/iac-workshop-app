az acr login --name iacworkshop
docker build -t iacworkshop.azurecr.io/infrawebapp:v1 .
docker push iacworkshop.azurecr.io/infrawebapp:v1