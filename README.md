# 🧠 CVSiteAPI – ASP.NET Core GitHub Portfolio API

CVSiteAPI is a backend RESTful API built with ASP.NET Core. It connects to the GitHub API and provides endpoints to dynamically fetch portfolio repositories for a personal CV website.

---

## ✨ Features

- 🔍 Search GitHub repositories by keyword, language, and user
- 📁 Fetch portfolio projects directly from a GitHub account
- 🌐 REST API with clean routing (`/api/github/...`)
- ⚙️ Built-in support for Dependency Injection and configuration files

---

## 📁 Project Structure

- `Controllers/GitHubController.cs` – Handles API endpoints for portfolio and search
- `GitHubService/GitHubApiService.cs` – Communicates with GitHub API using Octokit
- `Program.cs` – Main entry point and service registration
- `appsettings.json` – Stores API keys or GitHub settings

---

## 🚀 How to Run

1. Clone the repository
2. Open the solution `CVSiteAPI.sln` in Visual Studio or VS Code
3. Set your GitHub credentials/config in `appsettings.Development.json`
4. Run the application and access endpoints like:

```http
GET /api/github/portfolio
GET /api/github/search?query=api&language=csharp&user=myusername
```

---

## 🛠 Technologies Used

- ASP.NET Core Web API
- C# (latest)
- Octokit (GitHub API wrapper)
- REST API principles
- JSON configuration

---

## 📄 License

This project is open-source and free to use for educational or personal portfolio purposes.

---

## 👩‍💻 Developed by

Elisheva Tufik  
Software Developer passionate about clean architecture and dynamic web integration.
