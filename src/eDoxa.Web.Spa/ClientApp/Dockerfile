# https://mherman.org/blog/dockerizing-a-react-app/
# base image
FROM node:10.15.0

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY /src/eDoxa.Web.Spa/ClientApp/package.json /app/package.json
COPY /src/eDoxa.Web.Spa/ClientApp/package-lock.json /app/package-lock.json
COPY /src/eDoxa.Web.Spa/ClientApp/env.sh /app/env.sh
COPY /src/eDoxa.Web.Spa/ClientApp/.env /app/.env
RUN npm install npm -g --silent
RUN npm install react-scripts -g --silent
RUN npm install typesync -g --silent
RUN typesync
RUN npm install --silent
RUN apt update
RUN apt install dos2unix
RUN dos2unix /app/.env
RUN dos2unix /app/env.sh

# start app
ENTRYPOINT ["/bin/bash", "-c", "chmod +x /app/env.sh && /app/env.sh && mv /app/env-config.js /app/public && npm start"]
