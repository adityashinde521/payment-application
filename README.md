Below is the updated and detailed `README.md` file based on the provided Dockerfiles and `docker-compose.yml`:

---

# Containerized Sample Payment Application (BASIC CRUD) Setup

This repository provides a step-by-step guide to set up and run a containerized application consisting of:

- **Frontend**: Angular [v7]
- **Backend**: .NET Core 6 API  
- **Database**: SQL Server  [2022]

The application uses Docker and Docker Compose to streamline deployment.

---

## Prerequisites

### 1. Install Dependencies
Ensure the following tools are installed:

- **Docker**: [Docker Desktop](https://www.docker.com/products/docker-desktop) or Docker Engine for Linux users.  
- **Node.js**: Version 18.x or later ([Download Node.js](https://nodejs.org/)) for Angular application (if local modifications are required).  
- **.NET SDK**: Version 6.x ([Download .NET SDK](https://dotnet.microsoft.com/download)) for .NET Core API (if local modifications are required).  

##  Required Versions
-  Tool	      Required Version
-  Docker	    20.10+
-  Node.js	  18.x
-  NPM	      8.x
-  .NET SDK	  6.0.x

---

### 2. Verify Installed Versions
Run the following commands to confirm the installed versions:

```bash
docker --version   # Docker version     
node --version     # Node.js version
npm --version      # NPM version
dotnet --version   # .NET SDK version
```

## Repository Structure

```plaintext
|-- WebAPP/              # Angular application source code
|   |-- Dockerfile       # Dockerfile for Angular application
|-- WebAPI/              # .NET Core 6 API source code
|   |-- Dockerfile       # Dockerfile for .NET Core API
|-- docker-compose.yml   # Orchestrates the services
|-- sql/                 # SQL Server initialization scripts (if any)
|   |-- init.sql         # Optional database setup script
```

---

## Setup and Running the Application

### Step 1: Clone the Repository
Clone the repository to your local machine:

```bash
git clone https://github.com/adityashinde521/payment-application.git
cd payment-application
```

---

### Step 2: Build and Start the Containers
Run the following command to build and start all containers:

```bash
docker-compose up --build
```

- This command builds the images for:
  - **Angular (Frontend)**: Served via Nginx on `http://localhost:8080`.  
  - **.NET Core API (Backend)**: Accessible on `http://localhost:5000`.  
  - **SQL Server (Database)**: Exposed on `localhost:1433`.  

---

### Step 3: Access the Application
After starting the containers, use the following URLs to access the services:

- **Frontend (Angular)**: [http://localhost:8080](http://localhost:8080)  
- **Backend API (.NET Core)**: [http://localhost:5000](http://localhost:5000)  
- **Database (SQL Server)**: Use `localhost:1433` with the following credentials:  
  ```plaintext
  Username: sa  
  Password: Qwerty!123  
  ```

---

## Configuration Details

### Docker Compose Configuration (`docker-compose.yml`)
- **WebAPP (Frontend)**: Angular app served via Nginx.  
- **WebAPI (Backend)**: .NET Core 6 API. Connects to SQL Server using the connection string:  
  ```
  Server=sqlserver;Database=PaymentDetailDB;User Id=sa;Password=Qwerty!123;Encrypt=false;MultipleActiveResultSets=True;
  ```
- **SQL Server**:
  - Image: `mcr.microsoft.com/mssql/server:2022-latest`
  - Port: `1433`

---

## Troubleshooting

### Port Conflicts
Ensure the following ports are not already in use by other applications:
- **8080**: Frontend  
- **5000**: Backend  
- **1433**: SQL Server  

### Viewing Container Logs
To debug any issues, view container logs using:

```bash
docker-compose logs <service_name>
```

For example:
```bash
docker-compose logs webapp
```

### Rebuilding Containers
If code changes or configuration updates are made, rebuild the containers:

```bash
docker-compose up --build
```

---

## Stopping and Cleaning Up

### Stop Containers
To stop all running containers:

```bash
docker-compose down
```

### Clean Up Containers and Images
To remove containers, volumes, and images:

```bash
docker-compose down --volumes --rmi all
```

---

## Additional Notes

- **Node Options for Angular**: `NODE_OPTIONS=--openssl-legacy-provider` is set to ensure compatibility during Angular build.  
- **Database Healthcheck**: The SQL Server service uses a `healthcheck` to verify readiness before allowing dependent services to start.  

---

## Contributing

1. Fork the repository.  
2. Create a new branch (`git checkout -b feature-name`).  
3. Commit your changes (`git commit -m "Added feature X"`).  
4. Push the branch (`git push origin feature-name`).  
5. Open a pull request.

---

## License

This project is licensed under the [MIT License](LICENSE).

--- 

This documentation provides a clear, step-by-step guide to set up the containerized application, ensuring that all configurations and dependencies are adequately covered.
