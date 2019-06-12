cd ..
docker container rm $(docker container ls -aq) --force
docker-compose -f docker-compose-integration-tests.yml -f docker-compose-integration-tests.override.yml up --build --remove-orphans