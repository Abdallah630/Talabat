# 🛒 Talabat E-Commerce API

A production-ready RESTful E-Commerce API built with ASP.NET Core using Onion Architecture.

---

## 🚀 Technologies

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?style=for-the-badge&logo=redis&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=json-web-tokens&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-BE1B1B?style=for-the-badge)
![Stripe](https://img.shields.io/badge/Stripe-635BFF?style=for-the-badge&logo=stripe&logoColor=white)

---

## 🏗️ Architecture
```
Talabat/
├── Talabat.API         → Controllers, Middleware, Extensions
├── Talabat.Core        → Entities, Interfaces, DTOs
├── Talabat.Repository  → EF Core, DbContext, Data Seeding, Redis
└── Talabat.Service     → Business Logic, Auth Service
```

---

## ✅ Features

- 🔐 **Authentication & Authorization** — JWT Bearer Token with ASP.NET Identity
- 👤 **User Management** — Register, Login with Data Seeding
- 📦 **Products** — CRUD with Filtering, Sorting & Pagination
- 🗂️ **Categories & Brands** — Full management
- 🛒 **Basket** — Redis-based shopping cart
- 📋 **Orders** — Order creation and management
- 💳 **Payment** — Stripe integration
- 🌱 **Data Seeding** — Auto seed products and identity data on startup
- 🚨 **Error Handling** — Global exception middleware with custom error responses
- ✅ **Validation** — Custom API validation response
- 📄 **Swagger** — API documentation with JWT support
- 🗺️ **AutoMapper** — Clean DTO mapping
- 🐳 **Docker** — Containerized application

---

## ⚙️ Setup

**1. Clone the repo**
```bash
git clone https://github.com/Abdallah630/Talabat.git
```

**2. Update `appsettings.json`**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Talabat;Trusted_Connection=True",
    "IdentityConnection": "Server=.;Database=Talabat.Identity;Trusted_Connection=True",
    "Redis": "localhost"
  },
  "JWT": {
    "AuthKey": "your-secret-key",
    "ValidAudience": "MySecuredApiUsers",
    "ValidIssuer": "https://localhost:7209",
    "DurationInDays": 30
  },
  "ApiBaseUrl": "https://localhost:7209"
}
```

**3. Run the API**
```bash
dotnet run --project Talabat.API
```

> ✅ Migrations and Data Seeding run automatically on startup.

---

## 📌 Endpoints

### Auth
| Method | URL | Description |
|--------|-----|-------------|
| POST | /api/account/register | Register |
| POST | /api/account/login | Login |

### Products
| Method | URL | Description |
|--------|-----|-------------|
| GET | /api/products | Get all products |
| GET | /api/products/{id} | Get product by id |

### Basket
| Method | URL | Description |
|--------|-----|-------------|
| GET | /api/basket | Get basket |
| POST | /api/basket | Update basket |
| DELETE | /api/basket | Delete basket |

### Orders
| Method | URL | Description |
|--------|-----|-------------|
| POST | /api/orders | Create order |
| GET | /api/orders | Get user orders |
| GET | /api/orders/{id} | Get order by id |

---

## 🐳 Docker
```bash
docker-compose up
```

---

## 👨‍💻 Author

**Abdallah** — [GitHub](https://github.com/Abdallah630) · [LinkedIn](https://www.linkedin.com/in/abdallah-saad-925b4224a)