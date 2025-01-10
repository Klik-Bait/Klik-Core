# Klik-Core

High level architecture and requirements

Not all cases are covered for now


# Message Flow Diagram

```mermaid
graph TD
    A[Sender API Consumer: Online] -->|User sender message| B(API Gateway)
    B -->|Forwards message| C[MesssageCore.Receiver]
    C -->|Checks if receiver is active| X{Is Receiver Active?}
    X -->|Yes| D[(RabbitMQ)]
    X -->|No| R[Redis]
    R -->|TTL exceeds 5 hours| N[NoSQL Database]
    D -->|Consumed by| E[MesssageCore.Sender]
    E -->|Sends message| F(API Gateway)
    F -->|Delivers message| G[Receiver API Consumer: Online]

```

## Sender send a massage and receiver is online at the time of sending message 



![alt text](architecture-drawings/receiver_is_active.drawio.svg)

### flow:

* User sends message
* Api gateway route it to Message Handler microservice
* Message Handler check if user is online (in this case it is)
* send message to rabbitmq
* Microservice "Message Sender" Consume message
* Message is ack if Receiver API get message otherwise retry, (case when he log in for second and be unavaialbe again for few hours is not covered) we need to implement dead queue letter automation or move later to kafka

## Sender send a massage and receiver is offline at the time of sending message but shorter tha reddis TTL (for now 24 hours)

![alt text](architecture-drawings/user_is_inactive_for_short_period_of_time.drawio.svg)

* User sends message
* Api gateway route it to Message Handler microservice
* Message Handler check if user is online (in this case it is not)
* Save message to redis
* Worker checks if users messages that are stored in redis are active now
* If user is active then message is send to rabbitmq
* Microservice "Message Sender" Consume message
* Message is ack if Receiver API get message otherwise retry, (case when he log in for second and be unavaialbe again for few hours is not covered) we need to implement dead queue letter automation or move later to kafka

