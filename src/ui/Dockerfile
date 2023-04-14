FROM node:18.13.0 as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY ./package*.json .
RUN npm install
COPY . .

RUN ng build --configuration=production

FROM nginx:1.21-alpine
COPY --from=build /app/dist/amogusui /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]