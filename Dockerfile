FROM mcr.microsoft.com/dotnet/sdk:6.0.402 AS build-env
WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY Groffe.AspNetCore.ApiKeyChecking/Groffe.AspNetCore.ApiKeyChecking.csproj ./Groffe.AspNetCore.ApiKeyChecking/
COPY APIContagem/APIContagem.csproj ./APIContagem/
RUN dotnet restore APIContagem/APIContagem.csproj

# Build da aplicacao
COPY . ./
RUN dotnet publish APIContagem/APIContagem.csproj -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet:6.0.10
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "APIContagem.dll"]