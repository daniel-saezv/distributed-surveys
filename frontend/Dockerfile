# Stage 1: Build Angular app
FROM node:22-alpine AS build

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build --configuration $BUILD_CONFIG

# Stage 2: Serve app with nginx
FROM nginx:alpine

COPY --from=build /app/dist/frontend /usr/share/nginx/html

# COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
