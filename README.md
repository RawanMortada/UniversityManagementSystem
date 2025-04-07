# **University Management System API**

This is a simple university management system api built using ASP.NET and Entity Framework. The API allows the admin to manage information related to students, courses, and instructors.


## **Project Overview**

This API provides endpoints for the following core functionalities:
- Students: add, delete, and update student information
- Instructors: add, delete, and update instructor information
- Courses: add, delete, and update course information
- Enrollement: enroll/remove students into/from courses

  
## **Tech Stack🖥️**

- ASP.NET Core – Framework used to build the RESTful API
- Entity Framework Core – ORM used for database interaction
- SQL Server – Relational database used for storing data


## **Authentication🔒**

This API uses JWT tokens to authenticate users. When a user logs in, a token is generated and returned. The client then includes this token in the Authorization header for future requests.

### How It Works:

- The API generates a JWT with the user's email and username

- The client uses this token to make authenticated requests

- The token expires after 40 minutes

