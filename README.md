# k8s-playground
Playground of .NET 6 and Kubernetes in Docker Desktop.

# Installation steps
### Build docker images
```
docker build -t greeting-api src\GreetingApi
docker build -t greeting-grpc-service src\GreetingGrpcService
docker build -t greeting-rest-service src\GreetingRestService
```
### Launch deployment script
```
cd deploy
deploy-all.bat
```
### Verify nginx ingress
```
kubectl get service -n ingress-nginx
```
You should see, that `ingress-nginx-controller` has `external-ip`:
```
NAME                                 TYPE           CLUSTER-IP      EXTERNAL-IP   PORT(S)                      AGE
ingress-nginx-controller             LoadBalancer   10.99.153.90    localhost     80:31014/TCP,443:31676/TCP   116s
ingress-nginx-controller-admission   ClusterIP      10.102.62.193   <none>        443/TCP                      116s
```
If value is `pending`, quit Docker Desktop and reopen it (not restart!).
### Verify application
Navigate to http://kubernetes.docker.internal/ and you should see `Greeting Api is running.` message. If IIS screen appears, stop IIS from command line (requires admin privileges):
```
iisreset /stop
```
Check HTTP communication: http://kubernetes.docker.internal/rest?name=test

Check gRPC communication: http://kubernetes.docker.internal/grpc?name=test