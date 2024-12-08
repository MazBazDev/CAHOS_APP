# Cahos APP by MazBaz

## Description

This project is a desktop application built with Avalonia and C#. It provides a user interface for managing various aspects of a business, including categories, stocks, orders, clients, and logs (with csv export). The application also includes authentication, charts, statistics and auto-refresh features.

## Features

- **Authentication**: Register, Login and logout functionality.
- **Navigation**: Navigate between different pages such as Home, Categories, Stocks, Orders, Clients, and Logs.
- **Auto-Refresh**: Automatically refreshes content every 30 seconds if enabled.
- **Charts**: Displays charts orders.
- **Statistics**: Displays statistics for stocks, orders, and clients.
- **Logs**: Displays logs for different actions performed in the application.
- **Dark Mode**: Switch between light and dark mode (follow the current device theme).
- **Responsive Design**: Works on different screen sizes.
- **Error Handling**: Displays error messages with different colors based on the type of message (error or success).
- **CSV Export**: Export logs to a CSV file.
- **Logout**: Log out and return to the login screen.
- **API Integration**: Communicates with the [cahos_api](https://github.com/mazbazdev/cahos_api) to fetch and update data.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Avalonia](https://docs.avaloniaui.net/docs/get-started/install) (I'm a Apple user, so I used the AvaloniaUI for building cross-platform applications)
- [cahos_api](https://github.com/mazbazdev/cahos_api)  Must be running

## Getting Started

1. Clone the repository:
    ```sh
    git https://github.com/mazbazdev/cahos_app.git
    cd cahos_app/App
    ```

2. Set up the `/App/.env` file:
    ```dotenv
    API_BASE_URL=http://127.0.0.1:8010/api/
    AUTO_REFRESH=False
    ```
    - `API_BASE_URL`: The base URL of the API.
    - `AUTO_REFRESH`: Set to `true` to enable auto-refresh.

    - **Note**: ⚠️ The API base URL should match the URL where the API is running.


3. Ensure the API is running:
    Follow the instructions in the [cahos_api](https://github.com/mazbazdev/cahos_api) repository to start the API.


4. Build and run the application:
    ```sh
    dotnet build
    dotnet run
    ```

## Usage
- **Authentication**: Register or login to access the application.
- **Navigation**: Use the buttons to navigate between different pages.
- **Auto-Refresh**: If enabled, the content will refresh every 30 seconds.
- **Logout**: Click the logout button to log out and return to the login screen.
- **CSV Export**: Click the export button to export logs to a CSV file.
- **Dark Mode**: Switch between light and dark mode.
- **CRUDS Operations**: Perform CRUD operations on categories, stocks, orders, and clients.
