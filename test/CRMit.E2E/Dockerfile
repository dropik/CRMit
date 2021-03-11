FROM mcr.microsoft.com/dotnet/sdk:5.0
COPY . .
WORKDIR /test/CRMit.E2E
RUN dotnet restore "CRMit.E2E.csproj"
ENTRYPOINT ["dotnet", "test"]