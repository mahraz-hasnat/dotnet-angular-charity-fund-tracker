# Charity Fund Tracker

Charity Fund Tracker is a web application designed to manage and track charity funds. Users can contribute to charity by making payments via bKash, providing the payment amount and transaction ID. The system verifies their payment history before saving their contribution details. All users can view the payment information through the user interface.

## Features

- **User Authentication**: Secure login and registration using JWT authentication.
- **Payment Tracking**: Users can submit payment details (amount and transaction ID) for verification and record keeping.
- **Payment Verification**: The system verifies the payment history before saving contributions.
- **Transparency**: All users can view the payment information on the UI, ensuring transparency.

## Technologies Used

- **Frontend**: Angular 17 with ng-bootstrap for UI components.
- **Backend**: .NET Web API with Entity Framework Core for data access.
- **Authentication**: JWT (JSON Web Token) for secure user authentication.
- **Database**: SQL Server (or any supported database configured with EF Core).
- **Payment Gateway**: bKash for handling charity fund transactions.

## Prerequisites

- Node.js (for Angular)
- .NET SDK
- SQL Server
- bKash account (for testing or production environment)

## Getting Started

### Backend Setup

1. Clone the repository:
   ```bash
   https://github.com/mahraz-hasnat/dotnet-angular-charity-fund-tracker.git
   cd API
   ```
2. Restore .NET dependencies:
   ```bash
   dotnet restore
   ```
3. Update the database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Your SQL Server Connection String"
   }
   ```
4. Apply migrations and seed the database:
   ```bash
   dotnet ef database update
   ```
5. Run the API:
   ```bash
   dotnet run
   ```

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd client
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Update the API endpoint in `environment.ts`:
   ```typescript
   export const environment = {
       production: false,
       apiUrl: 'http://localhost:5000/api' // Adjust if your API runs on a different port
   };
   ```
4. Start the Angular development server:
   ```bash
   ng serve
   ```
5. Open your browser and navigate to `http://localhost:4200`.

## Key Functionalities

### User Features

- **Sign Up / Sign In**: Users can register and log in securely.
- **Submit Payment Details**: Users can provide the payment amount and transaction ID for verification.
- **View Payments**: Users can view all verified payment records in the UI.

### Admin Features (Optional)

- **Manage Payments**: Administrators can verify or reject payment entries.
- **User Management**: Manage user accounts and access levels.

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register a new user.
- `POST /api/auth/login` - Log in and retrieve a JWT token.

### Payments
- `POST /api/payments` - Submit payment details.
- `GET /api/payments` - Retrieve all payment records (secured endpoint).

## Future Enhancements

- **Email Notifications**: Notify users upon successful payment verification.
- **Dashboard**: Add a summary dashboard for admins to track total contributions.
- **Mobile-Friendly Design**: Optimize the UI for mobile devices.

## Contributing

Contributions are welcome! Please fork this repository, make your changes, and submit a pull request. For major changes, please open an issue first to discuss your ideas.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## Contact

For inquiries or support, please contact [abulhasnatmahraz23@gmail.com].

---

**Thank you for contributing to charity through technology!**

