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

# We mount secrets so they can't end up in logs or build layers.
# We chain both restore commands so we only make the token available
# once and don't store it elsewhere.
# see:
#   - https://docs.docker.com/reference/dockerfile/#arg
#   - https://docs.docker.com/compose/how-tos/use-secrets/
RUN --mount=type=secret,id=LBHPACKAGESTOKEN \
  export LBHPACKAGESTOKEN=$(cat /run/secrets/LBHPACKAGESTOKEN) && \
  dotnet restore ./Hackney.Shared.Person/Hackney.Shared.Person.csproj && \
  dotnet restore ./Hackney.Shared.Person.Tests/Hackney.Shared.Person.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out Hackney.Shared.Person.Tests/Hackney.Shared.Person.Tests.csproj

CMD dotnet test
