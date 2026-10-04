"# Iska_Care-VB.NET-Project" 
# Iska-Care

Iska-Care is a school clinic management system built with **VB.NET (Windows Forms)** and a **MySQL** backend (managed via phpMyAdmin). It's designed to help school clinic staff record, track, and manage student health records digitally instead of relying on paper logs.

## Features

- Student health record management (add, view, update, search)
- Clinic visit / consultation logging
- Medicine or supply inventory tracking
- User authentication for clinic staff
- Report generation for clinic visits and records

## Tech Stack

- **Language:** VB.NET (Windows Forms)
- **Database:** MySQL
- **Database Admin:** phpMyAdmin
- **IDE:** Visual Studio

## Prerequisites

- Visual Studio (2019 or later recommended)
- .NET Framework / .NET Desktop Development workload
- XAMPP or similar (for MySQL + phpMyAdmin)
- MySQL Connector for .NET (e.g., `MySql.Data`)

## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/gian45623-hub/Iska_Care-VB.NET-Project.git
   ```
2. Start your MySQL server (e.g., via XAMPP Control Panel).
3. Open phpMyAdmin and import the provided `.sql` database file (if included) to create the required database and tables.
4. Open the project's `.sln` file in Visual Studio.
5. Update the database connection string in the project's data access/config file to match your local MySQL credentials (host, database name, username, password).
6. Build and run the project (F5).

## Usage

1. Launch the application.
2. Log in with a valid clinic staff account.
3. Use the dashboard to add or search student records, log clinic visits, and manage inventory as needed.

## Project Structure

```
Iska_Care-VB.NET-Project/
├── IskaCare/              # Main VB.NET Windows Forms project
│   ├── Forms/              # UI forms (login, dashboard, records, etc.)
│   ├── Modules/             # Shared code / database helpers
│   └── ...
├── Database/                # SQL scripts (if included)
└── README.md
```

> Note: Update the folder names above to match your actual repo structure once finalized.

## Contributing

This is a school project. Pull requests and suggestions are welcome for improvements or bug fixes.

## License

This project is for academic purposes as part of coursework at Polytechnic University of the Philippines (PUP), Lopez Campus.
