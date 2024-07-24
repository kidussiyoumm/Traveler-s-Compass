# Traveler's-Compass

.Net 7.0 C# Asp.Net Core 
- web application provides a user-friendly platform for tourists to explore various attractions and plan their itinerary effortlessly. Users can browse through a comprehensive list of attractions, view detailed information about each attraction, and create personalized itineraries tailored to their preferences and interests. With features such as interactive maps, reviews, and ratings, our application aims to enhance the travel experience by empowering users to discover, plan, and enjoy memorable journeys with ease. Whether exploring a new city or embarking on a dream vacation, our application is designed to be the ultimate companion for travelers seeking adventure and discovery.

•	Data Modeling and Database Management:
- Implemented a code-first approach to model classes using data annotations.
- Installed Entity Framework Core and defined DbSet properties.
- Conducted initial migrations and established correct dependency injection pipelines in the program class.
- Created a database in Microsoft SQL Server which was able to be populated using CRUD operations by RESTFUL_Services.
- Hashing of passwords was later used in the Authentication and Authorization.

•	Repository Pattern:
- Designed asynchronous interfaces and implemented repository classes for each entity, utilizing dependency injection on interfaces and the DbContext class.

•	Action Methods and CRUD Operations:
- Developed asynchronous RESTful services with HTTP GET, POST, PUT, and DELETE methods, implementing them towards the repository with try-catch blocks and exception handling.
- Tested endpoints using Swagger UI and Postman, passing JSON data to perform CRUD operations on the_database.
- Implemented JSON serialization and deserialization to handle data transfer between the client and server efficiently.

•	DTOs and Mapping:
- Created Data Transfer Objects (DTOs) to ensure clients receive only necessary information.
Implemented Auto Mapper to map entities to DTOs and vice versa.
- Utilized Mapper functionality to efficiently map data between different layers of the application.

•	Authentication and Authorization:
.	 Password Hashing and Encryption:
- Implemented secure password hashing using the ASP.NET Core Identity framework, which employs PBKDF2 (Password-Based Key Derivation Function 2) for hashing.
- During registration, user passwords are hashed before being stored in the database, ensuring that plain text passwords are never saved.
- For each login attempt, the provided password is hashed and compared with the stored hashed password.

.	JWT Authentication:
- Configured JWT authentication to manage secure token generation and validation.
- Defined roles for users and agents, ensuring secure access and role-based authorization.
- Used Postman for testing authentication and authorization workflows.

•	Testing:
- Conducted unit tests for all action methods using xUnit and mocked dependencies with FakeItEasy.



Frontend Development:
Angular Application:

- Developed the frontend using Angular, creating various components with HTML, CSS, Bootstrap, and TypeScript.
- Styled the application with CSS and Bootstrap for a responsive design.

•	Component-Based Architecture:
- Ensured scalability and maintainability by using interfaces and dependency injection within Angular components.
- Created dynamic forms for user and agent registration using reactive templates, managing local storage for persistent sessions. These sessions were later transferred to the database using HttpClient, with the database connected through pipelines to perform CRUD operations.

•	User Interface:
- Developed a comprehensive UI including landing pages and dynamic navigation bars.
- Implemented JWT tokens for persistent user sessions, enabling a seamless shopping cart system for users and a platform for agents to post packages and itineraries.

I.	Data Flow and Integration:
•	CORS and Data Transfer:
- Enabled CORS to facilitate secure data flow between the Angular frontend and ASP.NET Core backend.
- Ensured efficient data transfer to and from the database through well-structured pipelines.

•	DevOps and CI/CD:
- Continuous Integration and Deployment:
- worked on hosting the application and implementing CI/CD pipelines using YAML by creating a build pipeline and release pipeline.
- Creating jobs and tasks to build the solution to artifacts, ensuring a consistent development cycle.

This project showcases my comprehensive knowledge of data structures and full stack development using C# Language implementing .Net Framework and .Net Core, employing technologies such as ASP.NET Core(REST Services), MVC, Angular(JavaScript, CSS, HTML and Bootstrap), and Microsoft SQL Server(quires and store producers) to create a robust and scalable web application hosting on Azure(DevOps, Azure Functions and App Services).

