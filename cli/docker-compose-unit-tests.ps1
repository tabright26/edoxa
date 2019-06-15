cd ..
docker container rm $(docker container ls -aq) --force
docker-compose -f docker-compose-unit-tests.yml up --build