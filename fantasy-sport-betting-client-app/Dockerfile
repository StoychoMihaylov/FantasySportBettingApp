FROM node:lts-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod --verbose
RUN ls -la /app/dist/fantasy-sport-betting-client-app

FROM nginx:alpine
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /app/dist/fantasy-sport-betting-client-app /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
RUN ls -la /usr/share/nginx/html
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]


