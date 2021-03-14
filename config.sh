cat > docker-compose.secrets.yml <<EOL
version: '3.4'

services:
  customers-db:
    environment:
      - MYSQL_ROOT_PASSWORD=root
      - MYSQL_USER=customers_service
      - MYSQL_PASSWORD=password

  crmit-customers:
    environment:
      - ConnectionString=Server=db;Port=3306;Database=CustomersDB;Uid=customers_service;Pwd=password;
EOL
