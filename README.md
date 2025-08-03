# Console Cookbook

A console-based recipe management application (Polish-language recipes and database) built with C# and Entity Framework Core, using MySQL as the database backend.

## Features

- **Recipe Management**: Add, view, search, and delete recipes
- **Ingredient Tracking**: Each recipe can have multiple ingredients with quantities
- **Interactive Console Interface**: User-friendly menu-driven interface
- **Database Integration**: MySQL database with Entity Framework Core
- **Search Functionality**: Search recipes by name or ingredients
- **Data Seeding**: Comes with 5 pre-loaded sample recipes

## Database Structure

The application uses two main tables with a one-to-many relationship:

### Recipes Table (`Przepisy`)
- `Id` (Primary Key)
- `Nazwa` (Name) - Recipe name (max 200 characters)
- `Instrukcje` (Instructions) - Cooking instructions
- `CzasPrzygotowania` (Preparation Time) - Time in minutes
- `LiczbaOsob` (Servings) - Number of servings
- `DataDodania` (Date Added) - Creation timestamp

### Ingredients Table (`Skladniki`)
- `Id` (Primary Key)
- `Nazwa` (Name) - Ingredient name (max 100 characters)
- `Ilosc` (Amount) - Quantity (e.g., "500g", "2 cups")
- `PrzepisId` (Foreign Key) - Reference to recipe

## Prerequisites

- .NET 8.0 SDK
- MySQL Server (tested with Laragon)
- MySQL connection available at `localhost` with root user (no password)

## Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd ConsoleCookbook
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Ensure MySQL is running on localhost with default settings

4. Run the application:
```bash
dotnet run
```

## Manual Database Setup (Alternative)

If Entity Framework automatic database creation doesn't work, you can manually set up the database using the provided SQL scripts:

1. Run `database_schema.sql` to create the database structure
2. Run `database_seed.sql` to populate with sample data

## Usage

When you run the application, you'll see a menu with the following options:

1. **Show all recipes** - Display a list of all recipes with basic info
2. **Show recipe details** - View complete recipe information including ingredients and instructions
3. **Add new recipe** - Interactively add a new recipe with ingredients
4. **Search recipes** - Search by recipe name or ingredient name
5. **Delete recipe** - Remove a recipe and all its ingredients
0. **Exit** - Close the application

### Adding a New Recipe

1. Select option 3 from the main menu
2. Enter recipe name
3. Enter preparation time in minutes
4. Enter number of servings
5. Enter cooking instructions (finish with an empty line)
6. Add ingredients one by one (type 'koniec' to finish)

### Sample Recipes Included

The application comes with 5 pre-loaded recipes:

1. **Spaghetti Carbonara** - Classic Italian pasta dish
2. **Chicken Soup** (Rosół z kurczaka) - Traditional Polish chicken soup
3. **Pancakes** (Naleśniki) - Simple pancake recipe
4. **Pork Cutlet** (Kotlet schabowy) - Polish breaded pork cutlet
5. **No-Bake Cheesecake** (Sernik na zimno) - Refrigerator cheesecake

## Project Structure

```
ConsoleCookbook/
├── Models/
│   ├── Przepis.cs          # Recipe entity model
│   └── Skladnik.cs         # Ingredient entity model
├── Data/
│   └── CookbookContext.cs  # Entity Framework context
├── Services/
│   └── PrzepisService.cs   # Recipe business logic
├── Program.cs              # Main application entry point
└── ConsoleCookbook.csproj  # Project configuration
```

## Technologies Used

- **C# / .NET 8.0** - Main programming language and framework
- **Entity Framework Core** - ORM for database operations
- **MySQL** - Database backend
- **MySql.EntityFrameworkCore** - MySQL provider for EF Core

## Database Connection

The application connects to MySQL using the following connection string:
```
Server=localhost;Database=cookbook;Uid=root;Pwd=;
```

Make sure your MySQL server is configured to accept connections with these parameters.

## License

This project is open source and available under the [MIT License](LICENSE).
