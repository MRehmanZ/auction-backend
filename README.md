# Auction Backend RESTful Service in C#
## Utilise this as a reference to construct the backend infrastructure for AuctionBackend, incorporating an integrated database functionality.


# Overview 
> In this context, we examine the necessary steps involved in implementing a "Code-First Approach."
> It's important to highlight that this compilation was essembled using visual studio.
>This project constitutes an Auction backend service using ASP.NET Core, offering a robust foundation for builiding and overseeing several auction functionalities. It encompasses features such as Bid, Category, comment, and AuctionRecord, among others.

# Technology Utilised
>SQL server: relational database management system, offering realibility, scalability, and retrieval within the ASP.NET framework.
>C# programming language: serves as the primary language for development purposes.
>ASP.NET Core: A versatile framework designed for creating web appliations and services, leveraging the  C# programming language.
>Entity Framework Core: An Object-Relational Mappinng (ORM) framework for .NET facilitates database  access within application..
>ASP.NET Core Identity: offes functionalities for applicationuser authentication and authorisation. capabilities.
>JWT Authentication: employed to secure API, utilising JSON Web Token.


## Sequence
1. Initiating a new project
2. Generate Model classes and integrating DBContext model class
3. Incorporating the context with dependency injection
4. Implementing the Database Connection string
5. Installing the package manager
6. Install necessary packages using the package manager console
7. Adding the Migrations
8. Integrating the Controllers
9. Publish to Microsoft Azure
10. Deployment

-----------

1. Creating a new project

Use the command

```
dotnet new 
```

-----------

2. Creating Model classes Adding DBContext model classes

Create a file under the Models folder, name it as what you would name 
Create many Files as you would like Models created

Remember: (at the top of each model)
```
using System.Collections.Generic;
```

-----------
3. Registering the context with dependency injection

In the program.cs we add this code, this is subject to which databse
solution your choose, in this case I use SQLLite:

```
builder.Services.AddControllers();
builder.Services.AddDbContext<SchoolContext>(options =>
  ptions.UseSqlite(builder.Configuration.GetConnectionString("Connection")));
```
and then add
```
app.MapControllers();
```
just before the
```
app.UseHttpsRedirection();
```
-----------
4. Adding the Database Connection string

In the appsettings.json file we connection string to our database server, this is subject to which databse solution your choose, 
In this case I use SQLLite

```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=mydatabase.db"
  }
```
-----------

5. Installing the Package Manager

Two ways to install a package 
```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
```
OR

Hitting searching 
```
>NuGet Package Manager: Add Package"
```

-----------
6. Installation of required packages using the package manager 

## Install Entity Framework Core Tools Global Tool
```
dotnet tool install --global dotnet-ef
```

Packages Installed for this program, can be found in the path:
```
bin/Debug/net8.0/S3.deps.json
```
The dependencies I have in my app are:

    "Microsoft.EntityFrameworkCore": "8.0.1",
    "Microsoft.EntityFrameworkCore.Design": "8.0.1",
    "Microsoft.EntityFrameworkCore.Sqlite": "8.0.1",
    "Microsoft.EntityFrameworkCore.Tools": "8.0.1",
    "Microsoft.VisualStudio.Web.CodeGeneration.Design": "8.0.0"

Only use packages you need. 


-----------

7. Adding the Migrations

Run the following command to create migrations
```
dotnet new tool-manifest

dotnet tool install --local dotnet-ef

dotnet ef migrations add InitialCreate


```

Apply the migrations to update the database:

```
dotnet ef database update

```

-------------

8. Adding the controllers

Create the controllers, by manually getting them into the controllers folder. Be sure to use 
the correct naming convention, if your model is called Grade.cs then its corresponding
controller would be called GradesController.cs

Or Via the command line

```
dotnet tool install --local dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

```


To add Sign In Funcitonality, JWT and Identity Entity Framework check these steps out:

Now go to the SchoolContext.cs file and change this line 
(inheritance class basically):
From
```
public class SchoolContext : DbContext
```
to
```
public class SchoolContext : IdentityDbContext<IdentityUser>
```

be sure to install the package:

```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

and
```
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
```

at the top of the SchoolContext.cs file

now go to the program.cs file and add another service 
dependency injection:
```
//code
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SchoolContext>().AddDefaultTokenProviders();
```
If you see errors, add 
```
using Microsoft.AspNetCore.Identity; 
```
at the top of the program.cs file 

Once you have made these changes and saved the files.
Now go to tools -> NuGet package manager -> Package manager console
And type in
```
dotnet ef migrations add IdentityAdded
```
If it is successful, you will see a change in migrations 
folder, after this type this in the same console:
```
dotnet ef database update
```

Create a new Model named: AuthModel.cs

In the project directory create a new folder named Services

and two files named: EmailSettings.cs & EmailService.cs

install the package:

```
MailKit by Jeffrey Stedfast

dotnet add package MailKit

```

at the top of the EmaillService.cs file be sure to use these libraries:

```
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using YourProject.Services;
```

Then go to your COntrollers folder and add a new controller called AccountController.cs

then use these lirbaries in your controllers file:

```
using IdentityPractice.Models;
using IdentityPractice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
```

Now to register our email service in our program.cs file we need to add:

```
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();
```

in the appsettings.jsson file we need to add after theconnection string:

```
"EmailSettings": {
 "SmtpServer": "smtp.gmail.com",//only valid for gmail
 "SmtpPort": 587,
 "SmtpUsername": "yourgmailaccount@gmail.com",//create a testing gmail account
 "SmtpPassword": "your gmail app password"//use one time generated app password
 },
```
