# https://mherman.org/blog/dockerizing-a-react-app/
# base image
FROM node:10.15.0

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY /src/eDoxa.Web.Spa/ClientApp/package.json /app/package.json
RUN npm install npm -g --silent
RUN npm install react-scripts -g --silent
RUN npm install typesync -g --silent
RUN npm install --silent

# start app
CMD ["npm", "start"]
