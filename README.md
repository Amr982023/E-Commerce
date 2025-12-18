🛒 E-Commerce Management System
📌 Overview

A modern, secure, and scalable E-Commerce Management System built using Clean / Onion Architecture principles.
The system provides complete product, order, and user management with authentication, authorization, logging, and robust data handling for real-world business scenarios.

✨ Features

👤 User Management (Admin, Seller, Customer roles)

🔐 Authentication & Authorization using JWT (Role-based & Policy-based)

🛍 Product Management (categories, products, variations, items)

📦 Order Management (create orders, order lines, statuses, tracking)

🔎 Advanced search, filtering, and pagination

🧩 DTO-based data exchange for API contracts

🔄 Clean separation between Domain, Application, Infrastructure, and API layers

🧠 Business rules validation at service level

🛡 Custom Middleware for global exception handling

📜 Logging with Serilog (console + file logging)

🧾 Transactional data handling to ensure consistency


🛠️ Tech Stack

Language: C#

Framework: ASP.NET Core Web API

Architecture: Clean Architecture / Onion Architecture

ORM: Entity Framework Core

Database: SQL Server

Data Access: EF Core + LINQ

Security: JWT Authentication & Authorization

Logging: Serilog

Mapping: AutoMapping (Mapster) / Manual Mapping / DTOs

API Style: RESTful

📂 Project Structure

/Ecommerce.API → API Layer (Controllers, Middleware, Logs)

/Ecommerce.Application → Application Layer (Services, DTOs, Business Logic)

/Ecommerce.Domain → Domain Layer (Entities, Enums, Interfaces, Core Rules)

/Ecommerce.Infrastructure → Infrastructure Layer (EF Core, Repositories, DB Context)

/Database → SQL Server scripts & schema

🚀 How to Run

Clone the repository:

git clone (https://github.com/Amr982023/E-Commerce-API.git)


Open the solution in Visual Studio.

Restore NuGet packages.

Update the connection string in appsettings.json.

Apply database migrations:

update-database


Run the project.

Access the API via:

https://localhost:{port}/api

Admin Account:
Username : User1
Password : Amr@1234

📌 Future Enhancements

🛒 Frontend integration (Angular / React)

📱 Mobile application support

🔔 Notifications system (Email / Push)

📊 Advanced analytics & reporting

🌍 Multi-language support
