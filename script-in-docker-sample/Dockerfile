FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /app

# Ensure standard environment variables are clear about where the cache is located
ENV NUGET_PACKAGES=/root/.nuget/packages

ENTRYPOINT []

# This is just a placeholder, you can run the docker command and pass the name of the script you want, with argument
# Uses ANSI escape codes: \033[1;31m turns text bold red, and \033[0m resets it back to normal
# Provide a helpful error message if someone forgets to pass a script name
CMD ["sh", "-c", "echo '\\033[1;31mError: Please specify a script path.\\033[0m\\nExample: docker compose run --rm script-runner ./MyScript.cs'"]
