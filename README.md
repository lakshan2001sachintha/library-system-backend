# 📚 Library Management System - Backend

**Front End -** https://github.com/lakshan2001sachintha/library-system-frontend

A lightweight **CRUD Web API** built with **.NET 9**, **Entity Framework Core**, and **SQLite** to manage library books.  
It supports basic operations like **Add**, **View**, **Update**, and **Delete** books and follows a clean **MVC** architecture.

---

## 🏗️ Project Structure



```bash
LibraryBackend/
├── Controllers/         
│   └── BooksController.cs
├── Data/                
│   └── LibraryContext.cs
├── DTOs/              
│   ├── BookCreateDto.cs
│   ├── BookReadDto.cs
│   └── BookUpdateDto.cs
├── Models/              
│   └── Book.cs
├── Services/           
│   ├── BookService.cs
│   └── IBookService.cs
├── Migrations/          
├── Library.db          
└── Program.cs          
```



## 🚀 Setup & Run Instructions

### 1. Clone the repository
```bash
git clone https://github.com/lakshan2001sachintha/library-system-backend.git
cd LibraryBackend
```
### 2. Install dependencies
```bash
dotnet restore
```
### 3. Create database (SQLite)
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
### 4. Run the application
```bash
dotnet run
```

