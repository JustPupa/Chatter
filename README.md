Cozy Chatter
=====================
***Chatter*** is an application that combines the functions of a social network and a messenger
Chats support the transfer of various types of `media`, and the `author's toolset` allows you to analyze the popularity of posts in the feed
***
To run the application you need to start both Kestrel and Vite servers:
1) Start the Kestrel server by running it in the code editor or calling the corresponding `Cozy_Chatter.exe` file
2) Run Vite server by calling the ```npm run dev``` command in the terminal
3) Navigate to the web address specified when starting the Vite server
***
How to run application
=====================
The application has Docker+Kubernetes support, so you don't need to manually run two separate servers, Kestrel and Vite. Below is a list of Docker and K8S commands if you use the console instead of Docker Desktop with extensions.

1. Reassemblying ASP.NET part after changing the code:

Description                               | Bash command
------------------------------------------|----------------------
Switching to a directory with Kestrel     | cd <path-to-kestrel>/Chatter/Server
Publishing an application                 | dotnet publish -c Release -o ./bin/Release/net9.0/publish
Building a Docker image                   | docker build -t chatter-kestrel .
Update Deployment in Kubernetes           | kubectl set image deployment/chatter-server chatter-server=chatter-kestrel:latest
Check the Rollout status                  | kubectl rollout status deployment/chatter-server

2. Reassemblying React part after changing the code:

Description                               | Bash command
------------------------------------------|----------------------
Switching to a directory with React part  | cd <path-to-kestrel>/Chatter/Client
Building an application                   | npm run build
Building a Docker image                   | docker build -t chatter-client .
Update Deployment in Kubernetes           | kubectl set image deployment/chatter-client chatter-client=chatter-client:latest
Check the Rollout status                  | kubectl rollout status deployment/chatter-client

3. The remaining important commands for Kubernetes

Description                               | Bash command
------------------------------------------|----------------------
Change the `Kestrel launch template`      | kubectl apply -f k8s/kestrel-deployment.yaml
Changing the `ports for accessing Kestrel`| kubectl apply -f k8s/kestrel-service.yaml
Change the `React launch template`        | kubectl apply -f k8s/client-deployment.yaml
Changing the `ports for accessing React`  | kubectl apply -f k8s/client-service.yaml
Change `Ingress settings`                 | kubectl apply -f k8s/ingress.yaml
Viewing `K8S pods`                        | kubectl get pods
Viewing `K8S services`                    | kubectl get svc
Check `Kestrel logs`                      | kubectl logs deployment/chatter-server
Check `React logs `                       | kubectl logs deployment/chatter-client
Check the `logs of a specific Pod`        | kubectl logs pod/<pod-name> -c <container-name>

***
Application files description
=====================
File/Directory Name            | Content
-------------------------------|----------------------
Chatter/Server/Controllers     | A controller for `processing incoming requests` and sending responses to the Vite server
Chatter/Server/Models          | Model classes representing `database entities`
Chatter/Server/Repositories    | Intermediate classes dealing with the `business logic` of the transmitted data
Chatter/Client/src/components  | React application `components`
Chatter/Client/src/services    | The part of the React application that contains `requests` to the Kestrel server
Chatter/k8s                    | Representation of objects in the `Kubernetes API`

