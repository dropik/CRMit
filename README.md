# CRMit
An example of a CRM system for a generic busyness. Based on microservice architecture. Implemented with ASP.NET Core.

## Purposes
As the 'CRM' (Customer Relationship Management) prefix in the name CRMit suggests, this system manages the relationship 
between the customer itself and the busyness (which in this case is a generic item-selling busyness). It stores 
customer data, handles items, purchases and various notifications to customer.

## Getting Started
For demonstration purposes, composing microservices is made via docker-compose. To bootstrap the system, run
```cmd
> config.cmd
> run.cmd
```
Once you'll see the message, that Customers service is listening for requests, check http://localhost:8080/crmit/v1/customers/
with a software like Postman.
