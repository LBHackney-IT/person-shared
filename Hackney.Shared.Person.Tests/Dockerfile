FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./Hackney.Shared.Person.sln ./
COPY ./Hackney.Shared.Person/Hackney.Shared.Person.csproj ./Hackney.Shared.Person/
COPY ./Hackney.Shared.Person.Tests/Hackney.Shared.Person.Tests.csproj ./Hackney.Shared.Person.Tests/
COPY /nuget.config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./Hackney.Shared.Person/Hackney.Shared.Person.csproj
RUN dotnet restore ./Hackney.Shared.Person.Tests/Hackney.Shared.Person.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out Hackney.Shared.Person.Tests/Hackney.Shared.Person.Tests.csproj

CMD dotnet test
