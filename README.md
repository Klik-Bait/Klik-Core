# High level architecture

## Message Flow Diagram

```mermaid
graph TD
    subgraph Top[ ]
        direction LR
        A[Sender API Consumer]
        G[Receiver API Consumer]
    end
    A --> B(API Gateway)
    B --> G
    B -->|Forwards message| C[MesssageCore.ClientReceiver]
    C -->|Checks if receiver is active| X{Receiver user is Active?}
    X -->|Yes| D[(RabbitMQ)]
    X -->|No| R[Store message in NoSql]

    D --> E[MesssageCore.ClientSender]
    E -->|Sends message to Receiver| B
```

### Sender send a massage and receiver is online at the time of sending message 

* User sends message
* Api gateway route it to Message Handler microservice
* Message Handler check if user is online (in this case it is)
* send message to rabbitmq
* Microservice "Message Sender" Consume message
* Message is ack if Receiver API get message otherwise retry, (case when he log in for second and be unavaialbe again for few hours is not covered) we need to implement dead queue letter automation or move later to kafka

### Sender send a massage and receiver is offline at the time of sending message

* User sends message
* Api gateway route it to Message Handler microservice
* Message Handler check if user is online (in this case it is not)
* Save message to nosql

## Keeping track of users active status
``` mermaid
graph TD
    A[Sender API Consumer]
    A -->|Login/Logout Request| B(API Gateway)
    B -->|no auth for now| C[MesssageCore.ClientReceiver]
    C -->|Update Login Status| D[Redis]
```

### User logged in
* Microservice "MesssageCore.ClientReceiver" update user status in redis to "active"

### User logged out
* Microservice "MesssageCore.ClientReceiver" update user status in redis to "not active"

## Send undelivered messages

``` mermaid
graph TD
    Client <--> B(API Gateway)
    B --> C[MesssageCore.ClientReceiver]
    C --> H{Messages Found in NoSQL?}
    H -->|Yes| RabbitMq
    H -->|No| F1[No Action Needed]
    RabbitMq --> E[MesssageCore.ClientSender]
    E -->|Sends message to Client| B
```
