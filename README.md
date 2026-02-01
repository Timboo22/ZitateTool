# 📜 ZitateTool

**ZitateTool** is a modern web application for creating, managing, and displaying quotes.  
It is built with a **.NET Minimal API** backend and an **Angular 21** frontend.

The project focuses on a clean architecture, fast APIs, and a modern Angular setup using the latest features.

---

## 🚀 Tech Stack

### Backend
- **.NET (Minimal API)**
- RESTful API (JSON)
- Lightweight and fast endpoints
- Easily extendable (EF Core, SQL, Auth, etc.)

### Frontend
- **Angular 21**
- Standalone Components
- Signals for state management
- HttpClient for API communication
- Modern template syntax (`@for`, `@if`)

---

## ✨ Features

- 📖 Display quotes
- ➕ Create new quotes
- 🔄 Instant refresh after creating a quote
- 🖼️ Optional avatar / image support
- ⚡ High-performance backend using Minimal API
- 🧩 Clear separation of frontend and backend

---

## 📂 Project Structure

```text
ZitateTool/
├── backend/
│   ├── ZitateTool.Api/
│   │   ├── Program.cs
│   │   ├── Endpoints/
│   │   └── Models/
│
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   ├── services/
│   │   └── pages/
│
└── README.md
```

---

## ⚙️ Running the Backend (Minimal API)

```bash
cd backend/ZitateTool.Api
dotnet restore
dotnet run
```

➡️ The API will be available at  
`https://localhost:5001` or `http://localhost:5000`

---

## 🖥️ Running the Frontend (Angular 21)

```bash
cd frontend
npm install
ng serve
```

➡️ The frontend will be available at  
`http://localhost:4200`

---

## 🔌 API Examples

### Get all quotes
```http
GET /api/zitate
```

### Create a new quote
```http
POST /api/zitate
Content-Type: application/json

{
  "zitateName": "The journey is the reward",
  "author": "Confucius"
}
```

---

## 🧠 Architecture Overview

- Backend handles **business logic and data**
- Frontend handles **UI, UX, and state**
- Communication via **REST (HTTP / JSON)**
- Easily extendable with:
  - Authentication
  - Database persistence
  - Categories & tags
  - Favorites / likes

---

## 🛠️ Requirements

- **.NET SDK 8+**
- **Node.js 18+**
- **Angular CLI**
- Git

---

## 📌 Roadmap (Optional)

- [ ] Edit & delete quotes  
- [ ] Categories / tags  
- [ ] User authentication  
- [ ] Dark mode  
- [ ] Docker & deployment  

---

## 👤 Author

**Timboo22**  
💻 Apprentice: Software Developer (Application Development)  
🚀 Focus: Web Development, APIs, Angular, .NET
