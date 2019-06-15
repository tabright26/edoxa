cd ..
docker container rm $(docker container ls -aq) --force
docker-compose -f docker-compose-testing.yml -f docker-compose-testing.override.yml up -d --remove-orphans