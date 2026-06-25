# Example: Running a C# script file in a Docker Container

## To run an arbitrary script from the local directory

- `--quite`: Suppresses docker messages
- `--rm`: Runs script in a temporary container
- `./my-script-name.cs`: The name of the script to run
  - Note: Script file must contain: `#!/usr/bin/env -S dotnet` at the very top for this to work
- `arg1`, `arg2`: Arguments (if any) passed to the script file

```bash
docker compose run --quiet --rm script-runner ./my-script-name.cs arg1 arg2
```

## To rebuild the container

- Rebuilds the container itself (if necessary)

```bash
docker compose build
```

## Issues

### `Thursday, 5/25/2026`

- Note `app2.cs` doesn't run correctly with a command-line argument, need to figure out why
- Note, that the original way I had this, wasn't passing command-line arguments to the scripts
- Now with the following update, I can any arbitrary script, with command-line arguments:

```bash
# Uses ANSI escape codes: \033[1;31m turns text bold red, and \033[0m resets it back to normal
CMD ["sh", "-c", "echo -e '\\033[1;31mError: Please specify a script path.\\033[0m\\nExample: docker compose run --rm script-runner ./MyScript.cs'"]
```