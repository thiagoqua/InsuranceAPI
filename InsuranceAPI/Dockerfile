FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App
COPY . ./
RUN dotnet restore
#EXPOSE 5000
ENTRYPOINT ["dotnet", "run"]
