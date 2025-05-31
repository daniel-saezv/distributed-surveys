# Frontend

Angular application for managing and visualizing surveys.

## Technologies

- Angular 19

## Installation

```sh
npm install
```

## Development

```sh
npm run start
```

## Production Build

```sh
npm run build -- --configuration production
```

## Docker Usage

```sh
docker build -t frontend .
docker run -p 4200:80 frontend
```

Alternatively, you can use the service defined in the Docker Compose file.

## Environment Variables

Set any required variables in a `.env` file if needed.