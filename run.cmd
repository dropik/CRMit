setlocal
set production_file=docker-compose.prod.yml
if "%~1"=="/t" (
  set production_file=docker-compose.override.yml
)
docker-compose -f docker-compose.yml -f %production_file% -f docker-compose.secrets.yml up
endlocal