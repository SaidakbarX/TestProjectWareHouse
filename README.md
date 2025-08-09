# 📦 TestProjectWareHouse

Полноценный full-stack проект, реализованный с использованием:

- 🔧 Backend: ASP.NET Core (.NET 8, Clean Architecture)
- 🌐 Frontend: React + TypeScript + Vite

Проект разделён на два основных модуля: серверная часть (API) и клиентская часть (интерфейс пользователя), находящиеся в общей структуре репозитория (monorepo).

---

## 🚀 Быстрый старт

### 1. Клонирование репозитория
```bash
git clone https://github.com/SaidakbarX/TestProjectWareHouse.git
cd TestProjectWareHouse
🖥️ Клиентская часть (Frontend)
📁 Путь: Client/project

Установка и запуск
bash
Копировать
Редактировать
cd Client/project
npm install
npm run dev
⚙️ Серверная часть (Backend)
📁 Путь: TestProjectWareHouse.Api

Запуск API
Откройте решение TestProjectWareHouse.sln в Visual Studio или VS Code

Установите TestProjectWareHouse.Api как стартовый проект

Запустите (F5) или через CLI:

bash
Копировать
Редактировать
cd TestProjectWareHouse.Api
dotnet run
🗂️ Архитектура проекта
Проект построен по принципам Clean Architecture, с разделением на слои:

bash
Копировать
Редактировать
WareHouse/
├── TestProjectWareHouse.Domain         # Сущности (Entity), интерфейсы
├── TestProjectWareHouse.Application    # Логика приложения, DTO, сервисы
├── TestProjectWareHouse.Infrastructure # Доступ к данным, репозитории (EF Core)
├── TestProjectWareHouse.Api            # Web API, контроллеры, конфигурация
└── Client/project                      # React-приложение (Vite + TS)
Вход в систему

📦 Используемые технологии
Backend:
ASP.NET Core 8

Entity Framework Core
Layered Architecture (Domain, Application, Infrastructure, API)

Swagger (автогенерация документации)

 Service Layer, DTO Mapping

Frontend:
React 18

TypeScript

Vite

React Hooks

API запросы к .NET серверу

🧪 Тестирование
Пока что тесты не реализованы, но предусмотрена возможность:

Юнит-тесты (xUnit, Moq)

Интеграционные тесты (.NET TestServer)

Frontend-тесты через Vitest / Testing Library

📝 Автор
Разработчик: Saidakbar Khayumxojayev
📧 Email: khayumkhujayev6937@gmail.com
🔗 GitHub: SaidakbarX

📄 Лицензия
Проект предназначен для обучения и личного использования.
Свободен для модификации и развития.
