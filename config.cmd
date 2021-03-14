@echo off
echo.version: '3.4'> docker-compose.secrets.yml
echo.>> docker-compose.secrets.yml
echo.services:>> docker-compose.secrets.yml
echo.  customers-db:>> docker-compose.secrets.yml
echo.    environment:>> docker-compose.secrets.yml
echo.      - MYSQL_ROOT_PASSWORD=root>> docker-compose.secrets.yml
echo.      - MYSQL_USER=customers_service>> docker-compose.secrets.yml
echo.      - MYSQL_PASSWORD=password>> docker-compose.secrets.yml
echo.>> docker-compose.secrets.yml
echo.  crmit-customers:>> docker-compose.secrets.yml
echo.    environment:>> docker-compose.secrets.yml
echo.      - ConnectionString=Server=db;Port=3306;Database=CustomersDB;Uid=customers_service;Pwd=password;>> docker-compose.secrets.yml