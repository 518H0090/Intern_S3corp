FROM node:alpine
WORKDIR /app
COPY package.json /app
RUN yarn install --production 
COPY . /app
CMD ["yarn","run","start"]