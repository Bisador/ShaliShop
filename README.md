📌 Title
# 🛍️ ShaliShop — Modular E-Commerce System with Domain-Driven Design

📖 Overview
ShaliShop is a modular, test-driven e-commerce backend built with Domain-Driven Design, CQRS, and MediatR. It models key bounded contexts—Shopping, Inventory, Shipping, and Product Catalog—with rich domain events, lifecycle transitions, and clean architecture principles.

🧱 Features
- 🚚 Shipping workflows: Dispatch, delivery, retry, cancellation
- 📦 Inventory tracking: Reservations, restocking, low-stock alerts
- 🛒 Product catalog: Publish, price changes, variant configuration
- 🧪 Comprehensive test coverage for application logic
- 📡 CQRS orchestration with MediatR
- 🧠 Domain observability via event emission

🚀 Technologies 
- ASP.NET Core
- Entity Framework Core
- MediatR
- FluentAssertions & xUnit
- Value Objects, Domain Events


🛠️ Setup 
```bash
git clone https://github.com/Bisador/ShaliShop
cd ShaliShop
dotnet build
dotnet test
