# Secure Commerce Gateway

A C# ASP.NET Core microservices project that uses **Ocelot API Gateway** and **JWT authentication** to secure access to product and order endpoints.

## Architecture

The system consists of four services:

| Service | Local URL | Responsibility |
|---|---|---|
| API Gateway | `http://localhost:5000` | Routes requests and enforces authentication and rate limiting |
| Products Service | `http://localhost:5001` | Product CRUD operations |
| Orders Service | `http://localhost:5002` | Returns sample order data |
| Identity Service | `http://localhost:5003` | Validates login credentials and issues JWTs |

## Technologies

- C#
- ASP.NET Core Web API (.NET 8)
- Ocelot API Gateway
- JWT Bearer Authentication
- Postman
- Swagger / OpenAPI

## Features

- Public product listing and product details
- JWT-protected product creation, updates, and deletion
- JWT-protected orders endpoint
- Login endpoint that issues a signed JWT
- Login rate limiting with HTTP `429 Too Many Requests` when the configured limit is exceeded

## API Routes

| Method | Gateway Route | Access |
|---|---|---|
| GET | `/api/products` | Public |
| GET | `/api/products/{id}` | Public |
| POST | `/api/products` | JWT required |
| PUT | `/api/products/{id}` | JWT required |
| DELETE | `/api/products/{id}` | JWT required |
| GET | `/api/orders` | JWT required |
| POST | `/api/auth/login` | Public; rate limited |

## Local Setup

### Requirements

- Visual Studio with the .NET 8 SDK
- Postman or another HTTP client

### Run the services

Start all four projects using Visual Studio's multiple startup projects configuration. Ensure each service listens on its assigned port:

- Gateway: `5000`
- Products: `5001`
- Orders: `5002`
- Identity: `5003`

### Login

Send a `POST` request to:

`http://localhost:5000/api/auth/login`

Body (`application/json`):

{
  "username": "admin",
  "password": "Pass123!"
}

A successful login returns an access token. Use it as a **Bearer Token** when calling protected endpoints.

> The sample credentials and JWT signing key are for local development only. Replace them with secure configuration before deployment.

## Testing

The project was tested locally using Postman and Swagger.

| Test | Expected Result | Observed Result |
|---|---|---|
| List products without token | `200 OK` | `200 OK` |
| Get product by ID without token | `200 OK` | `200 OK` |
| Login with valid credentials | `200 OK` | `200 OK` |
| Create product without token | `401 Unauthorized` | `401 Unauthorized` |
| Create product with token | `201 Created` | `201 Created` |
| Update product with token | `204 No Content` | `204 No Content` |
| Delete product without token | `401 Unauthorized` | `401 Unauthorized` |
| Delete product with token | `204 No Content` | `204 No Content` |
| Get orders without token | `401 Unauthorized` | `401 Unauthorized` |
| Get orders with token | `200 OK` | `200 OK` |
| Exceed login rate limit | `429 Too Many Requests` | `429 Too Many Requests` observed |

See [`docs/test-matrix.md`](docs/test-matrix.md) for the detailed test matrix.

## Limitations

- Products and orders use sample in-memory data.
- Data resets when the corresponding service restarts.
- Login credentials are hardcoded for demonstration purposes.
- The JWT signing key is configured for local development and must be secured before deployment.
- The project has been tested locally; production deployment has not been verified.

## Repository Structure

SecureCommerceGateway/
├── src/
│   ├── SecureCommerce.Gateway/
│   ├── SecureCommerce.Products/
│   ├── SecureCommerce.Orders/
│   └── SecureCommerce.Identity/
├── docs/
│   └── test-matrix.md
├── SecureCommerceGateway.sln
└── README.md
