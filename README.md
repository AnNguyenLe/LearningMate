### Requirements

For this codebase to run properly, the followings are required:

1. Google Gemini AI API key
2. Google Cloud Platform Credential file

### Install .NET 8

https://dotnet.microsoft.com/en-us/download/dotnet/8.0

### Install Entity Framework Core tools reference - .NET Core CLI

https://learn.microsoft.com/en-us/ef/core/cli/dotnet

```
dotnet tool install --global dotnet-ef
```

### Install PostgreSQL

Direct install: https://www.postgresql.org/download/

or

Using Docker: https://hub.docker.com/_/postgres/

Setup PostgreSQL and update the connection string in appsettings.Development.json accordingly:

`appsettings.Development.json`

```json
    ...
    "ConnectionStrings": {
		"Default": "Server=127.0.0.1;Port=5432;Database=app-bootstrap;User Id=postgres;Password=admin123;"
	},
    ...
```

### Get codebase

```
git clone https://github.com/AnNguyenLe/LearningMate.git
```

### Create Google's Gemini Flask API Key

1. Go to [Google AI Studio](https://aistudio.google.com/).
2. Log in with your Google account.
3. Create an API key.
4. Add Gemini API key to `appsettings.Development.json`

```json
    "GeminiFlashOptions": {
		"BaseUrl": "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent",
		"ApiKey": "your-gemini-flash-api-key-here"
	},
```

### Create Google Cloud Platform credential file

1. Go to: https://console.cloud.google.com

(Reference: [Learn how to get API Key for GCP TTS](https://www.youtube.com/watch?v=GVPWz-nhJhg))

2. Store and get file path to GCP Crendential on local machine

3. Paste file path into `appsettings.Development.json`

```json
    "GoogleCreadentialOptions": {
		"FilePath": "your-json-google-credential-file-path-here"
	}
```

### Database Design Migration

From root folder LearningMate (Terminal view):

Step 1:

```
LearningMate % cd src/LearningMate.Infrastructure
```

Step 2:

```
LearningMate.Infrastructure % dotnet ef database update --startup-project ../LearningMate.WebAPI
```

### Start this project as an backend server

From root folder LearningMate (Terminal view):

Step 1:

```
LearningMate % cd src/LearningMate.WebAPI
```

Step 2:

```
LearningMate.WebAPI % dotnet build
```

Step 3:

```
LearningMate.WebAPI % dotnet watch
```
