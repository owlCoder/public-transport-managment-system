# Public Transport Management System

This project is a WPF (Windows Presentation Foundation) application with WCF (Windows Communication Foundation) services and Entity Framework 6 (EF6) integration. It is designed to manage a public transport system, handling operations related to buses, routes, and drivers.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [Service Contracts](#service-contracts)
- [Contributing](#contributing)
- [License](#license)

## Overview
The Public Transport Management System allows users to manage the fleet of buses, their routes, and the drivers assigned to these routes. The system supports CRUD operations (Create, Read, Update, Delete) for buses, routes, and drivers, as well as the ability to assign and remove drivers from routes.

## Features
- **Bus Management**: Add, update, delete, and view details of buses.
- **Route Management**: Add, update, delete, and view details of routes.
- **Driver Management**: Add, update, delete, and view details of drivers.
- **Driver Assignment**: Assign and remove drivers from specific routes.
- **Authentication**: Driver login functionality.

## Architecture
The system is built using the following technologies:
- **WPF**: For the client-side application providing a graphical user interface.
- **WCF**: For creating and consuming web services.
- **EF6**: For data access and ORM (Object-Relational Mapping).

### Layers
1. **Presentation Layer**: WPF application.
2. **Service Layer**: WCF services defining the business logic.
3. **Data Access Layer**: Entity Framework 6 for database operations.

## Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- SQL Server

## Setup and Installation
1. **Clone the Repository**
   ```sh
   git clone https://github.com/owlCoder/public_transport_system.git
   cd public-transport-system
   ```

2. **Open the Solution in Visual Studio**
   Open `PublicTransportSystem.sln` in Visual Studio.

3. **Restore NuGet Packages**
   Restore the necessary NuGet packages by right-clicking the solution in Solution Explorer and selecting "Restore NuGet Packages".

4. **Update Database Connection String**
   Update the database connection string in the `App.config` or `Web.config` file of the project.

5. **Run Database Migrations**
   Open the Package Manager Console in Visual Studio and run:
   ```sh
   Update-Database
   ```

6. **Build and Run the Application**
   Build the solution and run the application.

## Usage
1. **Start the WCF Services**
   Ensure that the WCF services are running. These services will handle the business logic and data access operations.

2. **Launch the WPF Application**
   Run the WPF application to interact with the public transport management system. Use the UI to manage buses, routes, and drivers.

## Service Contracts
### Autobus Service
```csharp
[ServiceContract]
public interface IAutobusService
{
    [OperationContract]
    bool DodajAutobus(string oznaka);

    [OperationContract]
    bool IzmeniAutobus(int id, AutobusDTO data);

    [OperationContract]
    bool ObrisiAutobus(int id);

    [OperationContract]
    AutobusDTO Procitaj(int id);

    [OperationContract]
    List<AutobusDTO> ProcitajSve();
}
```

### Linija Service
```csharp
[ServiceContract]
public interface ILinijaService
{
    [OperationContract]
    int DodajLiniju(LinijaDTO data);

    [OperationContract]
    int IzmeniLiniju(int id, LinijaDTO data);

    [OperationContract]
    int ObrisiLiniju(int id);

    [OperationContract]
    LinijaDTO Procitaj(int id);

    [OperationContract]
    List<LinijaDTO> ProcitajSve();

    [OperationContract]
    List<LinijaDTO> Pretraga(bool poOdredistu, string unos);
}
```

### VozacLinija Service
```csharp
[ServiceContract]
public interface IVozacLinijaService
{
    [OperationContract]
    bool DodajVozacaNaLiniju(int vozac_id, int linija_id);

    [OperationContract]
    bool UkloniVozacaNaLiniji(int vozac_id, int linija_id);
}
```

### Vozac Service
```csharp
[ServiceContract]
public interface IVozacService
{
    [OperationContract]
    bool DodajVozaca(VozacDTO data);

    [OperationContract]
    VozacDTO IzmeniVozaca(int id, VozacDTO data);

    [OperationContract]
    bool ObrisiVozaca(int id);

    [OperationContract]
    VozacDTO Procitaj(int id);

    [OperationContract]
    List<VozacDTO> ProcitajSve();

    [OperationContract]
    int Prijava(string username, string password);
}
```

## Contributing
Contributions are welcome! Please fork the repository and submit pull requests for any features, bug fixes, or improvements.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.

---

Feel free to modify the README file as per your project's requirements and add any additional details you deem necessary.