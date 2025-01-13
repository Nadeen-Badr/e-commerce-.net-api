A robust and scalable **E-Commerce API** built with **.NET Core 8** and **Entity Framework Core**. This API supports **sellers** to manage products and **buyers** to browse products, add items to their cart, and place orders.

---

## **Features**

### **Sellers**
- **Register** as a seller.
- **Login** with JWT authentication.
- **Create**, **update**, and **delete** products.
- **View** products created by the seller.

### **Buyers**
- **Register** as a buyer.
- **Login** with JWT authentication.
- **Browse** all available products.
- **Add** products to the cart.
- **Remove** products from the cart.
- **Place** orders.
- **View** order history.

### **General**
- **Role-based authorization** (Seller, Buyer).
- **JWT authentication** for secure access.
- **Repository pattern** for clean and maintainable code.
- **DTOs** for clean and consistent API responses.
- **AutoMapper** for easy mapping between entities and DTOs.

---

## **Technologies Used**

- **Backend**: .NET Core 8
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Logging**: Built-in .NET logging
- **API Documentation**: Swagger/OpenAPI
- **Dependency Injection**: Built-in .NET Core DI

---

## **Getting Started**

### **Prerequisites**

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

---

### **Setup**

1. **Clone the Repository**:


2. **Configure the Database**:
   - Update the connection string in `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
     ```

3. **Run Migrations**:
   ```bash
   dotnet ef database update
   ```

4. **Run the Application**:
   ```bash
   dotnet run
   ```

5. **Access the API**:
   - Open your browser and navigate to `https://localhost:5001/swagger` to view the Swagger UI.

---

## **API Endpoints**

### **Authentication**
- **Register**: `POST /api/auth/register`
- **Login**: `POST /api/auth/login`

### **Products**
- **Get All Products**: `GET /api/product/all`
- **Create Product**: `POST /api/product` (Seller only)
- **Update Product**: `PUT /api/product/{id}` (Seller only)
- **Delete Product**: `DELETE /api/product/{id}` (Seller only)
- **Get My Products**: `GET /api/product/my-products` (Seller only)

### **Cart**
- **Get Cart Items**: `GET /api/cart` (Buyer only)
- **Add to Cart**: `POST /api/cart` (Buyer only)
- **Remove from Cart**: `DELETE /api/cart/{cartItemId}` (Buyer only)

### **Orders**
- **Place Order**: `POST /api/order/place-order` (Buyer only)
- **View Order History**: `GET /api/order/order-history` (Buyer only)

---

## **Example Requests**

### **Register a Seller**
```bash
POST /api/auth/register
{
  "email": "seller@example.com",
  "password": "Password123!",
  "role": "Seller"
}
```

### **Login as a Seller**
```bash
POST /api/auth/login
{
  "email": "seller@example.com",
  "password": "Password123!"
}
```

### **Create a Product**
```bash
POST /api/product
{
  "name": "Product 1",
  "description": "This is a test product.",
  "price": 19.99,
  "stock": 100
}
```

### **Place an Order**
```bash
POST /api/order/place-order
```

---

## **Project Structure**

```
ECommerceApi/
├── Controllers/          # API controllers
├── Data/                # Database context and migrations
├── DTOs/                # Data transfer objects
├── Helpers/             # Utility classes (e.g., JwtHelper, MappingProfile)
├── Models/              # Domain models
├── Repositories/        # Repository interfaces and implementations
├── Program.cs           # Application entry point
├── appsettings.json     # Configuration file
└── README.md            # Project documentation
```

---

## **Contributing**

Contributions are welcome! If you'd like to contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeatureName`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/YourFeatureName`).
5. Open a pull request.

---

## **Acknowledgments**

- Thanks to the .NET team for providing an amazing framework.
- Special thanks to the open-source community for their contributions.

---

## **Contact**

If you have any questions or feedback, feel free to reach out:

- **Your Name**  
- **Email**: nadeenbar0@gmail.com
 

---
