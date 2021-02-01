# Project Overview
This project is a server-side web application built with ASP.NET Core 3.1 MVC.
The project focuses on how to write a loosely coupled architecture and how to apply *object oriented design principles* in ASP.NET Core web application.
</br>
</br>
Some of technologies used in this project are:
* **NPM** for front-end dependency management.
* **Gulp** for front-end development task automation.
* Responsive web design using **Bootstrap 4** and custom css.
* **Azure Blob Storage** for storing uploaded image.
* **SignalR Core** for real-time notification.
* **ASP.NET Core Identity** for user management and authentication
* **Entity Framework Core** with **SQLite**

# Running The App On Your Local Machine
## Cloning the project
```bash
$ git clone https://github.com/prabuwardhana/Bulletin-Board-ASPNET-CORE.git
```

## SQLite Database Update
Navigate to the MVC project
```bash
$ cd BulletinBoard.MvcApp
```
Run the database update
```bash
$ dotnet ef database update
```
This command will generate a database file called **BulletinBoard.db** in the BulletinBoard.MvcApp assembly (inside BulletinBoard.MvcApp folder).

## Setting Up Configs for Azure Blob Storage And Mail Service
Add below code in the appsettings.json (**not recommended**) or set up a user secret (**recommended**).
```json
{
  "AzureBlobOption": {
    "ConnectionString" : "Connection string to your blob storage account",
    "Container": "Name of your container",
    "FileSizeLimit": 2097152
  },
  "EmailConfiguration": {
    "From": "your email address",
    "SmtpServer": "smtp.gmail.com", // if using gmail
    "Port": 465,
    "Username": "your username",
    "Password": "your password"
  }
}
```
> You need to create azure account (12 months free trial) if you don't have one.
>
> Make sure you use an email account without 2-step verivications.
