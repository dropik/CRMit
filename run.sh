PRODUCTION_FILE="docker-compose.prod.yml"
while getopts ":t" flag; do
  PRODUCTION_FILE="docker-compose.override.yml"
  DETACHED="-d"
done
docker-compose -f docker-compose.yml -f $PRODUCTION_FILE -f docker-compose.secrets.yml up $DETACHED
