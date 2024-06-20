# Use the official .NET 6 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files and restore any dependencies
COPY ["TruSec.Backend/TruSec.Backend/TruSec.Backend.csproj", "TruSec.Backend/"]
COPY ["TruSec.Backend/TruSec.BLL/TruSec.BLL.csproj", "TruSec.BLL/"]
COPY ["TruSec.Backend/TruSec.DAL/TruSec.DAL.csproj", "TruSec.DAL/"]
RUN dotnet restore "TruSec.Backend/TruSec.Backend.csproj"

# Copy the remaining files and build the application
COPY . .
WORKDIR "/src/TruSec.Backend/TruSec.Backend"
RUN dotnet build "TruSec.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TruSec.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TruSec.Backend.dll"]
