# TimeTrack API

The TimeTrack API is built in C# and serves as the backend for the TimeTrack web application. This API allows users to create and manage projects and track the time spent on tasks within each project.The api can be accessed at: https://api.timetrack.projects.bbdgrad.com and https://api.timetrack.projects.bbdgrad.com/swagger to see the avaliable endpoints.

## Getting Started

To run the TimeTrack API locally, follow these steps:

### Prerequisites

Ensure you have the following installed on your machine:

- Visual Studio
- .NET Core SDK
- .NET 8

### Configuration

Add and set the following environment variables to the `launchSettings.json` file in the `Properties` folder under the `https` section:

```json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development",
  "DATABASE_CONNECTION_STRING": "",
  "ALLOWED_HOSTS": "",
  "CLIENT_ID": "",
  "CLIENT_SECRET": "",
  "REDIRECT_URI": "",
  "TOKEN_ENDPOINT": ""
}
```
### Running the Application
- Open the project solution in Visual Studio.
- Run the application as https.
- The API should be accessible at https://localhost:7092. 
- Navigate to https://localhost:7092/swagger to see the available endpoints.

### Authorization
The TimeTrack API uses bearer token authentication. Users must be authenticated to access the API endpoints. The API restricts access so that users can only manage records they "own".

