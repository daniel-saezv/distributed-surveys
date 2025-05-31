# Distributed Surveys

This repository contains a microservices architecture for a distributed surveys application. It includes backend services built with .NET (Minimal APIs) and a frontend in Angular.

## Repository Structure

```
/
├── api-gateway/
├── auth-service/
├── survey-service/
├── response-service/
├── analysis-worker/
├── frontend/
├── docker-compose.yml
├── .env
└── README.md
```

## Requirements

- Docker and Docker Compose
- Node.js and Angular CLI (for local frontend development)
- .NET 9 SDK (for local backend development)

## Setup

1. The `.env` file contains dummy credentials for development purposes only.
2. Install frontend dependencies if you plan to develop locally:
    ```sh
    cd frontend
    npm install
    ```

## Usage

### Start all services with Docker Compose

```sh
docker-compose up
```

### Or start a specific service with its dependencies

```sh
docker-compose up auth-service
```

### Local frontend development

1. Stop the frontend service if it is running in Docker Compose:
    ```sh
    docker-compose stop frontend
    ```
    Or comment out the "frontend" service in `docker-compose.yml`.
2. Start the Angular development server:
    ```sh
    cd frontend
    npm run start
    ```

## Services

- **api-gateway:** API entry point.
- **auth-service:** Authentication service.
- **survey-service:** Survey management.
- **response-service:** Response management.
- **analysis-worker:** Asynchronous processing and analysis.
- **frontend:** Angular application.

## Dependencies

- **PostgreSQL:** Shared database between services, with dedicated schemas to minimize costs.
- **RabbitMq:** Queue for processing and analysis events.

## License

MIT