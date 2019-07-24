cd ..
docker container rm $(docker container ls -aq) --force
docker-compose -f docker-compose-functional-tests.yml -f docker-compose-functional-tests.override.yml up --build --remove-orphans