# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .csproj file and restore any dependencies
COPY ./FastFoodLogs/*.csproj ./
RUN dotnet restore

# Copy the remaining source code and build the application
COPY ./FastFoodLogs/. ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM public.ecr.aws/lambda/dotnet:8
WORKDIR /var/task
COPY --from=build /app/out .

# Set the CMD to your handler
CMD [ "FastFoodLogs::FastFoodLogs.Functions::Handler" ]