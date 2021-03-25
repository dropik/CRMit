# CRMit
An example of a CRM system for a generic busyness. Based on microservice architecture. Implemented with ASP.NET Core.

## Purposes
As the 'CRM' (Customer Relationship Management) prefix in the name CRMit suggests, this system manages the relationship 
between the customer itself and the busyness (which in this case is a generic item-selling busyness). It stores 
customer data, handles items, purchases and various notifications to customer.

## Getting Started
This repository is dedicated mostly for developers (for me) and for e2e testing. So to run the environment in a preview mode, use docker-compose:
```sh
> docker-compose -f docker-compose.yml up
```

Once the Customers service is up, you can observe it with a software like Postman.