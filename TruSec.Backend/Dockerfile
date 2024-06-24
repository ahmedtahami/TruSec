# Use the official .NET 6 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the project files and restore any dependencies
COPY ["TruSec.Backend/TruSec.Backend.csproj", "TruSec.Backend/"]
COPY ["TruSec.BLL/TruSec.BLL.csproj", "TruSec.BLL/"]
COPY ["TruSec.DAL/TruSec.DAL.csproj", "TruSec.DAL/"]
RUN dotnet restore "TruSec.Backend/TruSec.Backend.csproj"

# Copy the remaining files and publish the application
COPY . .
RUN dotnet publish "TruSec.Backend/TruSec.Backend.csproj" -c Release -o /app/publish

# Use the official .NET 6 ASP.NET Core runtime image as the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80

# Set environment variable for connection string
ENV ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=TruSecDb;User Id=sa;Password=TRU_SEC_DEV_DB!@#123;"

# Start the application
ENTRYPOINT ["dotnet", "TruSec.Backend.dll"]
