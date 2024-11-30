# Stage 1: Build the Angular application
FROM node:18 AS build-stage

# Set the working directory
WORKDIR /app

# Set NODE_OPTIONS to use the legacy OpenSSL provider - the app doesnt start without running this command before serve.
ENV NODE_OPTIONS=--openssl-legacy-provider

# Copy package.json and package-lock.json (or yarn.lock) into the container
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the Angular app for production
RUN npm run build --prod

# Stage 2: Serve the app using Nginx
FROM nginx:alpine AS production-stage

# Copy the built Angular app from the previous stage
COPY --from=build-stage /app/dist/Angular7 /usr/share/nginx/html

# Expose the port that Nginx will use
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
