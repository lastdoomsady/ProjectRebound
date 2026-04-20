# ProjectRebound Python Browser Prototype

This is a dependency-free Python 3.11 + tkinter prototype for the room browser.

## Run

```powershell
python Desktop\ProjectRebound.Browser.Python\project_rebound_browser.py
```

or double-click:

```text
Desktop\ProjectRebound.Browser.Python\run_browser.bat
```

## Notes

- Start the backend first: `dotnet run --project Backend\ProjectRebound.MatchServer\ProjectRebound.MatchServer.csproj`
- Config is saved to `%APPDATA%\ProjectReboundBrowser\config-python.json`.
- Create Room and Quick Match require the selected UDP port to be reachable from the backend.
- Join still launches the game with `-match=ip:port`.
- Create Room launches `ProjectReboundServerWrapper.exe` when it can find it under the configured game directory.
- This Python prototype is the current recommended GUI path; the WPF prototype remains in the repo but is not the active target.
